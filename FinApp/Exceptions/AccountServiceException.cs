﻿using System;

namespace FinApp.Exceptions
{
    public class AccountServiceException:Exception
    {
        public AccountServiceException(string message, Exception innerException) : base(message, innerException) { }
        public AccountServiceException(string message) : base(message) { }
    }
}