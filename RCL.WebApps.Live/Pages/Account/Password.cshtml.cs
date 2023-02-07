using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RCL.WebApps.Live.Pages.Account
{
    [Authorize]
    public class PasswordModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
