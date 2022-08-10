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
using FinApp.Exceptions;

namespace FinApp.Controllers
{
    [Authorize]
    public class DepositoryController : Controller
    {
        private IDepositoryService depositoryService;
        private ICreditService creditService;

        public DepositoryController(IDepositoryService depositoryService, ICreditService creditService)
        {
            this.depositoryService = depositoryService;
            this.creditService = creditService;
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
            var data = depositoryService.depositoriesByUserId(id);
            var response = data.Select(i => new { id = i.id, name = i.name, type = Enum.GetName(typeof(TypeDep), i.typeDep), value = i.amount, currency = Enum.GetName(typeof(TypeMoney), i.typeMoney) }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var idUser = User.Identity.GetUserId();
            Depository dep = depositoryService.get(id, idUser);
            var response = new { id = dep.id, name = dep.name, value = dep.amount, currency = Enum.GetName(typeof(TypeMoney), dep.typeMoney) };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, double amount)
        {
            var id = User.Identity.GetUserId();
            Depository depository = new Depository { idUser = id, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            depositoryService.add(depository);
            return Json(new { status = 200 });
        }

        [HttpGet]

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string name, int id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { status = 500, message = "invalid value" });
            }
            var idUser = User.Identity.GetUserId();
            depositoryService.rename(name, id, idUser);
            return Json(new { status = 200 });
        }

        [HttpGet]
        public ActionResult Count()
        {
            var idUser = User.Identity.GetUserId();
            int dep_count = depositoryService.count(idUser);
            int credit_count = creditService.count(idUser);
            return Json(new { dep_count, credit_count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var idUser = User.Identity.GetUserId();
            depositoryService.delete(id, idUser);
            return Json(new { status = 200 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(int idDepository, bool isSpending, string amountOfMoney, string comment, Category category)
        {
            var idUser = User.Identity.GetUserId();
            double amount = Double.Parse(amountOfMoney, CultureInfo.InvariantCulture);
            depositoryService.change(idDepository, isSpending, amount, idUser, comment, category);
            return Json(new { status = 200 });
        }

        [HttpGet]
        public ActionResult HistoryById(int id)
        {
            var idUser = User.Identity.GetUserId();
            var history = depositoryService.historyById(id, idUser);
            var response = history.Select(i => new { date = i.created.ToString("dddd, dd MMMM yyyy HH:mm:ss"), category = Enum.GetName(typeof(Category), i.category), comment = i.comment, value = i.isSpending ? ("-" + i.amountOfMoney).ToString() : ("+" + i.amountOfMoney).ToString(), status = i.isSpending }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                var response = new { status = 500, message = filterContext.Exception.Message };
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