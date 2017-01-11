namespace Logger.Base
{
    public interface ILogInternal
    {
        void SetServiceContext(object serviceContext);

        void Verbose(string message);

        void Information(string message);

        void Warning(string message);

        void Error(string message);

        void Critical(string message);

    }
}
