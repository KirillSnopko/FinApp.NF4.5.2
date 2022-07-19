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
        public UserApp user { get; set; }
        public int amountOfMoney { get; set; }
        public DateTime openDate { get; set; }
        public DateTime closeDate { get; set; }
        public string comment { get; set; }
    }
}