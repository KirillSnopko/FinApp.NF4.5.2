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
            financeService.CreditRepo().delete(idCredit);
            return Json(new { status = 200 });
        }
    }
}