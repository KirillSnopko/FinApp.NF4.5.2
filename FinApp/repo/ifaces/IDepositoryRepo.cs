using FinApp.Entities.Finance;
using System.Collections.Generic;

namespace FinApp.repo.ifaces
{
    public interface IDepositoryRepo
    {
        List<Depository> depositoriesByUserId(string id);
        void add(Depository depository);
        Depository get(int id, string idUser);
        void rename(string name, int id, string idUser);
        int count(string idUser);
        void delete(int id, string idUser);
        void deleteAll(string idUser);
        void change(int idDepository, bool isSpending, double amountOfMoney, string idUser);
    }
}
