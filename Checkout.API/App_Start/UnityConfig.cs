using System.Web.Http;
using Unity;
using Unity.WebApi;
using Checkout.API.Services;
using Checkout.API.Helper;

namespace Checkout.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IBankTransactionService, BankTransactionService>();
            container.RegisterType<IPaymentsRecordService, PaymentsRecordService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}