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
            var response = depositoryService.depositoriesByUserId(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var idUser = User.Identity.GetUserId();
            var response = depositoryService.get(id, idUser);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeDep tDep, TypeMoney tMoney, string name, double amount)
        {
            var id = User.Identity.GetUserId();
            depositoryService.add(tDep, tMoney, name, amount, id);
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
            var response = depositoryService.historyById(id, idUser);
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