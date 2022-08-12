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
            var response = creditService.creditsByUserId(idUser);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(double value, string comment, DateTime closeDate)
        {
            string idUser = User.Identity.GetUserId();
            creditService.add(value, comment, closeDate, idUser);
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
            var response = creditService.historyById(id, idUser);
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