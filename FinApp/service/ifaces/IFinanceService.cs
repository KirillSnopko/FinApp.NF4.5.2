using FinApp.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
