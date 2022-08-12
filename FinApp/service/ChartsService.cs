using FinApp.service.ifaces;
using FinApp.repo.ifaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using FinApp.Entities.Finance;
using Newtonsoft.Json;

namespace FinApp.service
{
    public class ChartsService : IChartsService
    {
        private IOperationRepo operationRepo;
        private IDepositoryService depositoryService;

        public ChartsService(IOperationRepo _operationRepo, IDepositoryService depositoryService)
        {
            operationRepo = _operationRepo;
            this.depositoryService = depositoryService;
        }

        public dynamic getAddDataAllDepAllTime(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == false && i.category != Category.Credit).ToList();
            var data = financeOperations.GroupBy(i => i.idDepository).Select(i => new { Depository = depositoryService.get(i.Key, idUser).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return data;
        }

        public dynamic getAddDataAllDepCurMonth(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == false && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            var data = financeOperations.GroupBy(i => i.idDepository).Select(i => new { Depository = depositoryService.get(i.Key, idUser).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return data;
        }

        public dynamic getAddDataCurDepCurMonth(int idDepository, string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdDepository(idDepository, idUser).Where(i => i.isSpending == false && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            var data = financeOperations.GroupBy(i => i.category).Select(i => new { Category = Enum.GetName(typeof(Category), i.Key), Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return data;
        }

        public dynamic getSpendDataAllDepAllTime(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == true && i.category != Category.Credit).ToList();
            var data = financeOperations.GroupBy(i => i.idDepository).Select(i => new { Depository = depositoryService.get(i.Key, idUser).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return data;
        }

        public dynamic getSpendDataAllDepCurMonth(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == true && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            var data = financeOperations.GroupBy(i => i.idDepository).Select(i => new { Depository = depositoryService.get(i.Key, idUser).name, Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return data;
        }

        public dynamic getSpendingDataCurDepCurMonth(int idDepository, string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdDepository(idDepository, idUser).Where(i => i.isSpending == true && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            var data = financeOperations.GroupBy(i => i.category).Select(i => new { Category = Enum.GetName(typeof(Category), i.Key), Sum = i.Sum(x => x.amountOfMoney) }).ToList();
            return data;
        }
    }
}