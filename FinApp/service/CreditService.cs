using FinApp.Entities.Finance;
using FinApp.Exceptions;
using FinApp.repo;
using FinApp.repo.ifaces;
using FinApp.service.ifaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinApp.service
{
    public class CreditService : ICreditService
    {
        private ICreditRepo creditRepo;
        private IOperationRepo operationRepo;
        private DbTransaction dbTransaction;

        public CreditService(ICreditRepo creditRepo, IOperationRepo operationRepo, DbTransaction dbTransaction)
        {
            this.creditRepo = creditRepo;
            this.operationRepo = operationRepo;
            this.dbTransaction = dbTransaction;
        }

        public void add(Credit credit)
        {
            creditRepo.add(credit);
        }

        public int count(string idUser)
        {
            return creditRepo.count(idUser);
        }

        public List<Credit> creditsByUserId(string id)
        {
            return creditRepo.creditsByUserId(id);
        }

        public Credit get(int id, string idUser)
        {
            return creditRepo.get(id, idUser);
        }

        public void rename(string name, int id, string idUser)
        {
            creditRepo.rename(name, id, idUser);
        }

        public List<FinanceOperation> historyById(int idCredit, string idUser)
        {
            return operationRepo.getByIdDepository(idCredit, idUser).Where(i => i.category == Category.Credit).ToList();
        }


        /*
         * Transactions
         */
        public void reduce(int idCredit, double value, string idUser, string comment)
        {
            Credit credit = creditRepo.get(idCredit, idUser);
            var diff = credit.balanceOwed - credit.returned;
            if (diff == 0)
            {
                throw new FinContextException("credit closed");
            }
            if (value > diff)
            {
                throw new FinContextException("value more then credit balance");
            }
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    creditRepo.reduce(idCredit, value, idUser);
                    operationRepo.SaveToHistory(idCredit, false, value, comment, idUser, Category.Credit);
                    if (value == diff)
                    {
                        creditRepo.closeCredit(credit);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
            }
        }

        public void delete(int id, string idUser)
        {
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    creditRepo.delete(id, idUser);
                    operationRepo.deleteByIdDepository(id, idUser);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
            }
        }

        public void deleteAll(string idUser)
        {
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    creditRepo.deleteAll(idUser);
                    operationRepo.deleteAll(idUser);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
            }
        }
    }
}