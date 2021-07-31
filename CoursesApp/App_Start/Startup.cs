using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(CoursesApp.App_Start.Startup))]

namespace CoursesApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CookieAuthenticationOptions cookeAuthOptions = new CookieAuthenticationOptions();
            cookeAuthOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookeAuthOptions.LoginPath = new PathString("/account/login");

            app.UseCookieAuthentication(cookeAuthOptions);

        }
    }
}
