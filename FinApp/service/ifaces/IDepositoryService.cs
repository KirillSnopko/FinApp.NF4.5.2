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
        dynamic depositoriesByUserId(string id);
        void add(TypeDep tDep, TypeMoney tMoney, string name, double amount, string idUser);
        dynamic get(int id, string idUser);
        void rename(string name, int id, string idUser);
        int count(string idUser);
        dynamic historyById(int idDepository, string idUser);
        void delete(int id, string idUser);
        void change(int idDepository, bool isSpending, double amountOfMoney, string idUser, string comment, Category category);
    }
}
