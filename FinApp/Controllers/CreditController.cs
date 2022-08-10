using FinApp.Entities.Finance;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FinApp.Controllers
{
    [Authorize]
    public class CreditController : Controller
    {
        private ICreditService creditService;
        public CreditController(ICreditService creditService)
        {
            this.creditService = creditService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var idUser = User.Identity.GetUserId();
            var credits = creditService.creditsByUserId(idUser);
            var response = credits.Select(i => new { id = i.id, balanceOwed = i.balanceOwed, returned = i.returned, comment = i.comment, date1 = i.openDate.ToString("dddd, dd MMMM yyyy"), date2 = i.closeDate.ToString("dddd, dd MMMM yyyy") }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(double value, string comment, DateTime closeDate)
        {
            string idUser = User.Identity.GetUserId();
            Credit credit = new Credit { balanceOwed = value, returned = 0, openDate = DateTime.Now, closeDate = closeDate, comment = comment, idUser = idUser };
            creditService.add(credit);
            return Json(new { status = 200 });
        }

        public ActionResult Delete(int idCredit)
        {
            var idUser = User.Identity.GetUserId();
            creditService.delete(idCredit, idUser);
            return Json(new { status = 200 });
        }

        public ActionResult Reduce(int idCredit, double value, string comment)
        {
            var idUser = User.Identity.GetUserId();
            creditService.reduce(idCredit, value, idUser, comment);
            return Json(new { status = 200 });
        }


        public ActionResult HistoryById(int id)
        {
            var idUser = User.Identity.GetUserId();
            var history = creditService.historyById(id, idUser);
            var response = history.Select(i => new { date = i.created.ToString("U"), comment = i.comment, value = i.amountOfMoney }).ToList();
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