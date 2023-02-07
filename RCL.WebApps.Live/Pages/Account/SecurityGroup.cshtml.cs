using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCL.Core.Identity.Tools;

namespace RCL.WebApps.Live.Pages.Account
{
    [Authorize]
    public class SecurityGroupModel : PageModel
    {
        public List<string> Groups { get; set; } = new List<string>();

        public void OnGet()
        {
            Groups = GroupClaims.GetGroups(User);
        }
    }
}
