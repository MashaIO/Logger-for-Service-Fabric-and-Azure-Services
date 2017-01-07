using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Logger.Base;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace sflogservice
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class sflogservice : StatelessService
    {
        private readonly ILog _log;

        public sflogservice(StatelessServiceContext context, ILog log)
            : base(context)
        {
            _log = log;
            _log.SetServiceContext(context);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                new ServiceInstanceListener(parameters => new OwinCommunicationListener("", new Startup(parameters), parameters, _log))
            };
        }

        
    }
}
