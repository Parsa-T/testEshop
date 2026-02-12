using Microsoft.Extensions.Configuration;
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

        public SendSmsSercives(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Sms:ApiKey"];
        }

        public async Task<bool> SendOtpAsync(string mobile, string code)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.sms.ir/v1/send/verify"
            );

            // هدرها طبق مستندات
            request.Headers.Add("X-API-KEY", _apiKey);
            request.Headers.Add("Accept", "application/json");

            var body = new
            {
                mobile = mobile,
                templateId = 136996,
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

            var response = await _httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }


    }
}
