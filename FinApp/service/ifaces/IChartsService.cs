using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.service.ifaces
{
    public interface IChartsService
    {
        dynamic getSpendingDataCurDepCurMonth(int idDepository, string idUser);
        dynamic getAddDataCurDepCurMonth(int idDepository, string idUser);
        dynamic getSpendDataAllDepCurMonth(string idUser);
        dynamic getAddDataAllDepCurMonth(string idUser);
        dynamic getSpendDataAllDepAllTime(string idUser);
        dynamic getAddDataAllDepAllTime(string idUser);
    }
}
