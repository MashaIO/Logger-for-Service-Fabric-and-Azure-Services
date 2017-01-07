using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Logger.Base;
using Logger.Infrastructure;
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

            var concreteFactoryServiceEventSource = new ConcreteFactoryServiceEventSource(this._serviceContext);
            ILoggerFactory[] loggerFactories = { concreteFactoryServiceEventSource };
            
            var container = new UnityContainer();

            // Register your Types
            container.RegisterType<ILogStrategy, LogStrategy>(new InjectionConstructor(
        new InjectionParameter<ILoggerFactory[]>(loggerFactories)));

            config.DependencyResolver = new UnityDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
        }
    }
}
