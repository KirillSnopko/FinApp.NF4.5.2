using FinApp.service;
using FinApp.service.ifaces;
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
            container.RegisterType<IFinanceService, FinanceService>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterInstance(typeof(HttpConfiguration), GlobalConfiguration.Configuration);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}