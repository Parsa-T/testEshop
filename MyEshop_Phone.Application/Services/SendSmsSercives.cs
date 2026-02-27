using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class SendSmsSercives
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IOtpRateLimitService _otpRateLimitService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        ISmsOtpRequestRepository _smsOtpRequestRepository;
        private readonly ILogger<SendSmsSercives> _logger;

        public SendSmsSercives(
            HttpClient httpClient,
            IConfiguration configuration,
            IOtpRateLimitService otpRateLimitService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<SendSmsSercives> logger,
            ISmsOtpRequestRepository smsOtpRequestRepository)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Sms:ApiKey"];
            _otpRateLimitService = otpRateLimitService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _smsOtpRequestRepository = smsOtpRequestRepository;
        }

        public async Task<bool> SendOtpAsync(string mobile, string code)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null)
                    return false;

                var ipAddress = httpContext.Request.Headers["X-Forwarded-For"]
                    .FirstOrDefault()
                    ?? httpContext.Connection.RemoteIpAddress?.ToString();

                if (string.IsNullOrEmpty(ipAddress))
                    return false;

                // ⭐ ثبت Log درخواست قبل از ارسال SMS
                var sms = new _SmsOtpRequest()
                {
                    Mobile = mobile,
                    IpAddress = ipAddress,
                    RequestedAt = DateTime.UtcNow,
                    IsBlocked = false
                };
                await _smsOtpRequestRepository.AddRequestSms(sms);

                await _smsOtpRequestRepository.Save();

                // ⭐ RateLimit بررسی
                var canSend = await _otpRateLimitService
                    .CanSendOtpAsync(mobile, ipAddress, false);

                if (!canSend)
                    return false;

                // ⭐ ارسال SMS
                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "https://api.sms.ir/v1/send/verify"
                );

                request.Headers.Add("X-API-KEY", _apiKey);
                request.Headers.Add("Accept", "application/json");

                var body = new
                {
                    mobile = mobile,
                    templateId = 101465,
                    parameters = new[]
                    {
                new { name = "Code", value = code }
            }
                };

                request.Content = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json"
                );

                _httpClient.Timeout = TimeSpan.FromSeconds(10);

                var response = await _httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
