using System;

namespace Logger.Base
{
    public interface  ILoggerFactory
    {
        ILogInternal GetLogger(); //Factory Method Declaration 

        bool AppliesTo(Type type);
    }
}
