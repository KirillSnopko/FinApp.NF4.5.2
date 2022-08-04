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
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            var id = User.Identity.GetUserId();
            var data = financeService.DepositoryRepo().depositoriesByUserId(id).Select(i => new { id = i.id, name = i.name, type = Enum.GetName(typeof(TypeDep), i.typeDep), value = i.amount, currency = Enum.GetName(typeof(TypeMoney), i.typeMoney) }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            Depository dep = financeService.DepositoryRepo().get(id);
            var data = new { id = dep.id, name = dep.name, value = dep.amount, currency = Enum.GetName(typeof(TypeMoney), dep.typeMoney) };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, double amount)
        {
            var id = User.Identity.GetUserId();
            Depository depository = new Depository { idUser = id, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            financeService.DepositoryRepo().add(depository);
            return Json(new { status = 200 });
        }

        [HttpGet]

        public ActionResult Details(int id)
        {
            var idUser = User.Identity.GetUserId();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string name, int id)
        {
            if (name != null || name.Trim() != "")
            {
                financeService.DepositoryRepo().rename(name, id);
                return Json(new { status = 200 });
            }
            return Json(new { status = 500, message = "invalid value" });
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            financeService.deleteDepository(id);
            return Json(new { status = 200 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(int idDepository, bool isSpending, string amountOfMoney, string comment, Category category)
        {
            var idUser = User.Identity.GetUserId();
            double value = financeService.DepositoryRepo().get(idDepository).amount;
            double amount = Double.Parse(amountOfMoney, CultureInfo.InvariantCulture);
            if (value >= amount)
            {
                financeService.DepositoryRepo().change(idDepository, isSpending, amount);
                financeService.OperationRepo().SaveToHistory(idDepository, isSpending, amount, comment, idUser, category);
                return Json(new { status = 200 });
            }
            return Json(new { status = 500, message = $"Short of money ({value}<{amount}" });
        }

        [HttpGet]
        public ActionResult HistoryById(int id)
        {
            var history = financeService.OperationRepo().getByIdDepository(id).Select(i => new { date = i.created.ToString("dddd, dd MMMM yyyy HH:mm:ss"), category = Enum.GetName(typeof(Category), i.category), comment = i.comment, value = i.isSpending ? ("-" + i.amountOfMoney).ToString() : ("+" + i.amountOfMoney).ToString(), status = i.isSpending }).ToList();
            return Json(history, JsonRequestBehavior.AllowGet);
        }
    }
}