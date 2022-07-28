using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.repo.ifaces;
using System.Collections.Generic;
using System.Linq;

namespace FinApp.repo
{
    public class DepositoryRepo : IDepositoryRepo
    {
        public FinContext financeContext { get; set; }

        public DepositoryRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public void add(Depository depository)
        {
            financeContext.depositories.Add(depository);
            financeContext.SaveChanges();
        }

        public void change(int idDepository, bool isSpending, double amountOfMoney)
        {
            var depository = financeContext.depositories.Where(i => i.id == idDepository).First();
            if (isSpending)
            {
                if (depository.amount >= amountOfMoney)
                {
                    depository.amount -= amountOfMoney;
                }
            }
            else
            {
                depository.amount += amountOfMoney;
            }
            financeContext.SaveChanges();
        }

        public int count(string idUser)
        {
            return financeContext.depositories.Where(i => i.idUser == idUser).Count();
        }

        public void delete(int id)
        {
            financeContext.depositories.ToList().RemoveAt(id);
            financeContext.SaveChanges();
        }

        public List<Depository> depositoriesByUserId(string id)
        {
            return financeContext.depositories.Where(i => i.idUser == id).ToList();
        }

        public Depository get(int id)
        {
            return financeContext.depositories.Where(i => i.id == id).First();
        }

        public void rename(string name, int id)
        {
            financeContext.depositories.First(i => i.id == id).name = name;
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.depositories.ToList().RemoveAll(i => i.idUser == idUser);
            financeContext.SaveChanges();
        }
    }
}