using FinApp.Entities.Identity.Account;
using Microsoft.AspNet.Identity.EntityFramework;

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
    }
}