using System;

namespace Logger.Base
{
    public interface ILogStrategy
    {
        ILog CreateLog(Type type);
    }
}