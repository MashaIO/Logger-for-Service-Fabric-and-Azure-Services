namespace ServiceFabric.Logger
{
    class ConcreteFactoryServiceEventSource : LoggerFactory
    {
        public override ILog GetLogger() //Factory Method Implementation 
        {
            return  ServiceFabricLog.Current;
        }
    }
}