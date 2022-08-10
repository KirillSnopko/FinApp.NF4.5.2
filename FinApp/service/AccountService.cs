using FinApp.Entities.Identity.Account;
using FinApp.Entities.Identity.Managers;
using FinApp.Exceptions;
using FinApp.repo;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;

namespace FinApp.service
{
    public class AccountService : IAccountService
    {
        private UserManagerImpl UserManager;
        private IAuthenticationManager AuthManager;
        private DbTransaction dbTransaction;
        private DepositoryRepo depositoryRepo;
        private CreditRepo creditRepo;
        private OperationRepo operationRepo;

        public AccountService(DbTransaction dbTransaction, OperationRepo operationRepo, CreditRepo creditRepo, DepositoryRepo depositoryRepo)
        {
            this.dbTransaction = dbTransaction;
            this.UserManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManagerImpl>();
            this.AuthManager = HttpContext.Current.GetOwinContext().Authentication;
            this.operationRepo = operationRepo;
            this.creditRepo = creditRepo;
            this.depositoryRepo = depositoryRepo;
        }

        public void changePassword(string old_password, string new_password, string userName)
        {
            UserApp user = UserManager.Find(userName, old_password);
            if (user == null)
            {
                throw new AccountServiceException("invalid current password");
            }
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(new_password);
            var result = UserManager.Update(user);
            if (!result.Succeeded)
            {
                throw new AccountServiceException("server error");
            }
        }

        public void login(string username, string password)
        {
            UserApp user = UserManager.Find(username, password);
            if (user == null)
            {
                throw new AccountServiceException("invalid name or password");
            }
            ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
        }

        public void logout()
        {
            AuthManager.SignOut();
        }

        public void register(string name, string password, string email)
        {
            UserApp user = new UserApp { UserName = name, Email = email };

            IdentityResult result = UserManager.Create(user, password);
            if (!result.Succeeded)
            {
                throw new AccountServiceException("Required \npassword: lenght = 6, uppercase, lowercase;\nlogin: allow Only Alphanumeric user names;\n email: only unique.");
            }
        }

        public void removeAccount(string password, string userName)
        {
            UserApp user = UserManager.Find(userName, password);
            if (user == null)
            {
                throw new AccountServiceException("invalid password");
            }
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    UserManager.Delete(user);
                    operationRepo.deleteAll(user.Id);
                    depositoryRepo.deleteAll(user.Id);
                    creditRepo.deleteAll(user.Id);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new AccountServiceException(ex.Message, ex);
                }
            }
        }

        public void rename(string name, string idUser)
        {
            UserApp user = UserManager.FindById(idUser);
            user.UserName = name;
            IdentityResult result = UserManager.Update(user);

            if (!result.Succeeded)
            {
                throw new AccountServiceException("Change password exception");
            }
            ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
        }
    }
}