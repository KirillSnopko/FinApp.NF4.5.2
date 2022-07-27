using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.repo.ifaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.repo
{
    public class CreditRepo : ICreditRepo
    {
        public FinContext financeContext { get; set; }

        public CreditRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }
        public void add(Credit depository)
        {
            throw new NotImplementedException();
        }

        public void change(int idDepository, bool isSpending, double amountOfMoney)
        {
            throw new NotImplementedException();
        }

        public int count(string idUser)
        {
           return financeContext.credits.Where(i => i.idUser == idUser).Count();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Credit> depositoriesByUserId(string id)
        {
            throw new NotImplementedException();
        }

        public Credit get(int id)
        {
            throw new NotImplementedException();
        }

        public void rename(string name, int id)
        {
            throw new NotImplementedException();
        }

        public void deleteAll(string idUser)
        {
            throw new NotImplementedException();
        }
    }
}