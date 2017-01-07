using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Logger.Base;

namespace Logger.AppService
{
    internal class AppServiceLog : EventSource, ILog
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

        public void LogVerbose(string message)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(string message)
        {
            throw new NotImplementedException();
        }

        public void LogAlways(string message)
        {
            throw new NotImplementedException();
        }
    }
}
