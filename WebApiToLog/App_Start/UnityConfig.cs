using Microsoft.Practices.Unity;
using System.Web.Http;
using Logger.AppService;
using Logger.Base;
using Unity.WebApi;

namespace WebApiToLog
{
    public static class UnityConfig
    {
        public static void RegisterComponents(ILoggerFactory[] loggerFactories)
        {
			var container = new UnityContainer();

            // Register your Types
            container.RegisterType<ILogStrategy, LogStrategy>(new InjectionConstructor(
                            new InjectionParameter<ILoggerFactory[]>(loggerFactories)));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}