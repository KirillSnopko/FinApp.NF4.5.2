using FinApp.Entities.Finance;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.Entities.Identity.Account
{
    public class UserApp : IdentityUser
    {
        public ICollection<Credit> credits { get; set; }
        public virtual ICollection<Depository> depositories { get; set; }
        public ICollection<FinanceOperation> operations { get; set; }

        public UserApp()
        {
            credits = new List<Credit>();
            depositories = new List<Depository>();
            operations = new List<FinanceOperation>();
        }
    }
}