#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;

namespace RCL.WebApps.Live.Areas.Admin.Pages.Group
{
    [Authorize(Policy = "Admin")]
    public class EditModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        [BindProperty]
        public Models.Group Group { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public EditModel(LiveDbContext db,
            IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage = blobStorage;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Group = await _db.Groups.FindAsync(id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
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
                    if (fileImage != null)
                    {
                        string s = Helpers.FileUploadHelper.FileChecker(fileImage, true);

                        if (s != Helpers.FileUploadHelper.FILE_OK)
                        {
                            ErrorMessage = s;
                            return Page();
                        }

                        string oldBlobName = Group.image;
                        string newBlobName = await Helpers.FileUploadHelper.SaveFileAsync(fileImage, _blobStorage);
                        
                        if (string.IsNullOrEmpty(newBlobName))
                        {
                            ErrorMessage = "Could not save image file";
                            return Page();
                        }
                        else
                        {
                            Group.image = newBlobName;
                            await Helpers.FileUploadHelper.DeleteFileAsync(oldBlobName, _blobStorage);
                        }
                    }

                    _db.Attach(Group).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    return RedirectToPage("./Index");
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
