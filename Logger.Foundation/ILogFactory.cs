using System;

namespace Logger.Base
{
    public interface  ILogFactory
    {
        ILogInternal GetLogger(); //Factory Method Declaration 

        bool AppliesTo(Type type);
    }
}
