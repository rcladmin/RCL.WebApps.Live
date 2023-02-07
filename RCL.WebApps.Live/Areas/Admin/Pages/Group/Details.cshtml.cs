#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;

namespace RCL.WebApps.Live.Areas.Admin.Pages.Group
{
    [Authorize(Policy = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        public Models.Group Group { get; set; } = new Models.Group();
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
                Group = await _db.Groups.FindAsync(id);

                if(!string.IsNullOrEmpty(Group.image))
                {
                    Group.image = _blobStorage.GetBlobSasUri(ConstantsHelper.BLOBCONTAINER, Group.image);
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
