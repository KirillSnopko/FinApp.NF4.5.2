using FinApp.Entities.Finance;
using FinApp.service;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinApp.Controllers
{
    [Authorize]
    public class ChartsController : ApiController
    {
        private IChartsService chartsService;
        private IDepositoryService depositoryService;

        public ChartsController(IChartsService chartsService, IDepositoryService depositoryService)
        {
            this.chartsService = chartsService;
            this.depositoryService = depositoryService;
        }

        public ChartsController() { }

        [HttpGet]
        [Route("api/Charts/Spending/CurrentDepository/CurrentMonth/{idDepository}")]
        public IHttpActionResult getSpendingDataCurDepCurMonth(int idDepository)
        {
            var idUser = User.Identity.GetUserId();
            return Json(chartsService.getSpendingDataCurDepCurMonth(idDepository, idUser));
        }

        [HttpGet]
        [Route("api/Charts/Addition/CurrentDepository/CurrentMonth/{idDepository}")]
        public IHttpActionResult getAddDataCurDepCurMonth(int idDepository)
        {
            var idUser = User.Identity.GetUserId();
            return Json(chartsService.getAddDataCurDepCurMonth(idDepository, idUser));
        }

        [HttpGet]
        [Route("api/Charts/Spending/allDepository/CurMonth")]
        public IHttpActionResult getSpendDataAllDepCurMonth()
        {
            string idUser = User.Identity.GetUserId();
            return Json(chartsService.getSpendDataAllDepCurMonth(idUser));
        }

        [HttpGet]
        [Route("api/Charts/Addition/allDepository/CurMonth")]
        public IHttpActionResult getAddDataAllDepCurMonth()
        {
            string idUser = User.Identity.GetUserId();
            return Json(chartsService.getAddDataAllDepCurMonth(idUser));
        }

        [HttpGet]
        [Route("api/Charts/Spending/AllDepository/AllTime")]
        public IHttpActionResult getSpendDataAllDepAllTime()
        {
            string idUser = User.Identity.GetUserId();
            return Json(chartsService.getSpendDataAllDepAllTime(idUser));
        }

        [HttpGet]
        [Route("api/Charts/Addition/allDepository/AllTime")]
        public IHttpActionResult getAddDataAllDepAllTime()
        {
            string idUser = User.Identity.GetUserId();
            return Json(chartsService.getAddDataAllDepAllTime(idUser));
        }
    }
}
