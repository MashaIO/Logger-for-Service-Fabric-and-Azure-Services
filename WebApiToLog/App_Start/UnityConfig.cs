using Microsoft.Practices.Unity;
using System.Web.Http;
using Logger.AppService;
using Logger.Base;
using Unity.WebApi;

namespace WebApiToLog
{
    public static class UnityConfig
    {
        public static void RegisterComponents(ILogFactory loggerFactory)
        {
			var container = new UnityContainer();

            // Register your Types
            container.RegisterType<ILog, Log>(new InjectionConstructor(
                            new InjectionParameter<ILogFactory>(loggerFactory)));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}