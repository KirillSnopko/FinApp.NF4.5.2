using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace FinApp.repo
{
    public class OperationRepo : IOperationRepo
    {
        public FinContext financeContext { get; set; }

        public OperationRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public List<FinanceOperation> getById(int idDepository)
        {
            return financeContext.operations.Where(i => i.idDepository == idDepository).ToList();
        }

        public void SaveToHistory(int idDepository, bool isSpending, double amountOfMoney, string comment, string idUser)
        {
            FinanceOperation operation = new FinanceOperation { idDepository = idDepository, amountOfMoney = amountOfMoney, comment = comment, isSpending = isSpending, idUser = idUser, created = DateTime.Now };
            financeContext.operations.Add(operation);
            financeContext.SaveChanges();
        }
    }
}