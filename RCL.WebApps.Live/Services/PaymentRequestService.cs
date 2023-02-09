#nullable disable

using Microsoft.Extensions.Options;
using RCL.WebApps.Live.Models;
using RCL.WebApps.Live.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RCL.WebApps.Live.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private static readonly HttpClient _client;
        private readonly IOptions<PaymentRequestOptions> _options;

        static PaymentRequestService()
        {
            _client = new HttpClient();
        }

        public PaymentRequestService(IOptions<PaymentRequestOptions> options)
        {
            _options = options;
        }

        public async Task<PaymentRequest> GeneratePaymentLinkAsync(PaymentRequest paymentRequest)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Value.ApiKey);

                var response = await _client.PostAsync($"{_options.Value.ApiEndpoint}/v1/payment/paymentrequest/subscriptionid/{_options.Value.SubscriptionId}/create",
                     new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json"));

                string content = ResolveContent(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    PaymentRequest result = JsonSerializer.Deserialize<PaymentRequest>(content);
                    return result;
                }
                else
                {
                    throw new Exception($"{response.StatusCode} : {content}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ResolveContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            else
            {
                if (content.ToLower().Contains("!doctype html"))
                {
                    return string.Empty;
                }
                else
                {
                    return content;
                }
            }
        }
    }
}
