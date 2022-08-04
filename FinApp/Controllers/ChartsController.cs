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
        private readonly IFinanceService financeService;

        public ChartsController(IFinanceService financeService)
        {
            this.financeService = financeService;
        }

        public ChartsController() { }

        [HttpGet]
        [Route("api/Charts/Spending/CurrentDepository/CurrentMonth/{idDepository}")]
        public IHttpActionResult getSpendingDataCurDepCurMonth(int idDepository)
        {
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getByIdDepository(idDepository).Where(i => i.isSpending == true && i.category != Category.Credit).ToList();
            var data = financeOperations.Where(i => i.created.Month == DateTime.Now.Month).GroupBy(i => i.category).Select(i => new { Category = Enum.GetName(typeof(Category), i.Key), Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }

        [HttpGet]
        [Route("api/Charts/Addition/CurrentDepository/CurrentMonth/{idDepository}")]
        public IHttpActionResult getAddDataCurDepCurMonth(int idDepository)
        {
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getByIdDepository(idDepository).Where(i => i.isSpending == false && i.category != Category.Credit).ToList();
            var data = financeOperations.Where(i => i.created.Month == DateTime.Now.Month).GroupBy(i => i.category).Select(i => new { Category = Enum.GetName(typeof(Category), i.Key), Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }

        [HttpGet]
        [Route("api/Charts/Spending/allDepository/CurMonth")]
        public IHttpActionResult getSpendDataAllDepCurMonth()
        {
            string idUser = User.Identity.GetUserId();
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getByIdUser(idUser).Where(i => i.isSpending == true && i.category != Category.Credit).ToList();
            var data = financeOperations.Where(i => i.created.Month == DateTime.Now.Month).GroupBy(i => i.idDepository).Select(i => new { Depository = financeService.DepositoryRepo().get(i.Key).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }

        [HttpGet]
        [Route("api/Charts/Addition/allDepository/CurMonth")]
        public IHttpActionResult getAddDataAllDepCurMonth()
        {
            string idUser = User.Identity.GetUserId();
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getByIdUser(idUser).Where(i => i.isSpending == false && i.category != Category.Credit).ToList();
            var data = financeOperations.Where(i => i.created.Month == DateTime.Now.Month).GroupBy(i => i.idDepository).Select(i => new { Depository = financeService.DepositoryRepo().get(i.Key).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }

        [HttpGet]
        [Route("api/Charts/Spending/AllDepository/AllTime")]
        public IHttpActionResult getSpendDataAllDepAllTime()
        {
            string idUser = User.Identity.GetUserId();
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getByIdUser(idUser).Where(i => i.isSpending == true && i.category != Category.Credit).ToList();
            var data = financeOperations.GroupBy(i => i.idDepository).Select(i => new { Depository = financeService.DepositoryRepo().get(i.Key).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }

        [HttpGet]
        [Route("api/Charts/Addition/allDepository/AllTime")]
        public IHttpActionResult getAddDataAllDepAllTime()
        {
            string idUser = User.Identity.GetUserId();
            List<FinanceOperation> financeOperations = financeService.OperationRepo().getByIdUser(idUser).Where(i => i.isSpending == false && i.category != Category.Credit).ToList();
            var data = financeOperations.GroupBy(i => i.idDepository).Select(i => new { Depository = financeService.DepositoryRepo().get(i.Key).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return Json(data);
        }
    }
}
