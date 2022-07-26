using FinApp.Entities.Identity.Managers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using FinApp.Entities.Identity.Account;
using System.Linq;
using FinApp.Entities.Finance;
using System.Collections.Generic;
using FinApp.service;
using System.Web.Security;

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


        private FinanceService financeService = new FinanceService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Login(string name, string password)
        {
            UserApp user = await UserManager.FindAsync(name, password);

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

            return Json("error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string name, string password, string email)
        {
            if (name.Trim() != "" && password.Trim() != "" && email.Trim() != "")
            {
                UserApp user = new UserApp { UserName = name, Email = email };
                IdentityResult result = UserManager.Create(user, password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Json("error");
                }
            }
            return Json("error");
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return Redirect("/");
        }

        /*
         [HttpPost]
         [Authorize]
         public ActionResult Remove(string password)
         {

             string name = User.Identity.GetUserName();
             UserApp user = UserManager.Find(name, password);
             if (user != null)
             {
                financeService.operationRepo.
                 IdentityResult result = UserManager.Delete(user);
             }
             UserManager.

             AppUser user = await UserManager.FindByIdAsync(id);

             if (user != null)
             {
                 IdentityResult result = await UserManager.DeleteAsync(user);
                 if (result.Succeeded)
                 {
                     return RedirectToAction("Index");
                 }
                 else
                 {
                     return View("Error", result.Errors);
                 }
             }
             else
             {
                 return View("Error", new string[] { "Пользователь не найден" });
             }
         }
        */

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string name)
        {
            var id = User.Identity.GetUserId();
            UserApp user = UserManager.FindById(id);
            user.UserName = name;
            IdentityResult result = UserManager.Update(user);
            
            if (result.Succeeded)
            {
                // FormsAuthentication.SetAuthCookie(name, true);
                ClaimsIdentity ident =  UserManager.CreateIdentity(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);

                return RedirectToAction("Index");
            }
            return Json("error");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string old_password, string new_password)
        {
            UserManager.ChangePassword(User.Identity.GetUserId(), old_password, new_password);
            return RedirectToAction("Index");
        }
    }
}