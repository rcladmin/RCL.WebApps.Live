#nullable disable

namespace RCL.WebApps.Live.Options
{
    public class PaymentRequestOptions
    {
        public string ApiEndpoint { get; set; }
        public string SubscriptionId { get; set; }
        public string ApiKey { get; set; }
        public string WebhookReceiverSecurityKey { get; set; }
    }
}
