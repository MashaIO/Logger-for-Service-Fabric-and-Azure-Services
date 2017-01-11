using System;
using System.Linq;

namespace Logger.Base
{
    public class Log : ILog
    {
        private readonly ILoggerFactory _logFactory;

        public Log(ILoggerFactory logFactory)
        {
            if (logFactory == null)
            {
                throw new ArgumentNullException(nameof(logFactory));
            }

            this._logFactory = logFactory;
        }

        private ILogInternal CurrentLog => _logFactory.GetLogger();

        public void SetServiceContext(object serviceContext)
        {
            throw new NotImplementedException();
        }

        public void Verbose(string message)
        {
            CurrentLog.Verbose(message);
        }

        public void Information(string message)
        {
            CurrentLog.Information(message);
        }

        public void Warning(string message)
        {
            CurrentLog.Warning(message);
        }

        public void Error(string message)
        {
            CurrentLog.Error(message);
        }

        public void Critical(string message)
        {
            CurrentLog.Critical(message);
        }
    }
}
