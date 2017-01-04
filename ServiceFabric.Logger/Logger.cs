using System.Threading.Tasks;

namespace ServiceFabric.Logger
{
    public class Logger
    {
        static Logger()
        {
            Task.Run(() => { });
        }

        // Instance constructor is private to enforce singleton semantics
        private Logger() : base()
        {

        }

        public static readonly Logger Current = new Logger();

        public ILog ServiceLogger
        {
            get
            {
                LoggerFactory loggerFactory = new ConcreteFactoryServiceEventSource();
                ILog log = loggerFactory.GetLogger();
                return log;
            }
        }
    }
}