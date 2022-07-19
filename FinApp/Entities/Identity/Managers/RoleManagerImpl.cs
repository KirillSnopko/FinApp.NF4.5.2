using FinApp.Entities.Identity.Account;
using FinApp.Entities.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;


namespace FinApp.Entities.Identity.Managers
{
    public class RoleManagerImpl : RoleManager<Role>, IDisposable
    {
        public RoleManagerImpl(RoleStore<Role> store)
            : base(store)
        { }

        public static RoleManagerImpl Create(IdentityFactoryOptions<RoleManagerImpl> options,
            IOwinContext context)
        {
            return new RoleManagerImpl(new
                RoleStore<Role>(context.Get<UserContext>()));
        }
    }
}