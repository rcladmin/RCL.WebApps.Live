using Microsoft.Extensions.DependencyInjection.Extensions;
using RCL.Core.Identity.Graph;
using RCL.WebApps.Live.Options;
using RCL.WebApps.Live.Services;

namespace RCL.WebApps.Live.Extensions
{
    public static class PaymentRequestExtension
    {
        public static IServiceCollection AddPaymentRequestServices(this IServiceCollection services,
            Action<PaymentRequestOptions> setupAction)
        {
            services.TryAddTransient<IPaymentRequestService, PaymentRequestService>();
            services.Configure(setupAction);

            return services;
        }
    }
}
