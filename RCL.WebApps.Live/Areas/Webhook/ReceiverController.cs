#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;
using RCL.WebApps.Live.Models;
using RCL.WebApps.Live.Options;
using System.Text.Json;

namespace RCL.WebApps.Live.Areas.Webhook
{
    [Route("webhook/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly LiveDbContext _db;
        private readonly IOptions<PaymentRequestOptions> _options;

        public ReceiverController(LiveDbContext db,
            IOptions<PaymentRequestOptions> options)
        {
            _db = db;
            _options = options;
        }

        // POST: webhook/Receiver
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (IsAuthorized(this.Request) == true)
            {
                await ProcessTransactionAsync(transaction);
            }

            return new OkResult();
        }

        private async Task ProcessTransactionAsync(Transaction transaction)
        {
            if (transaction?.transactionType == ConstantsHelper.CHARGE)
            {
                transaction.externalId = transaction.Id;
                transaction.Id = null;

                var newTransaction = await AddNewTransactionAsync(transaction);

                if (!string.IsNullOrEmpty(newTransaction?.status))
                {
                    string userId = transaction.orderId.Split('|')[0];
                    int eventId = Convert.ToInt32(transaction.orderId.Split("|")[1]);

                    EventAttendee eventAttendee = new EventAttendee
                    {
                        chargeDate = (DateTime)transaction?.chargeDate,
                        eventId = eventId,
                        orderId = transaction.orderId,
                        status = ConstantsHelper.REGISTERED,
                        transactionId = (int)newTransaction?.Id,
                        userId = userId
                    };

                    var newEventAttendee = await AddNewEventAttendeeAsync(eventAttendee);
                }
            }
        }

        private bool IsAuthorized(HttpRequest request)
        {
            bool b = false;

            try
            {
                if (request.Headers.TryGetValue("Authorization", out var values))
                {
                    string authHeader = values.FirstOrDefault() ?? string.Empty;

                    if (authHeader != string.Empty && authHeader.StartsWith("Bearer"))
                    {
                        string apikey = authHeader.Replace("Bearer", "").Trim();

                        if (!string.IsNullOrEmpty(apikey))
                        {
                            if (apikey == _options.Value.WebhookReceiverSecurityKey)
                            {
                                b = true;
                            }
                        }
                    }
                }
            }
            catch { }

            return b;
        }

        private async Task<Transaction> AddNewTransactionAsync(Transaction transaction)
        {
            try
            {
                await _db.AddAsync(transaction);
                await _db.SaveChangesAsync();

                return transaction;
            }

            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return new Transaction();
        }

        private async Task<EventAttendee> AddNewEventAttendeeAsync(EventAttendee eventAttendee)
        {
            try
            {
                await _db.AddAsync(eventAttendee);
                await _db.SaveChangesAsync();

                return eventAttendee;
            }

            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return new EventAttendee();
        }
    }
}
