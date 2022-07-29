using FinApp.Entities.Finance;
using FinApp.service;
using FinApp.service.ifaces;
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
        private readonly IFinanceService financeService;

        public ChartsController(IFinanceService financeService)
        {
            this.financeService = financeService;
        }

        public ChartsController() { }

        [HttpGet]
        [Route("api/Charts/Doughnut/CurrentMonth/{idDepository}")]
        public IHttpActionResult Doughnut(int idDepository)
        {
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getById(idDepository);
            var data = financeOperations.Where(i => i.created.Month == DateTime.Now.Month).GroupBy(i => i.category).Select(i => new { Category = Enum.GetName(typeof(Category), i.Key), Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }

    }
}
