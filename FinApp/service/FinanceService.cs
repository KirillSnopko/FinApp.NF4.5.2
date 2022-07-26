using FinApp.Entities.Database;
using FinApp.repo;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.service
{
    public class FinanceService
    {
        private static FinContext financeContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<FinContext>();
            }
        }

        public DepositoryRepo depositoryRepo = new DepositoryRepo(financeContext);
        public OperationRepo operationRepo = new OperationRepo(financeContext);
        public CreditRepo creditRepo = new CreditRepo(financeContext);
    }
}