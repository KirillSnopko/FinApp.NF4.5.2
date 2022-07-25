using FinApp.Entities.Database;
using FinApp.Entities.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;


namespace FinApp.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<UserContext>(UserContext.Create);
            app.CreatePerOwinContext<FinContext>(FinContext.Create);
            app.CreatePerOwinContext<UserManagerImpl>(UserManagerImpl.Create);
            app.CreatePerOwinContext<RoleManagerImpl>(RoleManagerImpl.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

        }
    }
}