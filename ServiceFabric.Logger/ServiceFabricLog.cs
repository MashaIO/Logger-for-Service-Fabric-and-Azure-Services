using System;
using System.Diagnostics.Tracing;
using System.Fabric;
using System.Threading.Tasks;

namespace ServiceFabric.Logger
{
    [EventSource(Name = "Lbas-Diagnostics-webservice")]
    internal class ServiceFabricLog : EventSource, ILog
    {
        
        public static readonly ServiceFabricLog Current = new ServiceFabricLog();

        static ServiceFabricLog()
        {
            // A workaround for the problem where ETW activities do not get tracked until Tasks infrastructure is initialized.
            // This problem will be fixed in .NET Framework 4.6.2.
            Task.Run(() => { });
        }

        // Instance constructor is private to enforce singleton semantics
        private ServiceFabricLog() : base()
        {

        }

        // Todo : Move to constants file
        protected const int ServiceInfoMessageEventId = 7;
        protected const int ServiceWarnMessageEventId = 8;
        protected const int ServiceErrorMessageEventId = 9;
        protected const int ServiceVerboseMessageEventId = 10;
        protected const int ServiceCriticalMessageEventId = 11;
        protected const int ServiceLogAlwaysMessageEventId = 12;
        
        public static class Keywords
        {
            public const EventKeywords Requests = (EventKeywords)0x1L;
            public const EventKeywords ServiceInitialization = (EventKeywords)0x2L;
            public const EventKeywords LxFabric = (EventKeywords)0x4L;
        }

        public ServiceContext ServiceContext { get; set; }

