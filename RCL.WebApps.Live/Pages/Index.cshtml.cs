#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RCL.Core.Azure.BlobStorage;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Helpers;

namespace RCL.WebApps.Live.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly LiveDbContext _db;
        private readonly IAzureBlobStorageService _blobStorage;

        public List<Models.Event> Events { get; set; } = new List<Models.Event>();

        public IndexModel(LiveDbContext db,
            IAzureBlobStorageService blobStorage)
        {
            _db = db;
            _blobStorage= blobStorage;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await GetEvents();

            return Page();
        }

        private async Task GetEvents()
        {
            try
            {
                Events = await _db.Events
                     .Where(w => w.status == "Active")
                     .OrderByDescending(o => o.start)
                     .Take(20)
                     .ToListAsync();

                if(Events?.Count > 0)
                {
                    foreach(var item in Events)
                    {
                        if(!string.IsNullOrEmpty(item.image))
                        {
                            item.image = _blobStorage.GetBlobSasUri(ConstantsHelper.BLOBCONTAINER, item.image);
                        }
                    }
                }
            }
            catch(Exception) { }
        }
    }
}