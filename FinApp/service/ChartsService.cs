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

        public ChartsService(IOperationRepo _operationRepo)
        {
            operationRepo = _operationRepo;
        }

        public List<FinanceOperation> getAddDataAllDepAllTime(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == false && i.category != Category.Credit).ToList();
            return financeOperations;
        }

        public List<FinanceOperation> getAddDataAllDepCurMonth(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == false && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            return financeOperations;
        }

        public List<FinanceOperation> getAddDataCurDepCurMonth(int idDepository, string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdDepository(idDepository, idUser).Where(i => i.isSpending == false && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            return financeOperations;
        }

        public List<FinanceOperation> getSpendDataAllDepAllTime(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == true && i.category != Category.Credit).ToList();
            return financeOperations;
        }

        public List<FinanceOperation> getSpendDataAllDepCurMonth(string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdUser(idUser).Where(i => i.isSpending == true && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            return financeOperations;
        }

        public List<FinanceOperation> getSpendingDataCurDepCurMonth(int idDepository, string idUser)
        {
            List<FinanceOperation> financeOperations = operationRepo.getByIdDepository(idDepository, idUser).Where(i => i.isSpending == true && i.category != Category.Credit).Where(i => i.created.Month == DateTime.Now.Month).ToList();
            return financeOperations;
        }
    }
}