using FinApp.Entities.Finance;
using System.Collections.Generic;

namespace FinApp.repo
{
    public interface ICreditRepo
    {
        List<Credit> depositoriesByUserId(string id);
        void add(Credit depository);
        Credit get(int id);
        void rename(string name, int id);
        int count(string idUser);
        void delete(int id);
        void change(int idDepository, bool isSpending, double amountOfMoney);
    }
}
