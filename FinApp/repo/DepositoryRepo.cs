using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.Exceptions;
using FinApp.repo.ifaces;
using System.Collections.Generic;
using System.Linq;

namespace FinApp.repo
{
    public class DepositoryRepo : IDepositoryRepo
    {
        private FinContext financeContext;

        public DepositoryRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public void add(Depository depository)
        {
            financeContext.depositories.Add(depository);
            financeContext.SaveChanges();
        }


        public int count(string idUser)
        {
            return financeContext.depositories.Where(i => i.idUser == idUser).Count();
        }

        public List<Depository> depositoriesByUserId(string id)
        {
            return financeContext.depositories.Where(i => i.idUser == id).ToList();
        }

        public Depository get(int id, string idUser)
        {
            return financeContext.depositories.Where(i => i.id == id && i.idUser == idUser).First();
        }

        public void rename(string name, int id, string idUser)
        {
            financeContext.depositories.First(i => i.id == id && i.idUser == idUser).name = name;
            financeContext.SaveChanges();
        }


        /*
         * For transactions
         * 
         */

        public void delete(int id, string idUser)
        {
            financeContext.depositories.Remove(financeContext.depositories.Where(i => i.id == id && i.idUser == idUser).First());
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.depositories.ToList().RemoveAll(i => i.idUser == idUser);
            financeContext.SaveChanges();
        }

        public void change(int idDepository, bool isSpending, double amountOfMoney, string idUser)
        {
            var depository = financeContext.depositories.Where(i => i.id == idDepository && i.idUser == idUser).First();
            if (isSpending)
            {
                if (depository.amount >= amountOfMoney)
                {
                    depository.amount -= amountOfMoney;
                }
                else
                {
                    throw new FinContextException("Sorry, but you don't have enough money for this transaction in the account.");
                }
            }
            else
            {
                depository.amount += amountOfMoney;
            }
            financeContext.SaveChanges();
        }
    }
}