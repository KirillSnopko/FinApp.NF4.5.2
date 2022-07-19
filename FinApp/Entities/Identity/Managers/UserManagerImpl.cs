using FinApp.Entities.Identity.Account;
using FinApp.Entities.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;


namespace FinApp.Entities.Identity.Managers
{
    public class UserManagerImpl : UserManager<UserApp>
    {
        public UserManagerImpl(IUserStore<UserApp> store)
            : base(store)
        { }

        /*
         * For change language of error message we can impliment UserValidator/PasswordValidator
         * and override what we want
         */

        public static UserManagerImpl Create(IdentityFactoryOptions<UserManagerImpl> options,
            IOwinContext context)
        {
            UserContext db = context.Get<UserContext>();
            UserManagerImpl manager = new UserManagerImpl(new UserStore<UserApp>(db));

            manager.UserValidator = new UserValidator<UserApp>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };
            return manager;
        }
    }
}