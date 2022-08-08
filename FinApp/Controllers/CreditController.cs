using FinApp.Entities.Finance;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class CreditController : Controller
    {
        private IFinanceService financeService;
        public CreditController(IFinanceService financeService)
        {
            this.financeService = financeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var idUser = User.Identity.GetUserId();
            var credits = financeService.CreditRepo().creditsByUserId(idUser).Select(i => new { id = i.id, balanceOwed = i.balanceOwed, returned = i.returned, comment = i.comment, date1 = i.openDate.ToString("dddd, dd MMMM yyyy"), date2 = i.closeDate.ToString("dddd, dd MMMM yyyy") }).ToList();
            return Json(credits, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(double value, string comment, DateTime closeDate)
        {
            string idUser = User.Identity.GetUserId();
            Credit credit = new Credit { balanceOwed = value, returned = 0, openDate = DateTime.Now, closeDate = closeDate, comment = comment, idUser = idUser };
            financeService.CreditRepo().add(credit);
            return Json(new { status = 200 });
        }

        public ActionResult Delete(int idCredit)
        {
            var idUser = User.Identity.GetUserId();
            financeService.CreditRepo().delete(idCredit, idUser);
            financeService.OperationRepo().deleteByIdDepository(idCredit, idUser);
            return Json(new { status = 200 });
        }

        public ActionResult Reduce(int idCredit, double value, string comment)
        {
            var idUser = User.Identity.GetUserId();
            Credit credit = financeService.CreditRepo().get(idCredit, idUser);
            var diff = credit.balanceOwed - credit.returned;
            if (value <= diff)
            {
                financeService.CreditRepo().reduce(idCredit, value, idUser);
                financeService.OperationRepo().SaveToHistory(idCredit, false, value, comment, User.Identity.GetUserId(), Category.Credit);
                if (value == diff)
                {
                    credit.closeDate = DateTime.Now;
                    financeService.CreditRepo().financeContext.SaveChanges();
                }
                return Json(new { status = 200 });
            }
            if (diff == 0)
            {
                return Json(new { status = 500, message = "credit closed" });
            }
            return Json(new { status = 500, message = "invalid value" });
        }

        public ActionResult HistoryById(int id)
        {
            var idUser = User.Identity.GetUserId();
            var history = financeService.OperationRepo().getByIdDepository(id, idUser).Where(i=> i.category == Category.Credit) .Select(i => new { date = i.created.ToString("U"), comment = i.comment, value = i.amountOfMoney }).ToList();
            return Json(history, JsonRequestBehavior.AllowGet);
        }
    }
}