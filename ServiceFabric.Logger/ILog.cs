using System.Fabric;

namespace ServiceFabric.Logger
{
    public interface ILog 
    {
        ServiceContext ServiceContext { get; set; }

        void LogVerbose(string message);

        void LogInformation(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogCritical(string message);

        void LogAlways(string message);

    }
}