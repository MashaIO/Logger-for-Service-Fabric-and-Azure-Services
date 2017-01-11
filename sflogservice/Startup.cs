using System.Fabric;
using System.Web.Http;
using Logger.Base;
using Logger.ServiceFabric;
using Microsoft.Practices.Unity;
using Owin;
using Unity.WebApi;

namespace sflogservice
{
    internal class Startup : IOwinAppBuilder
    {
        private readonly StatelessServiceContext _serviceContext;

        public Startup(StatelessServiceContext serviceContext)
        {
            this._serviceContext = serviceContext;
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            
            ILoggerFactory loggerFactory = new ConcreteFactoryServiceFabricEventSource(this._serviceContext);

            var container = new UnityContainer();

            // Register your Types
            container.RegisterType<ILog, Log>(new InjectionConstructor(new InjectionParameter<ILoggerFactory>(loggerFactory)));

            config.DependencyResolver = new UnityDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
        }
    }
}
