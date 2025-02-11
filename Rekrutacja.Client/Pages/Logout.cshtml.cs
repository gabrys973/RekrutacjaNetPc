using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rekrutacja.Client.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LogoutModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = _configuration["applicationUrl"]
                },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}