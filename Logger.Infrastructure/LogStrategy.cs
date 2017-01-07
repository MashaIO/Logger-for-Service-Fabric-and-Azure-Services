using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger.Base;

namespace Logger.Infrastructure
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
