namespace Logger.Base
{
    public interface ILog
    {
        void SetServiceContext(object serviceContext);

        void LogVerbose(string message);

        void LogInformation(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogCritical(string message);

        void LogAlways(string message);
    }
}
