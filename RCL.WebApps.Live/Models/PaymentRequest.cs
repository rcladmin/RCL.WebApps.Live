#nullable disable

namespace RCL.WebApps.Live.Models
{
    public class PaymentRequest
    {
        public int Id { get; set; }
        public DateTime dateCreated { get; set; }
        public int subscriptionId { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public string orderId { get; set; }
        public string paymentLink { get; set; }
        public string code { get; set; }
    }
}
