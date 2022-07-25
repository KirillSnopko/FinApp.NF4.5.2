using FinApp.Entities.Identity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.Entities.Finance
{
    public class Credit
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public double amountOfMoney { get; set; }
        public DateTime openDate { get; set; }
        public DateTime closeDate { get; set; }
        public string comment { get; set; }
    }
}