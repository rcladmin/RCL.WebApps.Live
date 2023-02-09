#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;

namespace RCL.WebApps.Live.Areas.Admin.Pages.Event
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        public List<Models.Event> Events { get; set; } = new List<Models.Event>();
        public Models.Group Group { get; set; } = new Models.Group();
        public string ErrorMessage { get; set; } = string.Empty;

        public IndexModel(LiveDbContext db, 
            IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage = blobStorage;
        }

        public async Task<IActionResult> OnGetAsync(int groupId)
        {
            try
            {
                Group = await _db.Groups.FindAsync(groupId);

                Events = await _db.Events
                    .Where(w => w.groupId == groupId)
                    .AsNoTracking()
                    .OrderByDescending(o => o.start)
                    .ToListAsync();

                if (Events?.Count > 0)
                {
                    foreach (var item in Events)
                    {
                        if (!string.IsNullOrEmpty(item.image))
                        {
                            item.image = _blobStorage.GetBlobSasUri(ConstantsHelper.BLOBCONTAINER, item.image);
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
