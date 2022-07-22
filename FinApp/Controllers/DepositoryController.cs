using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.Entities.Identity.Account;
using FinApp.Entities.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class DepositoryController : Controller
    {
        private UserManagerImpl UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManagerImpl>();
            }
        }
        private UserContext context
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserContext>();
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            var id = User.Identity.GetUserId();
            UserApp user = context.Users.Where(i => i.Id == id).Include(i => i.depositories).First();// .ToList().Find(i => i.Id == id);
            return View(user.depositories);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, int amount)
        {
            UserApp userApp = UserManager.FindByName(User.Identity.Name);
            Depository depository = new Depository { user = userApp, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            userApp.depositories.Add(depository);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var idUser = User.Identity.GetUserId();
            Depository current = context.Users.Where(i => i.Id == idUser).Include(i => i.depositories).First().depositories.Where(i => i.id == id).First();
            return View(current);
        }

        [HttpPost]
        public ActionResult Rename(string name, int id)
        {
            if (name != null || name.Trim() != "")
            {
                var idUser = User.Identity.GetUserId();
                context.Users.Where(i => i.Id == idUser).Include(i => i.depositories).First().depositories.Where(i => i.id == id).Single().name = name;
                context.SaveChanges();
            }
            return RedirectToAction($"/Details/{id}", id);
        }

        [HttpGet]
        public ActionResult Count()
        {
            var idUser = User.Identity.GetUserId();
            int dep_count = context.Users.Where(i => i.Id == idUser).Include(i => i.depositories).First().depositories.Count();
            int credit_count = context.Users.Where(i => i.Id == idUser).Include(i => i.credits).First().credits.Count();

            return Json(new { dep_count, credit_count }, JsonRequestBehavior.AllowGet);
        }




    }
}