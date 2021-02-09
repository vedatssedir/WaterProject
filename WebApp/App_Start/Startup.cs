using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WebApp;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Panel/PanelGiris/Index"),
                LogoutPath = new PathString("/Panel/PanelGiris/Index"),
                CookieSecure = CookieSecureOption.SameAsRequest,
                CookieName = "Taskesti",
                SlidingExpiration = true,
                CookieSameSite = SameSiteMode.None
            });

        }
    }
}
