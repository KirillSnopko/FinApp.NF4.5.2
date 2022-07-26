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
        private FinContext financeContext;

        public OperationRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public List<FinanceOperation> getByIdDepository(int idDepository, string idUser)
        {
            return financeContext.operations.Where(i => i.idDepository == idDepository && i.idUser == idUser).ToList();
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

        public void delete(int idOperation, string idUser)
        {
            FinanceOperation fp = financeContext.operations.Where(i => i.id == idOperation && i.idUser == idUser).First();
            financeContext.operations.Remove(fp);
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.operations.RemoveRange(financeContext.operations.Where(i => i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }

        public void deleteByIdDepository(int idDepository, string idUser)
        {
            financeContext.operations.RemoveRange(financeContext.operations.Where(i => i.idDepository == idDepository && i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }
    }
}