using System;
using System.Linq;

namespace Logger.Base
{
    public class LogStrategy : ILogStrategy
    {
        private readonly ILoggerFactory[] _logFactories;

        public LogStrategy(ILoggerFactory[] logFactories)
        {
            if (logFactories == null)
            {
                throw new ArgumentNullException(nameof(logFactories));
            }

            this._logFactories = logFactories;
        }

        public ILog CreateLog(Type type)
        {
            var logFactory = this._logFactories
                .FirstOrDefault(factory => factory.AppliesTo(type));

            if (logFactory == null)
            {
                throw new Exception("type not registered");
            }

            return logFactory.GetLogger();
        }
    }
}
