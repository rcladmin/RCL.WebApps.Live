#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;

namespace RCL.WebApps.Live.Areas.Admin.Pages.Group
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        public IndexModel(LiveDbContext db,
            IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage = blobStorage;
        }

        public List<Models.Group> Groups { get; set; } = new List<Models.Group>();
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Groups = await _db.Groups
                    .AsNoTracking()
                    .OrderByDescending(o => o.ranking)
                    .ToListAsync();

                if(Groups?.Count > 0)
                {
                    foreach(var group in Groups)
                    {
                        if(!string.IsNullOrEmpty(group.image))
                        {
                            group.image = _blobStorage.GetBlobSasUri(ConstantsHelper.BLOBCONTAINER, group.image);
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
    }
}
