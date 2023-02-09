#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RCL.Core.Azure.BlobStorage;
using RCL.Core.Identity.Tools;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;
using RCL.WebApps.Live.Models;
using RCL.WebApps.Live.Services;

namespace RCL.WebApps.Live.Pages.Event
{
    public class DetailsModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;
        private readonly IPaymentRequestService _paymentRequestService;

        public Models.Event Event { get; set; } = new Models.Event();
        public Models.EventAttendee EventAttendee { get; set; } = new EventAttendee();
        public string ErrorMessage { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsFree { get; set; } = false;

        public DetailsModel(LiveDbContext db,
            IAzureBlobStorageService blobStorage,
            IPaymentRequestService paymentRequestService)
        {
            _db = db;
            _blobStorage = blobStorage;
            _paymentRequestService = paymentRequestService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await GetEventAsync(id);

            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                await GetEventAttendeeAsync(id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await GetEventAsync(id);
                await GetEventAttendeeAsync(id);

                if(string.IsNullOrEmpty(Status))
                {

                   if(Event.price > 0)
                    {
                        PaymentRequest paymentRequest = new PaymentRequest
                        {
                            amount = Event.price,
                            currency = Event.currency.ToLower(),
                            orderId = $"{ UserClaimsHelper.GetUserDataFromClaims(User).ObjectId}|{Event.id}"
                        };

                        var reponse = await _paymentRequestService
                            .GeneratePaymentLinkAsync(paymentRequest);

                        if (!string.IsNullOrEmpty(reponse?.paymentLink))
                        {
                            return Redirect(reponse.paymentLink);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        private async Task GetEventAsync(int id)
        {
            try
            {
                Event = await _db.Events.FindAsync(id);

                if (!string.IsNullOrEmpty(Event?.image))
                {
                    Event.image = _blobStorage.GetBlobSasUri(ConstantsHelper.BLOBCONTAINER, Event.image);
                }

                if (Event.price == 0)
                {
                    IsFree = true;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task GetEventAttendeeAsync(int id)
        {
            try
            {
                UserClaimsData claims = UserClaimsHelper.GetUserDataFromClaims(User);

                EventAttendee = await _db.EventAttendees
                    .Where(w => w.userId == claims.ObjectId && w.eventId == Event.id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(EventAttendee?.status))
                {
                    Status = EventAttendee?.status;
                }

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
