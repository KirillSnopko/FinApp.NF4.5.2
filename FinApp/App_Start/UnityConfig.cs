using FinApp.Controllers;
using FinApp.Entities.Database;
using FinApp.repo;
using FinApp.repo.ifaces;
using FinApp.service;
using FinApp.service.ifaces;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
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

            //context, transaction
            container.RegisterType<DbContext, FinContext>();
            FinContext finContext = container.Resolve<FinContext>();

            DbTransaction transaction = new DbTransaction(finContext);
            container.RegisterInstance(transaction);

            //logger
            ILog loggerAccount = LogManager.GetLogger(typeof(AccountController));
            container.RegisterInstance<ILog>(loggerAccount);

            //repo
            container.RegisterType<ICreditRepo, CreditRepo>();
            container.RegisterType<IDepositoryRepo, DepositoryRepo>();
            container.RegisterType<IOperationRepo, OperationRepo>();

            //services
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<ICreditService, CreditService>();
            container.RegisterType<IDepositoryService, DepositoryService>();
            container.RegisterType<IChartsService, ChartsService>();

            //for Api controller
            container.RegisterInstance(typeof(HttpConfiguration), GlobalConfiguration.Configuration);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}