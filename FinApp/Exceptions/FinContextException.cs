using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.Exceptions
{
    public class FinContextException : Exception
    {
        public FinContextException(string message, Exception innerException) : base(message, innerException) { }
        public FinContextException(string message) : base(message) { }
    }
}