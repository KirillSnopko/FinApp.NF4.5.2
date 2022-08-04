﻿using FinApp.repo;
using System;
using System.Collections.Generic;

namespace FinApp.service.ifaces
{
    public interface IFinanceService
    {
        void cleanUpAccountById(string id);
        void deleteDepository(int id);
        CreditRepo CreditRepo();
        DepositoryRepo DepositoryRepo();
        OperationRepo OperationRepo();
    }
}
