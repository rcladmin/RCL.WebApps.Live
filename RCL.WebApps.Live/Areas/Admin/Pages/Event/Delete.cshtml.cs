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
    public class DeleteModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        public Models.Event Event { get; set; } = new Models.Event();
        public string ErrorMessage { get; set; } = string.Empty;

        public DeleteModel(LiveDbContext db, IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage = blobStorage;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                // TODO : Disable delete if there are related records.

                Event = await _db.Events.FindAsync(id);

                if (!string.IsNullOrEmpty(Event.image))
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                Event = await _db.Events.FindAsync(id);

                int GroupId = Event.groupId;

                await FileUploadHelper.DeleteFileAsync(Event.image, _blobStorage);

                _db.Events.Remove(Event);
                await _db.SaveChangesAsync();

                return RedirectToPage("./Index", new  {groupId = GroupId });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
