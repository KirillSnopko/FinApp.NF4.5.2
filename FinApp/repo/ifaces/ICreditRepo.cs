using FinApp.Entities.Finance;
using System.Collections.Generic;

namespace FinApp.repo.ifaces
{
    public interface ICreditRepo
    {
        List<Credit> creditsByUserId(string id);
        void add(Credit credit);
        Credit get(int id, string idUser);
        void rename(string name, int id, string idUser);
        int count(string idUser);
        void delete(int id, string idUser);
        void deleteAll(string idUser);
        void reduce(int idCredit, double value, string idUser);
    }
}