        [NonEvent]
        public void LogVerbose(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageVerbose(
                    ServiceContext.ServiceName.ToString(),
                    ServiceContext.ServiceTypeName,
                    ServiceContext.ReplicaOrInstanceId,
                    ServiceContext.PartitionId,
                    ServiceContext.CodePackageActivationContext.ApplicationName,
                    ServiceContext.CodePackageActivationContext.ApplicationTypeName,
                    ServiceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void LogInformation(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageInfo(
                    ServiceContext.ServiceName.ToString(),
                    ServiceContext.ServiceTypeName,
                    ServiceContext.ReplicaOrInstanceId,
                    ServiceContext.PartitionId,
                    ServiceContext.CodePackageActivationContext.ApplicationName,
                    ServiceContext.CodePackageActivationContext.ApplicationTypeName,
                    ServiceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void LogWarning(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageWarn(
                    ServiceContext.ServiceName.ToString(),
                    ServiceContext.ServiceTypeName,
                    ServiceContext.ReplicaOrInstanceId,
                    ServiceContext.PartitionId,
                    ServiceContext.CodePackageActivationContext.ApplicationName,
                    ServiceContext.CodePackageActivationContext.ApplicationTypeName,
                    ServiceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void LogError(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageError(
                    ServiceContext.ServiceName.ToString(),
                    ServiceContext.ServiceTypeName,
                    ServiceContext.ReplicaOrInstanceId,
                    ServiceContext.PartitionId,
                    ServiceContext.CodePackageActivationContext.ApplicationName,
                    ServiceContext.CodePackageActivationContext.ApplicationTypeName,
                    ServiceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void LogCritical(string message)
        {
            if (this.IsEnabled())
            {
               
                ServiceMessageCritical(
                    ServiceContext.ServiceName.ToString(),
                    ServiceContext.ServiceTypeName,
                    ServiceContext.ReplicaOrInstanceId,
                    ServiceContext.PartitionId,
                    ServiceContext.CodePackageActivationContext.ApplicationName,
                    ServiceContext.CodePackageActivationContext.ApplicationTypeName,
                    ServiceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void LogAlways(string message)
        {
            if (this.IsEnabled())
            {
                
                ServiceMessageLogAlways(
                    ServiceContext.ServiceName.ToString(),
                    ServiceContext.ServiceTypeName,
                    ServiceContext.ReplicaOrInstanceId,
                    ServiceContext.PartitionId,
                    ServiceContext.CodePackageActivationContext.ApplicationName,
                    ServiceContext.CodePackageActivationContext.ApplicationTypeName,
                    ServiceContext.NodeContext.NodeName,
                    message);
            }
        }


        [Event(ServiceLogAlwaysMessageEventId, Level = EventLevel.LogAlways, Keywords = Keywords.LxFabric, Message = "{7}")]
        private
#if UNSAFE
        unsafe
#endif
            void ServiceMessageLogAlways(
                string serviceName,
                string serviceTypeName,
                long replicaOrInstanceId,
                Guid partitionId,
                string applicationName,
                string applicationTypeName,
                string nodeName,
                string message)
        {
#if !UNSAFE
            WriteEvent(ServiceLogAlwaysMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

#else
            const int numArgs = 8;
            fixed (char* pServiceName = serviceName, pServiceTypeName = serviceTypeName, pApplicationName = applicationName, pApplicationTypeName = applicationTypeName, pNodeName = nodeName, pMessage = message)
            {
                EventData* eventData = stackalloc EventData[numArgs];
                eventData[0] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                eventData[1] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                eventData[2] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                eventData[3] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                eventData[4] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                eventData[5] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                eventData[6] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                eventData[7] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                WriteEventCore(ServiceLogAlwaysMessageEventId, numArgs, eventData);
            }
#endif
        }

        #region Events

        [Event(ServiceInfoMessageEventId, Level = EventLevel.Informational, Keywords = Keywords.LxFabric, Message = "{7}")]
        private
#if UNSAFE
        unsafe
#endif
            void ServiceMessageInfo(
                string serviceName,
                string serviceTypeName,
                long replicaOrInstanceId,
                Guid partitionId,
                string applicationName,
                string applicationTypeName,
                string nodeName,
                string message)
        {
#if !UNSAFE
            WriteEvent(ServiceInfoMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

#else
            const int numArgs = 8;
            fixed (char* pServiceName = serviceName, pServiceTypeName = serviceTypeName, pApplicationName = applicationName, pApplicationTypeName = applicationTypeName, pNodeName = nodeName, pMessage = message)
            {
                EventData* eventData = stackalloc EventData[numArgs];
                eventData[0] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                eventData[1] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                eventData[2] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                eventData[3] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                eventData[4] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                eventData[5] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                eventData[6] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                eventData[7] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                WriteEventCore(ServiceInfoMessageEventId, numArgs, eventData);
            }
#endif
        }



        [Event(ServiceErrorMessageEventId, Level = EventLevel.Error, Keywords = Keywords.LxFabric, Message = "{7}")]
        private
#if UNSAFE
        unsafe
#endif
            void ServiceMessageError(
                string serviceName,
                string serviceTypeName,
                long replicaOrInstanceId,
                Guid partitionId,
                string applicationName,
                string applicationTypeName,
                string nodeName,
                string message)
        {
#if !UNSAFE
            WriteEvent(ServiceErrorMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

#else
            const int numArgs = 8;
            fixed (char* pServiceName = serviceName, pServiceTypeName = serviceTypeName, pApplicationName = applicationName, pApplicationTypeName = applicationTypeName, pNodeName = nodeName, pMessage = message)
            {
                EventData* eventData = stackalloc EventData[numArgs];
                eventData[0] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                eventData[1] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                eventData[2] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                eventData[3] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                eventData[4] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                eventData[5] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                eventData[6] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                eventData[7] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                WriteEventCore(ServiceErrorMessageEventId, numArgs, eventData);
            }
#endif
        }

        [Event(ServiceWarnMessageEventId, Level = EventLevel.Warning, Keywords = Keywords.LxFabric, Message = "{7}")]
        private
#if UNSAFE
        unsafe
#endif
            void ServiceMessageWarn(
                string serviceName,
                string serviceTypeName,
                long replicaOrInstanceId,
                Guid partitionId,
                string applicationName,
                string applicationTypeName,
                string nodeName,
                string message)
        {
#if !UNSAFE
            WriteEvent(ServiceWarnMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

#else
            const int numArgs = 8;
            fixed (char* pServiceName = serviceName, pServiceTypeName = serviceTypeName, pApplicationName = applicationName, pApplicationTypeName = applicationTypeName, pNodeName = nodeName, pMessage = message)
            {
                EventData* eventData = stackalloc EventData[numArgs];
                eventData[0] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                eventData[1] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                eventData[2] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                eventData[3] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                eventData[4] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                eventData[5] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                eventData[6] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                eventData[7] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                WriteEventCore(ServiceWarnMessageEventId, numArgs, eventData);
            }
#endif
        }

        [Event(ServiceVerboseMessageEventId, Level = EventLevel.Verbose, Keywords = Keywords.LxFabric, Message = "{7}")]
        private
#if UNSAFE
        unsafe
#endif
            void ServiceMessageVerbose(
                string serviceName,
                string serviceTypeName,
                long replicaOrInstanceId,
                Guid partitionId,
                string applicationName,
                string applicationTypeName,
                string nodeName,
                string message)
        {
#if !UNSAFE
            WriteEvent(ServiceVerboseMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

#else
            const int numArgs = 8;
            fixed (char* pServiceName = serviceName, pServiceTypeName = serviceTypeName, pApplicationName = applicationName, pApplicationTypeName = applicationTypeName, pNodeName = nodeName, pMessage = message)
            {
                EventData* eventData = stackalloc EventData[numArgs];
                eventData[0] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                eventData[1] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                eventData[2] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                eventData[3] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                eventData[4] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                eventData[5] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                eventData[6] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                eventData[7] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                WriteEventCore(ServiceVerboseMessageEventId, numArgs, eventData);
            }
#endif
        }

        [Event(ServiceCriticalMessageEventId, Level = EventLevel.Critical, Keywords = Keywords.LxFabric, Message = "{7}"
         )]
        private
#if UNSAFE
        unsafe
#endif
            void ServiceMessageCritical(
                string serviceName,
                string serviceTypeName,
                long replicaOrInstanceId,
                Guid partitionId,
                string applicationName,
                string applicationTypeName,
                string nodeName,
                string message)
        {
#if !UNSAFE
            WriteEvent(ServiceCriticalMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId,
                applicationName, applicationTypeName, nodeName, message);

#else
            const int numArgs = 8;
            fixed (char* pServiceName = serviceName, pServiceTypeName = serviceTypeName, pApplicationName = applicationName, pApplicationTypeName = applicationTypeName, pNodeName = nodeName, pMessage = message)
            {
                EventData* eventData = stackalloc EventData[numArgs];
                eventData[0] = new EventData { DataPointer = (IntPtr) pServiceName, Size = SizeInBytes(serviceName) };
                eventData[1] = new EventData { DataPointer = (IntPtr) pServiceTypeName, Size = SizeInBytes(serviceTypeName) };
                eventData[2] = new EventData { DataPointer = (IntPtr) (&replicaOrInstanceId), Size = sizeof(long) };
                eventData[3] = new EventData { DataPointer = (IntPtr) (&partitionId), Size = sizeof(Guid) };
                eventData[4] = new EventData { DataPointer = (IntPtr) pApplicationName, Size = SizeInBytes(applicationName) };
                eventData[5] = new EventData { DataPointer = (IntPtr) pApplicationTypeName, Size = SizeInBytes(applicationTypeName) };
                eventData[6] = new EventData { DataPointer = (IntPtr) pNodeName, Size = SizeInBytes(nodeName) };
                eventData[7] = new EventData { DataPointer = (IntPtr) pMessage, Size = SizeInBytes(message) };

                WriteEventCore(ServiceCriticalMessageEventId, numArgs, eventData);
            }
#endif
        }

        #endregion
    }
}