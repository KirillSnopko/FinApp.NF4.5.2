using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.service.ifaces
{
    public interface ICreditService
    {
        dynamic creditsByUserId(string id);
        void add(double value, string comment, DateTime closeDate, string idUser);
        Credit get(int id, string idUser);
        void rename(string name, int id, string idUser);
        int count(string idUser);
        dynamic historyById(int idCredit, string idUser);

        //Transactions
        void reduce(int idCredit, double value, string idUser, string comment);
        void delete(int id, string idUser);
        void deleteAll(string idUser);

    }
}
