# AzureSerficeFabricAndAppServiceLogger
Diagnositics for Service Fabric(ETW) and App Service(Tablestorage provider extendable)

The idea of this application is to have a common logger implmentation for both Service Fabric and App Service.

Also we can achieve to use the same Table storage(log table) for n number of Service Fabric and n number of App Service.

I have tried my best to decouple the depedencies for both testability and adaptability

This sample application will show how to log service fabric application with ETW which will finally log into table storage and also log azure services with event source again loged to table storage

# Patterns and Features
1. Strategy and factory pattern implemented to decouple appservice event source and service fabric event source
2. ARM template to create/associate table storage to service fabric.
3. Custom event added to App service(Table storage event listnere) and service fabric(ETW).
4. Extendable provider added for app service so that provider can be changed as per requirement.
5. BufferEventlistner will help in listen to events and log it to the table storage.
6. In test folder i tried to show case how to use the logger in both Service fabric and app service.

# Implementation

# Service Fabric

In your Startup you can see how logger is configured. By this we can say it is an decoupled one which will help in both testing and adaptability.

            // Creating a concrete service fabric event source 
            var concreteFactoryServiceEventSource = new ConcreteFactoryServiceEventSource(this._serviceContext);
            
            // Configure the required factories to the array(In our case this is service fabric so that Service fabric event source more than enough)
            ILoggerFactory[] loggerFactories = { concreteFactoryServiceEventSource };
            
            var container = new UnityContainer();
          
            // Register your Types
            container.RegisterType<ILogStrategy, LogStrategy>(new InjectionConstructor(
                            new InjectionParameter<ILoggerFactory[]>(loggerFactories)));

            config.DependencyResolver = new UnityDependencyResolver(container);

Testing the log(HelloContoller.cs)

        private readonly ILogStrategy _log;

        public HelloController(ILogStrategy log)
        {
            _log = log;
        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage GetAsync()
        {
             // Logs to ETW.
            _log.CreateLog(typeof(ConcreteFactoryServiceEventSource)).LogVerbose("Log Verbose");
            _log.CreateLog(typeof(ConcreteFactoryServiceEventSource)).LogCritical(("Log Critical"));
            _log.CreateLog(typeof(ConcreteFactoryServiceEventSource)).LogError("Log Error");
            _log.CreateLog(typeof(ConcreteFactoryServiceEventSource)).LogInformation("Log Information");
            _log.CreateLog(typeof(ConcreteFactoryServiceEventSource)).LogWarning("Log Warning");

            return new HttpResponseMessage()
            {
                Content = new StringContent("buena suerte !!!!")
            };
        }
 
 # App Service
 
 In global.asax file of WebApi project. You see how to enable events and configure the even source for App service.
 
 We will using the table storage as the provider.
            
            Sets up the provider specific details. 
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
            // Enable the Events.
            tsListener?.EnableEvents(eventSource, Microsoft.Diagnostics.Tracing.EventLevel.Verbose);
            
  Log controller implementation
  
  private readonly ILogStrategy _log;

        public ValuesController(ILogStrategy log)
        {
            _log = log;
        }

        [HttpGet]
        [Route("logmessage")]
        public HttpResponseMessage Get()
        {
             // Logs to table storage
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogVerbose("Log Verbose");
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogCritical(("Log Critical"));
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogError("Log Error");
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogInformation("Log Information");
            _log.CreateLog(typeof(ConcreteFactoryAppEventSource)).LogWarning("Log Warning");

            return new HttpResponseMessage()
            {
                Content = new StringContent("buena suerte !!!!")
            };
        }
# ARM Changes

Notice the changes required for associating a table storage log table to service fabric.

                                                      {
                                                        "provider": "sf-Diagnostics-webservice",
                                                        "scheduledTransferPeriod": "PT5M",
                                                        "DefaultEvents": {
                                                          "eventDestination": "sfServiceFabricLogs"
                                                        }
                                                      }

# Note

I have modifed Microsoft.Diagnostics.EventListeners/TableStorageEventListener.cs/ToTableEntity method to make the log table as same as service fabric. So its up to you to use the original one or the other one.
