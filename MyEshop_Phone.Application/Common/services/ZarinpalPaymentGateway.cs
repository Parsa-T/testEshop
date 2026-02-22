using Microsoft.Extensions.Options;
using MyEshop_Phone.Application.Common.Interfaces;
using MyEshop_Phone.Application.Common.model;
using MyEshop_Phone.Application.Common.setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Common.services
{
    public class ZarinpalPaymentGateway : IPaymentGateway
    {
        private readonly HttpClient _httpClient;
        private readonly ZarinpalSettings _settings;

        public ZarinpalPaymentGateway(
            HttpClient httpClient,
            IOptions<ZarinpalSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<string?> RequestPaymentAsync(int orderId, decimal amount)
        {
            var body = new
            {
                merchant_id = _settings.MerchantId,
                amount = (int)amount * 10,
                callback_url = _settings.CallbackUrl,
                description = $"Order #{orderId}"
            };

            var response = await _httpClient.PostAsJsonAsync(
                "https://api.zarinpal.com/pg/v4/payment/request.json",
                body);
            //var response = await _httpClient.PostAsJsonAsync(
            //    "https://sandbox.zarinpal.com/pg/v4/payment/request.json",
            //    body);

            var result = await response.Content
    .ReadFromJsonAsync<ZarinpalRequestResponse>();

            if (result?.data?.code == 100)
            {
                return result.data.authority;
            }

            return null;
        }

        public async Task<PaymentVerifyResult> VerifyAsync(string authority, decimal amount)
        {
            var body = new
            {
                merchant_id = _settings.MerchantId,
                authority = authority,
                amount = (int)amount * 10
            };

            var response = await _httpClient.PostAsJsonAsync(
    "https://api.zarinpal.com/pg/v4/payment/verify.json",
    body);
    //        var response = await _httpClient.PostAsJsonAsync(
    //"https://sandbox.zarinpal.com/pg/v4/payment/verify.json",
    //body);

            var result = await response.Content
    .ReadFromJsonAsync<ZarinpalVerifyResponse>();

            if (result?.data?.code == 100)
            {
                return new PaymentVerifyResult
                {
                    IsSuccess = true,
                    RefId = result.data.ref_id
                };
            }

            return new PaymentVerifyResult { IsSuccess = false };
        }
    }
}
