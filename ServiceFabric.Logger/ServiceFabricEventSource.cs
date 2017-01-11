using System;
using System.Diagnostics.Tracing;
using System.Fabric;
using System.Threading.Tasks;
using Logger.Base;

namespace Logger.ServiceFabric
{
    [EventSource(Name = "sf-Diagnostics-webservice")]
    internal sealed class ServiceFabricEventSource : EventSource, ILogInternal
    {
        public static readonly ServiceFabricEventSource Current = new ServiceFabricEventSource();

        static ServiceFabricEventSource()
        {
            // A workaround for the problem where ETW activities do not get tracked until Tasks infrastructure is initialized.
            // This problem will be fixed in .NET Framework 4.6.2.
            Task.Run(() => { });
        }

        // Instance constructor is private to enforce singleton semantics
        private ServiceFabricEventSource() : base()
        {

        }

        public static class Keywords
        {
            public const EventKeywords Requests = (EventKeywords)0x1L;
            public const EventKeywords ServiceInitialization = (EventKeywords)0x2L;
            public const EventKeywords LxFabric = (EventKeywords)0x4L;
        }

        private ServiceContext _serviceContext;

        public void SetServiceContext(object serviceContext)
        {
            if (serviceContext == null)
            {
                throw new ArgumentNullException(nameof(serviceContext));
            }

            var isServiceContext = serviceContext is ServiceContext;
            if (isServiceContext)
            {
                _serviceContext = (ServiceContext) serviceContext;
            }
        }

        [NonEvent]
        public void Verbose(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageVerbose(
                    _serviceContext.ServiceName.ToString(),
                    _serviceContext.ServiceTypeName,
                    _serviceContext.ReplicaOrInstanceId,
                    _serviceContext.PartitionId,
                    _serviceContext.CodePackageActivationContext.ApplicationName,
                    _serviceContext.CodePackageActivationContext.ApplicationTypeName,
                    _serviceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void Information(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageInfo(
                    _serviceContext.ServiceName.ToString(),
                    _serviceContext.ServiceTypeName,
                    _serviceContext.ReplicaOrInstanceId,
                    _serviceContext.PartitionId,
                    _serviceContext.CodePackageActivationContext.ApplicationName,
                    _serviceContext.CodePackageActivationContext.ApplicationTypeName,
                    _serviceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void Warning(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageWarn(
                    _serviceContext.ServiceName.ToString(),
                    _serviceContext.ServiceTypeName,
                    _serviceContext.ReplicaOrInstanceId,
                    _serviceContext.PartitionId,
                    _serviceContext.CodePackageActivationContext.ApplicationName,
                    _serviceContext.CodePackageActivationContext.ApplicationTypeName,
                    _serviceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void Error(string message)
        {
            if (this.IsEnabled())
            {
                ServiceMessageError(
                    _serviceContext.ServiceName.ToString(),
                    _serviceContext.ServiceTypeName,
                    _serviceContext.ReplicaOrInstanceId,
                    _serviceContext.PartitionId,
                    _serviceContext.CodePackageActivationContext.ApplicationName,
                    _serviceContext.CodePackageActivationContext.ApplicationTypeName,
                    _serviceContext.NodeContext.NodeName,
                    message);
            }
        }

        [NonEvent]
        public void Critical(string message)
        {
            if (this.IsEnabled())
            {
               
                ServiceMessageCritical(
                    _serviceContext.ServiceName.ToString(),
                    _serviceContext.ServiceTypeName,
                    _serviceContext.ReplicaOrInstanceId,
                    _serviceContext.PartitionId,
                    _serviceContext.CodePackageActivationContext.ApplicationName,
                    _serviceContext.CodePackageActivationContext.ApplicationTypeName,
                    _serviceContext.NodeContext.NodeName,
                    message);
            }
        }

        #region Events

        [Event(EventConstants.ServiceInfoMessageEventId, Level = EventLevel.Informational, Keywords = Keywords.LxFabric, Message = "{7}")]
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
            WriteEvent(EventConstants.ServiceInfoMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

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

                WriteEventCore(EventConstants.ServiceInfoMessageEventId, numArgs, eventData);
            }
#endif
        }



        [Event(EventConstants.ServiceErrorMessageEventId, Level = EventLevel.Error, Keywords = Keywords.LxFabric, Message = "{7}")]
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
            WriteEvent(EventConstants.ServiceErrorMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

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

                WriteEventCore(EventConstants.ServiceErrorMessageEventId, numArgs, eventData);
            }
#endif
        }

        [Event(EventConstants.ServiceWarnMessageEventId, Level = EventLevel.Warning, Keywords = Keywords.LxFabric, Message = "{7}")]
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
            WriteEvent(EventConstants.ServiceWarnMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

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

                WriteEventCore(EventConstants.ServiceWarnMessageEventId, numArgs, eventData);
            }
#endif
        }

        [Event(EventConstants.ServiceVerboseMessageEventId, Level = EventLevel.Verbose, Keywords = Keywords.LxFabric, Message = "{7}")]
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
            WriteEvent(EventConstants.ServiceVerboseMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId, applicationName, applicationTypeName, nodeName, message);

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

                WriteEventCore(EventConstants.ServiceVerboseMessageEventId, numArgs, eventData);
            }
#endif
        }

        [Event(EventConstants.ServiceCriticalMessageEventId, Level = EventLevel.Critical, Keywords = Keywords.LxFabric, Message = "{7}"
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
            WriteEvent(EventConstants.ServiceCriticalMessageEventId, serviceName, serviceTypeName, replicaOrInstanceId, partitionId,
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

                WriteEventCore(EventConstants.ServiceCriticalMessageEventId, numArgs, eventData);
            }
#endif
        }

        #endregion
    }
}