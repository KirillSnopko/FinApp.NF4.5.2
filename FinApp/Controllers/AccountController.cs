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
using System;
using log4net;

namespace FinApp.Controllers
{
    public class AccountController : Controller
    {
        private IFinanceService financeService;
        private IAccountService accountService;
        private ILog logger;

        public AccountController(IFinanceService financeService, IAccountService accountService, ILog loggerAccount)
        {
            this.financeService = financeService;
            this.accountService = accountService;
            this.logger = loggerAccount;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string name, string password)
        {
            if (name.Trim() != string.Empty && name != null && password != null && password.Trim() != string.Empty)
            {
                accountService.login(name, password);
                return Redirect("/");
            }
            else { return Json("Invalid input"); }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string name, string password, string email)
        {
            if (name.Trim() != string.Empty && password.Trim() != string.Empty && email.Trim() != string.Empty && name != null && password != null && email != null)
            {
                accountService.register(name, password, email);
                logger.Info($"created new user => name: {name}, email: {email}");
                return RedirectToAction("Index");
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
            if (password.Trim() != string.Empty && password != null)
            {
                accountService.removeAccount(password, name);
                financeService.cleanUpAccountById(id);
                logger.Info($"removed  user => id: {id}, name: {name}");
                return RedirectToAction("Logout");
            }
            else
            {
                return Json("invalid input");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string name)
        {
            if (name.Trim() != string.Empty && name != null)
            {
                var idUser = User.Identity.GetUserId();
                var currentName = User.Identity.Name;
                accountService.rename(name, idUser);
                logger.Info($"renamed user => id: {idUser}, old name: {currentName}, new name: {name}");
                return RedirectToAction("Index");
            }
            return Json("invalid input");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string old_password, string new_password)
        {
            if (new_password.Trim() != string.Empty && old_password.Trim() != string.Empty && new_password != null && old_password != null)
            {
                var idUser = User.Identity.GetUserId();
                var userName = User.Identity.Name;
                accountService.changePassword(old_password, new_password, userName);
                logger.Info($"user changed password => id: {idUser}, name: {userName}");
                return RedirectToAction("Index");
            }
            return Json("invalid input");
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.Exception != null)
            {
                var response = new { Status = 509, Message = filterContext.Exception.Message };
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = response
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}