using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCL.Core.Identity.Tools;

namespace RCL.WebApps.Live.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public UserClaimsData UserData { get; set; } = new UserClaimsData();

        public void OnGet()
        {
            UserData = UserClaimsHelper.GetUserDataFromClaims(User);
            string address = UserData.StreetAddress;
        }
    }
}
