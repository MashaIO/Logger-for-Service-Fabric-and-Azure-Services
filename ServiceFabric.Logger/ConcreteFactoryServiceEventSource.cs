using System;
using Logger.Base;

namespace Logger.ServiceFabric
{
    public class ConcreteFactoryServiceEventSource : ILoggerFactory
    {
        public ILog GetLogger() //Factory Method Implementation 
        {
            return new ServiceFabricLog();
        }
        public bool AppliesTo(Type type)
        {
            return typeof(ConcreteFactoryServiceEventSource) == type;
        }
    }
}