using System;
using Logger.Base;

namespace Logger.AppService
{
    public class ConcreteFactoryAppEventSource : ILoggerFactory
    {
        public ILog GetLogger()
        {
            return AppServiceLog.Current;
        }

        public bool AppliesTo(Type type)
        {
            return typeof(ConcreteFactoryAppEventSource) == type;
        }
    }

    sealed class TableStorageEventListener : EventListener
    {
        /// <summary>
        /// Storage file to be used to write logs
        /// </summary>


        /// <summary>
        /// Name of the current event listener
        /// </summary>
        private string m_Name;

        /// <summary>
        /// The format to be used by logging.
        /// </summary>
        private string m_Format = "{0:yyyy-MM-dd HH\\:mm\\:ss\\:ffff}\tType: {1}\tId: {2}\tMessage: '{3}'";

        private SemaphoreSlim m_SemaphoreSlim = new SemaphoreSlim(1);

        public StorageFileEventListener(string name)
        {
            this.m_Name = name;

            Debug.WriteLine("StorageFileEventListener for {0} has name {1}", GetHashCode(), name);

            AssignLocalFile();
        }

        private void AssignLocalFile()
        {

        }

        private void WriteToFile(IEnumerable<string> lines)
        {


        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {


            var lines = new List<string>();

            var newFormatedLine = string.Format(m_Format, DateTime.Now, eventData.Level, eventData.EventId, eventData.Payload[0]);

            Debug.WriteLine(newFormatedLine);

            lines.Add(newFormatedLine);

            WriteToFile(lines);
        }
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            Debug.WriteLine("OnEventSourceCreated for Listener {0} - {1} got eventSource {2}", GetHashCode(), m_Name, eventSource.Name);
        }
    }
}
