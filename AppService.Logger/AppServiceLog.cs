using System;
using Microsoft.Diagnostics.Tracing;
using System.Threading.Tasks;
using Logger.Base;

namespace Logger.AppService
{
    sealed class AppServiceLog : EventSource, ILog
    {
        public static readonly AppServiceLog Current = new AppServiceLog();

        static AppServiceLog()
        {
            // A workaround for the problem where ETW activities do not get tracked until Tasks infrastructure is initialized.
            // This problem will be fixed in .NET Framework 4.6.2.
            Task.Run(() => { });
        }

        // Instance constructor is private to enforce singleton semantics
        private AppServiceLog() : base()
        {

        }

        public void SetServiceContext(object serviceContext)
        {
            throw new NotImplementedException();
        }

        [Event(EventConstants.ServiceVerboseMessageEventId, Level = EventLevel.Verbose)]
        public void LogVerbose(string message)
        {
            WriteEvent(EventConstants.ServiceVerboseMessageEventId, message);
        }

        [Event(EventConstants.ServiceInfoMessageEventId, Level = EventLevel.Informational)]
        public void LogInformation(string message)
        {
            WriteEvent(EventConstants.ServiceInfoMessageEventId, message);
        }

        [Event(EventConstants.ServiceWarnMessageEventId, Level = EventLevel.Warning)]
        public void LogWarning(string message)
        {
            WriteEvent(EventConstants.ServiceWarnMessageEventId, message);
        }

        [Event(EventConstants.ServiceErrorMessageEventId, Level = EventLevel.Error)]
        public void LogError(string message)
        {
            WriteEvent(EventConstants.ServiceErrorMessageEventId, message);
        }

        [Event(EventConstants.ServiceCriticalMessageEventId, Level = EventLevel.Critical)]
        public void LogCritical(string message)
        {
            WriteEvent(EventConstants.ServiceCriticalMessageEventId, message);
        }

    }
}
