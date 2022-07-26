using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class DepositoryController : Controller
    {
        private FinanceService financeService = new FinanceService();


        [HttpGet]
        public ActionResult List()
        {
            var id = User.Identity.GetUserId();
            return View(financeService.depositoryRepo.depositoriesByUserId(id));
        }

        [HttpPost]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, double amount)
        {
            var id = User.Identity.GetUserId();
            Depository depository = new Depository { idUser = id, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            financeService.depositoryRepo.add(depository);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var idUser = User.Identity.GetUserId();
            return View(financeService.depositoryRepo.get(id));
        }

        [HttpPost]
        public ActionResult Rename(string name, int id)
        {
            if (name != null || name.Trim() != "")
            {
                financeService.depositoryRepo.rename(name, id);
            }
            return RedirectToAction($"/Details/{id}", id);
        }

        [HttpGet]
        public ActionResult Count()
        {
            var idUser = User.Identity.GetUserId();
            int dep_count = financeService.depositoryRepo.count(idUser);
            int credit_count = financeService.creditRepo.count(idUser);
            return Json(new { dep_count, credit_count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var idUser = User.Identity.GetUserId();
            financeService.depositoryRepo.delete(id);
            return RedirectToAction("/List");
        }

        [HttpPost]
        public ActionResult Change(int idDepository, bool isSpending, string amountOfMoney, string comment)
        {
            var idUser = User.Identity.GetUserId();
            double amount = Double.Parse(amountOfMoney, CultureInfo.InvariantCulture);
            financeService.depositoryRepo.change(idDepository, isSpending, amount);
            financeService.operationRepo.SaveToHistory(idDepository, isSpending, amount, comment, idUser);
            return RedirectToAction($"/Details/{idDepository}", idDepository);
        }

        [HttpGet]
        public ActionResult HistoryById(int id)
        {
            List<FinanceOperation> history = financeService.operationRepo.getById(id);
            return Json(history, JsonRequestBehavior.AllowGet);
        }
    }
}