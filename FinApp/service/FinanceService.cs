using FinApp.Entities.Database;
using FinApp.Entities.Finance;
using FinApp.repo;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace FinApp.service
{
    public class FinanceService : IFinanceService
    {
        private static FinContext financeContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<FinContext>();
            }
        }

        private DepositoryRepo depositoryRepo = new DepositoryRepo(financeContext);
        private OperationRepo operationRepo = new OperationRepo(financeContext);
        private CreditRepo creditRepo = new CreditRepo(financeContext);

        public void cleanUpAccountById(string idUser)
        {
            depositoryRepo.deleteAll(idUser);
            operationRepo.deleteAll(idUser);
            creditRepo.deleteAll(idUser);
        }

        public CreditRepo CreditRepo()
        {
            return creditRepo;
        }

        public DepositoryRepo DepositoryRepo()
        {
            return depositoryRepo;
        }

        public OperationRepo OperationRepo()
        {
            return operationRepo;
        }

    }
}