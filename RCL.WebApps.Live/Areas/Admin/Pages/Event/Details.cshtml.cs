#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;

namespace RCL.WebApps.Live.Areas.Admin.Pages.Event
{
    [Authorize(Policy = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        public Models.Event Event { get; set; } = new Models.Event();
        public string ErrorMessage { get; set; } = string.Empty;

        public DetailsModel(LiveDbContext db, 
            IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage = blobStorage;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Event = await _db.Events.FindAsync(id);

                if (!string.IsNullOrEmpty(Event?.image))
                {
                    Event.image = _blobStorage.GetBlobSasUri(ConstantsHelper.BLOBCONTAINER, Event.image);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
