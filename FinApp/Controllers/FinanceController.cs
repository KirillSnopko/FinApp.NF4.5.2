using FinApp.Entities.Identity.Managers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    public class FinanceController : Controller
    {
        private UserManagerImpl UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManagerImpl>();
            }
        }

        // GET: Finance
        public ActionResult Index()
        {
            return View();
        }
    }
}