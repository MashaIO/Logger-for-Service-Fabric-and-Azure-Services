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
            var configProvider = new AppServiceConfigurationProvider();

            TableStorageEventListener tsListener = null;
            if (configProvider.HasConfiguration)
            {
                var appServiceHealthReporter = new AppServiceHealthReporter(tableStorageEventListener);
                tsListener = new TableStorageEventListener(configProvider, appServiceHealthReporter);
            }

            ILogFactory loggerFactory = new AppServiceLogFactory();

            var eventSource = loggerFactory.GetLogger() as EventSource;
            tsListener?.EnableEvents(eventSource, EventLevel.Verbose);

            // Web API configuration and services
            UnityConfig.RegisterComponents(loggerFactory);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
