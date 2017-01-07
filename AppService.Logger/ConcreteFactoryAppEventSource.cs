using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;
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
