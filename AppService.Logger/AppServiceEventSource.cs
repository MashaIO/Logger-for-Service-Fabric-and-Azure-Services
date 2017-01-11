using System;
using Microsoft.Diagnostics.Tracing;
using System.Threading.Tasks;
using Logger.Base;

namespace Logger.AppService
{
    sealed class AppServiceEventSource : EventSource, ILogInternal
    {
        public static readonly AppServiceEventSource Current = new AppServiceEventSource();

        static AppServiceEventSource()
        {
            // A workaround for the problem where ETW activities do not get tracked until Tasks infrastructure is initialized.
            // This problem will be fixed in .NET Framework 4.6.2.
            Task.Run(() => { });
        }

        // Instance constructor is private to enforce singleton semantics
        private AppServiceEventSource() : base()
        {

        }

        public void SetServiceContext(object serviceContext)
        {
            throw new NotImplementedException();
        }

        [Event(EventConstants.ServiceVerboseMessageEventId, Level = EventLevel.Verbose)]
        public void Verbose(string message)
        {
            WriteEvent(EventConstants.ServiceVerboseMessageEventId, message);
        }

        [Event(EventConstants.ServiceInfoMessageEventId, Level = EventLevel.Informational)]
        public void Information(string message)
        {
            WriteEvent(EventConstants.ServiceInfoMessageEventId, message);
        }

        [Event(EventConstants.ServiceWarnMessageEventId, Level = EventLevel.Warning)]
        public void Warning(string message)
        {
            WriteEvent(EventConstants.ServiceWarnMessageEventId, message);
        }

        [Event(EventConstants.ServiceErrorMessageEventId, Level = EventLevel.Error)]
        public void Error(string message)
        {
            WriteEvent(EventConstants.ServiceErrorMessageEventId, message);
        }

        [Event(EventConstants.ServiceCriticalMessageEventId, Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            WriteEvent(EventConstants.ServiceCriticalMessageEventId, message);
        }

    }
}
