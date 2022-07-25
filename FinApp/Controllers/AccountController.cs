using FinApp.Entities.Identity.Managers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using FinApp.Entities.Identity.Account;
using FinApp.Models;
using System.Linq;

namespace FinApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManagerImpl UserManager
        {
            get
            {

                return HttpContext.GetOwinContext().GetUserManager<UserManagerImpl>();
            }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Login(LoginModel details)
        {
            UserApp user = await UserManager.FindAsync(details.Name, details.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректное имя или пароль.");
            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);

                if (UserManager.GetRoles(user.Id).Any(s => s.Equals("Admin")))
                {
                    return Redirect("/");
                }
                return Redirect("/");
            }

            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return Redirect("/");
        }
    }
}