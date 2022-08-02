using FinApp.Controllers;
using FinApp.Entities.Database;
using FinApp.service;
using FinApp.service.ifaces;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace FinApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            //logger
            ILog loggerAccount = LogManager.GetLogger(typeof(AccountController));
            container.RegisterInstance<ILog>(loggerAccount);
            
            //services
            container.RegisterType<IFinanceService, FinanceService>();
            container.RegisterType<IAccountService, AccountService>();

            //for Api controller
            container.RegisterInstance(typeof(HttpConfiguration), GlobalConfiguration.Configuration);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}