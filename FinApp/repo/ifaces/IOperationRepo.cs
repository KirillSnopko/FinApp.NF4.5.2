using FinApp.Entities.Finance;
using System.Collections.Generic;

namespace FinApp.repo.ifaces
{
    public interface IOperationRepo
    {
        void SaveToHistory(int idDepository, bool isSpending, double amountOfMoney, string comment, string idUser, Category category);
        List<FinanceOperation> getByIdDepository(int idDepository);
        void delete(int idOperation);
        void deleteAll(string idUser);
        void deleteByIdDepository(int idDepository);
        List<FinanceOperation> getByIdUser(string idUser);
    }
}
