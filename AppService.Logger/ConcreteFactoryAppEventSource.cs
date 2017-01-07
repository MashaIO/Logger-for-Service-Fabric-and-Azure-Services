using System;
using Logger.Base;

namespace Logger.AppService
{
    public class ConcreteFactoryAppEventSource : ILoggerFactory
    {
        public ILog GetLogger()
        {
            return AppServiceLog.Current;
        }

        public bool AppliesTo(Type type)
        {
            return typeof(ConcreteFactoryAppEventSource) == type;
        }
    }
}
