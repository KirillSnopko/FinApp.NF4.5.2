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

        public void add(TypeDep tDep, TypeMoney tMoney, string name, double amount, string idUser)
        {
            Depository depository = new Depository { idUser = idUser, typeDep = tDep, typeMoney = tMoney, name = name, amount = amount };
            depositoryRepo.add(depository);
        }

        public dynamic depositoriesByUserId(string id)
        {
            return depositoryRepo.depositoriesByUserId(id)
                .Select(i => new { id = i.id, name = i.name, type = Enum.GetName(typeof(TypeDep), i.typeDep), value = i.amount, currency = Enum.GetName(typeof(TypeMoney), i.typeMoney) }).ToList();
        }

        public dynamic get(int id, string idUser)
        {
            Depository dep = depositoryRepo.get(id, idUser);
            var response = new { id = dep.id, name = dep.name, value = dep.amount, currency = Enum.GetName(typeof(TypeMoney), dep.typeMoney) };
            return response;
        }

        public void rename(string name, int id, string idUser)
        {
            depositoryRepo.rename(name, id, idUser);
        }

        public int count(string idUser)
        {
            return depositoryRepo.count(idUser);
        }

        public dynamic historyById(int idDepository, string idUser)
        {
            return operationRepo.getByIdDepository(idDepository, idUser)
                .Select(i => new { date = i.created.ToString("dddd, dd MMMM yyyy HH:mm:ss"), category = Enum.GetName(typeof(Category), i.category), comment = i.comment, value = i.isSpending ? ("-" + i.amountOfMoney).ToString() : ("+" + i.amountOfMoney).ToString(), status = i.isSpending }).ToList(); ;
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