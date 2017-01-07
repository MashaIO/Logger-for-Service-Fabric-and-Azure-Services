using System;
using Logger.Base;

namespace Logger.Infrastructure
{
    public interface ILogStrategy
    {
        ILog CreateLog(Type type);
    }
}