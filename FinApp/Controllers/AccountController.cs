﻿using FinApp.Entities.Identity.Managers;
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
using System;
using log4net;
using FinApp.Exceptions;

namespace FinApp.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;
        private ILog logger;

        public AccountController(IAccountService accountService, ILog loggerAccount)
        {
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
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { status = 500, message = "invalid input" });
            }
            accountService.login(name, password);
            return Json(new { status = 200 });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string name, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return Json(new { status = 500, message = "invalid input" });
            }
            accountService.register(name, password, email);
            logger.Info($"created new user => name: {name}, email: {email}");
            return Json(new { status = 200 });
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
            if (string.IsNullOrWhiteSpace(password))
            {
                return Json(new { status = 500, message = "invalid password" });
            }
            string name = User.Identity.GetUserName();
            string id = User.Identity.GetUserId();
            accountService.removeAccount(password, name);
            accountService.logout();
            logger.Info($"removed  user => id: {id}, name: {name}");
            return Json(new { status = 200 });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { status = 500, message = "invalid input" });
            }
            var idUser = User.Identity.GetUserId();
            var currentName = User.Identity.Name;
            accountService.rename(name, idUser);
            logger.Info($"renamed user => id: {idUser}, old name: {currentName}, new name: {name}");
            return Json(new { status = 200 });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(string old_password, string new_password)
        {
            if (string.IsNullOrWhiteSpace(new_password) || string.IsNullOrWhiteSpace(old_password))
            {
                return Json(new { status = 500, message = "invalid input" });
            }
            var idUser = User.Identity.GetUserId();
            var userName = User.Identity.Name;
            accountService.changePassword(old_password, new_password, userName);
            logger.Info($"user changed password => id: {idUser}, name: {userName}");
            return Json(new { status = 200 });
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                logger.Error(filterContext.Exception);
                var response = new { status = 509, message = filterContext.Exception.Message };
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