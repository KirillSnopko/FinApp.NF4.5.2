using FinApp.Entities.Finance;
using FinApp.Exceptions;
using FinApp.repo;
using FinApp.repo.ifaces;
using FinApp.service.ifaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.service
{
    public class DepositoryService : IDepositoryService
    {
        private IDepositoryRepo depositoryRepo;
        private IOperationRepo operationRepo;
        private DbTransaction dbTransaction;

        public DepositoryService(IDepositoryRepo depositoryRepo, DbTransaction dbTransaction, IOperationRepo operationRepo)
        {
            this.depositoryRepo = depositoryRepo;
            this.dbTransaction = dbTransaction;
            this.operationRepo = operationRepo;
        }

        public void add(Depository depository)
        {
            depositoryRepo.add(depository);
        }

        public List<Depository> depositoriesByUserId(string id)
        {
            return depositoryRepo.depositoriesByUserId(id);
        }

        public Depository get(int id, string idUser)
        {
            return depositoryRepo.get(id, idUser);
        }

        public void rename(string name, int id, string idUser)
        {
            depositoryRepo.rename(name, id, idUser);
        }

        public int count(string idUser)
        {
            return depositoryRepo.count(idUser);
        }

        public List<FinanceOperation> historyById(int idDepository, string idUser)
        {
            return operationRepo.getByIdDepository(idDepository, idUser);
        }

        // Transactions

        public void change(int idDepository, bool isSpending, double amountOfMoney, string idUser, string comment, Category category)
        {
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    depositoryRepo.change(idDepository, isSpending, amountOfMoney, idUser);
                    operationRepo.SaveToHistory(idDepository, isSpending, amountOfMoney, comment, idUser, category);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
                finally
                {
                    dbTransaction.Dispose();
                }
            }
        }

        public void delete(int id, string idUser)
        {
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    depositoryRepo.delete(id, idUser);
                    operationRepo.deleteByIdDepository(id, idUser);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
                finally { dbTransaction.Dispose(); }
            }
        }
    }
}