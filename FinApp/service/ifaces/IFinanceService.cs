using FinApp.repo;

namespace FinApp.service.ifaces
{
    public interface IFinanceService
    {
        void cleanUpAccountById(string id);
        CreditRepo CreditRepo();
        DepositoryRepo DepositoryRepo();
        OperationRepo OperationRepo();
    }
}
