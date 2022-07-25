using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.Entities.Identity.Account;
using FinApp.Entities.Identity.Managers;
using FinApp.services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class DepositoryController : Controller
    {
        private FinContext financeContext
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<FinContext>();
            }
        }

        private OperationService operationService = new OperationService();


        [HttpGet]
        public ActionResult List()
        {
            var id = User.Identity.GetUserId();
            return View(financeContext.depositories.Where(i => i.idUser == id).ToList());
        }


        [HttpPost]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, double amount)
        {
            var id = User.Identity.GetUserId();
            Depository depository = new Depository { idUser = id, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            financeContext.depositories.Add(depository);
            financeContext.SaveChanges();
            return RedirectToAction("List");
        }



        [HttpGet]
        public ActionResult Details(int id)
        {
            var idUser = User.Identity.GetUserId();
            Depository current = financeContext.depositories.Where(i => i.idUser == idUser && i.id == id).First();
            return View(current);
        }

        [HttpPost]
        public ActionResult Rename(string name, int id)
        {
            if (name != null || name.Trim() != "")
            {
                var idUser = User.Identity.GetUserId();
                financeContext.depositories.First(i => i.id == id).name = name;
                financeContext.SaveChanges();
            }
            return RedirectToAction($"/Details/{id}", id);
        }

        [HttpGet]
        public ActionResult Count()
        {
            var idUser = User.Identity.GetUserId();
            int dep_count = financeContext.depositories.Where(i => i.idUser == idUser).Count();
            int credit_count = financeContext.credits.Where(i => i.idUser == idUser).Count();
            return Json(new { dep_count, credit_count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var idUser = User.Identity.GetUserId();
            financeContext.depositories.Where(i => i.idUser == idUser).ToList().RemoveAt(id);
            financeContext.SaveChanges();
            return RedirectToAction("/List");
        }

        [HttpPost]
        public ActionResult Change(int idDepository, bool isSpending, string amountOfMoney, string comment)
        {
            var idUser = User.Identity.GetUserId();
            double amount = Double.Parse(amountOfMoney, CultureInfo.InvariantCulture);
            var depository = financeContext.depositories.Where(i => i.idUser == idUser && i.id == idDepository).First();
            if (isSpending)
            {
                if (depository.amount >= amount)
                {
                    depository.amount -= amount;
                }
            }
            else
            {
                depository.amount += amount;
            }
            financeContext.SaveChanges();
            operationService.SaveToHistory(idDepository, isSpending, amount, comment, idUser);
            return RedirectToAction($"/Details/{idDepository}", idDepository);
        }

        [HttpGet]
        public ActionResult HistoryById(int id)
        {
            List<FinanceOperation> history = operationService.getById(id);
            return Json(history, JsonRequestBehavior.AllowGet);
        }
    }
}