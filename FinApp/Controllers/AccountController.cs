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
using FinApp.service.ifaces;
using FinApp.Exceptions;

namespace FinApp.Controllers
{
    public class AccountController : Controller
    {
        private IFinanceService financeService = new FinanceService();
        private IAccountService accountService = new AccountService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string name, string password)
        {
            try
            {
                accountService.login(name, password);
                return Redirect("/");
            }
            catch (AccountServiceException ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string name, string password, string email)
        {
            if (name.Trim() != string.Empty && password.Trim() != string.Empty && email.Trim() != string.Empty)
            {
                try
                {
                    accountService.register(name, password, email);
                    return RedirectToAction("Index");
                }
                catch (AccountServiceException ex)
                {
                    return Json(ex.Message);
                }
            }
            return Json("invalid input");
        }

        [Authorize]
        public ActionResult Logout()
        {
            accountService.logout();
            return Redirect("/");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string password)
        {
            string name = User.Identity.GetUserName();
            string id = User.Identity.GetUserId();
            try
            {
                accountService.removeAccount(password, name);
                financeService.cleanUpAccountById(id);
                return RedirectToAction("Logout");
            }
            catch (AccountServiceException ex)
            {
                return Json(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string name)
        {
            if (name.Trim() != string.Empty)
            {
                var idUser = User.Identity.GetUserId();
                try
                {
                    accountService.rename(name, idUser);
                    return RedirectToAction("Index");
                }
                catch (AccountServiceException ex)
                {
                    return Json(ex.Message);
                }

            }
            return Json("invalid input");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string old_password, string new_password)
        {
            if (new_password.Trim() != string.Empty)
            {
                var idUser = User.Identity.GetUserId();
                accountService.changePassword(old_password, new_password, idUser);
                return RedirectToAction("Index");
            }
            return Json("invalid input");
        }
    }
}