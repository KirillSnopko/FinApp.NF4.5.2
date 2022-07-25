using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinApp.services
{
    public interface IOperationService
    {
        void SaveToHistory(int idDepository, bool isSpending, double amountOfMoney, string comment, string idUser);
        List<FinanceOperation> getById(int idDepository);

    }
}
