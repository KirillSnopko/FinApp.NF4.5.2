using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.service.ifaces
{
    public interface IDepositoryService
    {
        List<Depository> depositoriesByUserId(string id);
        void add(Depository depository);
        Depository get(int id, string idUser);
        void rename(string name, int id, string idUser);
        int count(string idUser);
        List<FinanceOperation> historyById(int idDepository, string idUser);
        void delete(int id, string idUser);
        void change(int idDepository, bool isSpending, double amountOfMoney, string idUser, string comment, Category category);
    }
}
