using FinApp.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinApp.Entities.Database
{
    public class FinContext : DbContext
    {
        public FinContext() : base("FinContext")
        {
            System.Data.Entity.Database.SetInitializer<FinContext>(new DropCreateDatabaseIfModelChanges<FinContext>());
        }

        public DbSet<Credit> credits { get; set; }
        public DbSet<Depository> depositories { get; set; }
        public DbSet<FinanceOperation> operations { get; set; }
    }
}