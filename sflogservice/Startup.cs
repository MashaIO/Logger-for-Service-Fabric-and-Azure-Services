using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Logger.Base;
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

        public void Configuration(IAppBuilder appBuilder, ILog log)
        {
            HttpConfiguration config = new HttpConfiguration();
            var container = new UnityContainer();
            container.BuildUp(typeof(ILog), log);
            config.DependencyResolver = new UnityDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
        }
    }
}
