using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Diagnostic.EventListeners.AppService;
using Logger.AppService;
using Logger.Base;
using Microsoft.Diagnostics.EventListeners;
using Microsoft.Diagnostics.Tracing;

namespace WebApiToLog
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            const string tableStorageEventListener = "TableStorageEventListener";
            AppServiceConfigurationProvider configProvider = new AppServiceConfigurationProvider();

            TableStorageEventListener tsListener = null;
            if (configProvider.HasConfiguration)
            {
                var appServiceHealthReporter = new AppServiceHealthReporter(tableStorageEventListener);
                tsListener = new TableStorageEventListener(configProvider, appServiceHealthReporter);
            }

            var concreteFactoryAppServiceEventSource = new ConcreteFactoryAppEventSource();
            ILoggerFactory[] loggerFactories = { concreteFactoryAppServiceEventSource };


            var eventSource = concreteFactoryAppServiceEventSource.GetLogger() as EventSource;
            tsListener?.EnableEvents(eventSource, Microsoft.Diagnostics.Tracing.EventLevel.Verbose);

            // Web API configuration and services
            UnityConfig.RegisterComponents(loggerFactories);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
