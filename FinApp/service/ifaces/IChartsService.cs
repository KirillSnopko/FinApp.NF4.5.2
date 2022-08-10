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
        List<FinanceOperation> getSpendingDataCurDepCurMonth(int idDepository, string idUser);
        List<FinanceOperation> getAddDataCurDepCurMonth(int idDepository, string idUser);
        List<FinanceOperation> getSpendDataAllDepCurMonth(string idUser);
        List<FinanceOperation> getAddDataAllDepCurMonth(string idUser);
        List<FinanceOperation> getSpendDataAllDepAllTime(string idUser);
        List<FinanceOperation> getAddDataAllDepAllTime(string idUser);
    }
}
