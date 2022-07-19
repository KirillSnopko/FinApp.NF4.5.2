using FinApp.Entities.Finance;
using FinApp.Entities.Identity.Account;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FinApp.Entities.Database
{
    public class UserContext : IdentityDbContext<UserApp>
    {
        public UserContext() : base("IdentityDb") { }

        static UserContext()
        {
            System.Data.Entity.Database.SetInitializer<UserContext>(new DbInitIdentity());
        }

        public static UserContext Create()
        {
            return new UserContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserApp>()
                .HasMany(u => u.depositories)
                .WithRequired(a => a.user)
                .HasForeignKey(a => a.UserId);
        }
    }
}