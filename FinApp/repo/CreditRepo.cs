using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.repo.ifaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinApp.repo
{
    public class CreditRepo : ICreditRepo
    {
        public FinContext financeContext { get; set; }

        public CreditRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }
        public void add(Credit credit)
        {
            financeContext.credits.Add(credit);
            financeContext.SaveChanges();
        }

        public void reduce(int idCredit, double value, string idUser)
        {
            Credit credit = financeContext.credits.Where(i => i.id == idCredit&&i.idUser==idUser).First();
            credit.returned += value;
            financeContext.SaveChanges();
        }

        public int count(string idUser)
        {
            return financeContext.credits.Where(i => i.idUser == idUser).Count();
        }

        public void delete(int id, string idUser)
        {
            financeContext.credits.Remove(financeContext.credits.Where(i => i.id == id&&i.idUser==idUser).First());
            financeContext.SaveChanges();
        }

        public List<Credit> creditsByUserId(string id)
        {
            return financeContext.credits.Where(i => i.idUser == id).ToList();
        }

        public Credit get(int id, string idUser)
        {
            return financeContext.credits.Where(i => i.id == id&&i.idUser==idUser).First();
        }

        public void rename(string name, int id, string idUser)
        {
            financeContext.credits.Where(i => i.id == id&&i.idUser==idUser).First().comment = name;
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.credits.RemoveRange(financeContext.credits.Where(i => i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }
    }
}