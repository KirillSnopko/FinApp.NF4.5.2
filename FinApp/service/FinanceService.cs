﻿using FinApp.Entities.Database;
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

        public DepositoryRepo depositoryRepo = new DepositoryRepo(financeContext);
        public OperationRepo operationRepo = new OperationRepo(financeContext);
        public CreditRepo creditRepo = new CreditRepo(financeContext);

        public void cleanUpAccountById(string idUser)
        {
            depositoryRepo.deleteAll(idUser);
            operationRepo.deleteAll(idUser);
            creditRepo.deleteAll(idUser);
        }

        public void deleteDepository(int id, string idUser)
        {
            depositoryRepo.delete(id, idUser);
            operationRepo.deleteByIdDepository(id, idUser);
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