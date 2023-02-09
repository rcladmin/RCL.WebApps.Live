using RCL.WebApps.Live.Models;

namespace RCL.WebApps.Live.Services
{
    public interface IPaymentRequestService
    {
        Task<PaymentRequest> GeneratePaymentLinkAsync(PaymentRequest paymentRequest);
    }
}
