using FinApp.Entities.Finance;
using FinApp.service;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace FinApp.Controllers
{
    [Authorize]
    public class DepositoryController : Controller
    {
        private IFinanceService financeService;

        public DepositoryController(IFinanceService financeService)
        {
            this.financeService = financeService;
        }

        [HttpGet]
        public ActionResult List()
        {
            var id = User.Identity.GetUserId();
            return View(financeService.DepositoryRepo().depositoriesByUserId(id));
        }

        [HttpPost]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, double amount)
        {
            var id = User.Identity.GetUserId();
            Depository depository = new Depository { idUser = id, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            financeService.DepositoryRepo().add(depository);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var idUser = User.Identity.GetUserId();
            return View(financeService.DepositoryRepo().get(id));
        }

        [HttpPost]
        public ActionResult Rename(string name, int id)
        {
            if (name != null || name.Trim() != "")
            {
                financeService.DepositoryRepo().rename(name, id);
            }
            return RedirectToAction($"/Details/{id}", id);
        }

        [HttpGet]
        public ActionResult Count()
        {
            var idUser = User.Identity.GetUserId();
            int dep_count = financeService.DepositoryRepo().count(idUser);
            int credit_count = financeService.CreditRepo().count(idUser);
            return Json(new { dep_count, credit_count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var idUser = User.Identity.GetUserId();
            financeService.DepositoryRepo().delete(id);
            financeService.OperationRepo().deleteByIdDepository(id);
            return RedirectToAction("/List");
        }

        [HttpPost]
        public ActionResult Change(int idDepository, bool isSpending, string amountOfMoney, string comment, Category category)
        {
            var idUser = User.Identity.GetUserId();
            double amount = Double.Parse(amountOfMoney, CultureInfo.InvariantCulture);
            financeService.DepositoryRepo().change(idDepository, isSpending, amount);
            financeService.OperationRepo().SaveToHistory(idDepository, isSpending, amount, comment, idUser, category);
            return RedirectToAction($"/Details/{idDepository}", idDepository);
        }

        [HttpGet]
        public ActionResult HistoryById(int id)
        {
            var history = financeService.OperationRepo().getByIdDepository(id).Select(i => new { date = i.created.ToString("dddd, dd MMMM yyyy HH:mm:ss"), category = Enum.GetName(typeof(Category), i.category), comment = i.comment, value = i.isSpending?("-" + i.amountOfMoney).ToString(): ("+" + i.amountOfMoney).ToString(), status = i.isSpending }).ToList();
            return Json(history, JsonRequestBehavior.AllowGet);
        }
    }
}