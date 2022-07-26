using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;

namespace FinApp.repo
{
    public interface IDepositoryRepo
    {
        List<Depository> depositoriesByUserId(string id);
        void add(Depository depository);

        Depository get(int id);
        void rename(string name, int id);

        int count(string idUser);

        void delete(int id);

        void change(int idDepository, bool isSpending, double amountOfMoney);
    }
}
