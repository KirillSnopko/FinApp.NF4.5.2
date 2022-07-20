using FinApp.Entities.Identity.Account;
using FinApp.Entities.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinApp.Entities.Database
{
    public class DbInitIdentity :DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(UserContext context)
        {
            UserManagerImpl userMgr = new UserManagerImpl(new UserStore<UserApp>(context));
            RoleManagerImpl roleMgr = new RoleManagerImpl(new RoleStore<Role>(context));

            string roleName = "Admin";
            string userName = "Admin";
            string password = "Admin123";
            string email = "admin@gmail.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new Role(roleName));
            }

            UserApp user = userMgr.FindByName(userName);
            if (user == null)
            {
                UserApp admin = new UserApp { UserName = userName, Email = email };
                var result = userMgr.Create(admin, password);
                if (result.Succeeded)
                {
                    userMgr.AddToRole(userMgr.FindByName(userName).Id, roleName);
                }
            }
        }
    }
}