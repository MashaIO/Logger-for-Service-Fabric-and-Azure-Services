using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Threading;
using Logger.Base;

namespace Logger.AppService
{
    public class AppServiceLogFactory : ILogFactory
    {
        public ILogInternal GetLogger()
        {
            return AppServiceEventSource.Current;
        }

      
    }
}
