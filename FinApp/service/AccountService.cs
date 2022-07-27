using FinApp.Entities.Identity.Account;
using FinApp.Entities.Identity.Managers;
using FinApp.Exceptions;
using FinApp.service.ifaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace FinApp.service
{
    public class AccountService : IAccountService
    {
        private UserManagerImpl UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<UserManagerImpl>();
            }
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }


        public void changePassword(string old_password, string new_password, string userName)
        {
            UserApp user = UserManager.Find(userName, old_password);
            if (user != null)
            {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(new_password);
                var result = UserManager.Update(user);
                if (!result.Succeeded)
                {
                    throw new AccountServiceException("server error");
                }
            }
            else
            {
                throw new AccountServiceException("invalid current password");
            }
        }

        public void login(string username, string password)
        {
            UserApp user = UserManager.Find(username, password);
            if (user != null)
            {
                ClaimsIdentity ident = UserManager.CreateIdentity(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
            }
            else
            {
                throw new AccountServiceException("invalid name or password");
            }
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
                throw new AccountServiceException("Exception with new user adding");
            }
        }

        public void removeAccount(string password, string userName)
        {
            UserApp user = UserManager.Find(userName, password);
            if (user != null)
            {
                UserManager.Delete(user);
            }
            else
            {
                throw new AccountServiceException("invalid password");
            }
        }

        public void rename(string name, string idUser)
        {
            UserApp user = UserManager.FindById(idUser);
            user.UserName = name;
            IdentityResult result = UserManager.Update(user);

            if (result.Succeeded)
            {
                ClaimsIdentity ident = UserManager.CreateIdentity(user,
    DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
            }
            else
            {
                throw new AccountServiceException("Change password exception");
            }
        }
    }
}