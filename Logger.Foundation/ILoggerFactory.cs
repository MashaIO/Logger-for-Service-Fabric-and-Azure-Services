using System;

namespace Logger.Base
{
    public interface  ILoggerFactory
    {
        ILog GetLogger(); //Factory Method Declaration 

        bool AppliesTo(Type type);
    }
}
