using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.Entities.Identity.Account;
using FinApp.Entities.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    public class DepositoryController : Controller
    {
        private UserManagerImpl UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManagerImpl>();
            }
        }

        [Authorize]
        public ActionResult List()
        {
            UserContext context = new UserContext();

            UserApp user = UserManager.FindById(User.Identity.GetUserId());
            return View(user.depositories.ToList());
            //return Json(user.depositories.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name)
        {
            UserApp userApp = UserManager.FindByName(User.Identity.Name);
            Depository depository = new Depository { user = userApp, typeDep = tDep, typeMoney = tMoney, name = name };
            userApp.depositories.Add(depository);
            //UserManager.Update(userApp);
            UserContext.Create().SaveChanges();

            return RedirectToAction("Depository");
        }


    }
}