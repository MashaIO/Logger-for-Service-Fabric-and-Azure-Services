using System;
using Logger.Base;

namespace Logger.ServiceFabric
{
    public class ConcreteFactoryServiceEventSource : ILoggerFactory
    {
        private readonly object _serviceContext;

        public ConcreteFactoryServiceEventSource(object serviceContext)
        {
            _serviceContext = serviceContext;
        }
        
        public ILog GetLogger() //Factory Method Implementation 
        {
            var serviceFabricLogInstance = ServiceFabricLog.Current;
            serviceFabricLogInstance.SetServiceContext(_serviceContext);
            return serviceFabricLogInstance;
        }

        public bool AppliesTo(Type type)
        {
            return typeof(ConcreteFactoryServiceEventSource) == type;
        }
    }
}