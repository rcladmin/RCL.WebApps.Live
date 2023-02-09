#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;

namespace RCL.WebApps.Live.Areas.Admin.Pages.Event
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        [BindProperty]
        public Models.Event Event { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int GroupId { get; set; }

        public CreateModel(LiveDbContext db,
            IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage = blobStorage;
        }

        public void OnGet(int groupId)
        {
            GroupId = groupId;
            return;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile fileImage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Data input was not valid";

                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            ErrorMessage = $"{ErrorMessage},{modelError.ErrorMessage}";
                        }
                    }
                }
                else
                {
                    string s = Helpers.FileUploadHelper.FileChecker(fileImage, true);

                    if (s != Helpers.FileUploadHelper.FILE_OK)
                    {
                        ErrorMessage = s;
                        return Page();
                    }

                    string imageBlob = await Helpers.FileUploadHelper
                        .SaveFileAsync(fileImage, _blobStorage);

                    if (string.IsNullOrEmpty(imageBlob))
                    {
                        ErrorMessage = "Could not save file";
                        return Page();
                    }
                    else
                    {
                        Event.image = imageBlob;
                    }

                    await _db.AddAsync(Event);
                    await _db.SaveChangesAsync();

                    return RedirectToPage("./Index", new { groupId = Event.groupId });
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
