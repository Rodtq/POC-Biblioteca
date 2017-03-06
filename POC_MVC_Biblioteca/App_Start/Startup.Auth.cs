using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace POC_MVC_Biblioteca.App_Start
{
    public static class CustomAuthentication
    {
        public const String ApplicationCookie = "MyProjectAuthenticationType";
    }
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CustomAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "MyCookieName",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(12)
            });
        }
    }

}