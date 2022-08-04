﻿using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using FinApp.repo.ifaces;

namespace FinApp.repo
{
    public class OperationRepo : IOperationRepo
    {
        public FinContext financeContext { get; set; }

        public OperationRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public List<FinanceOperation> getByIdDepository(int idDepository)
        {
            return financeContext.operations.Where(i => i.idDepository == idDepository).ToList();
        }

        public List<FinanceOperation> getByIdUser(string idUser)
        {
            return financeContext.operations.Where(i => i.idUser == idUser).ToList();
        }

        public void SaveToHistory(int idDepository, bool isSpending, double amountOfMoney, string comment, string idUser, Category category)
        {
            FinanceOperation operation = new FinanceOperation { idDepository = idDepository, amountOfMoney = amountOfMoney, comment = comment, isSpending = isSpending, idUser = idUser, created = DateTime.Now, category = category };
            financeContext.operations.Add(operation);
            financeContext.SaveChanges();
        }

        public void delete(int idOperation)
        {
            financeContext.operations.ToList().RemoveAt(idOperation);
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.operations.RemoveRange(financeContext.operations.Where(i => i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }

        public void deleteByIdDepository(int idDepository)
        {
            financeContext.operations.RemoveRange(financeContext.operations.Where(i=>i.idDepository==idDepository).ToList());
            financeContext.SaveChanges();
        }
    }
}