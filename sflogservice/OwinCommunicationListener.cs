using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logger.Base;
using Microsoft.Owin.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace sflogservice
{
    public class OwinCommunicationListener : ICommunicationListener
    {
        private readonly IOwinAppBuilder startup;
        private readonly StatelessServiceContext serviceContext;
        private readonly string appRoot;
        private IDisposable serverHandle;
        private string listeningAddress;

        public OwinCommunicationListener(string appRoot, IOwinAppBuilder startup, StatelessServiceContext serviceContext)
        {
            this.startup = startup;
            this.appRoot = appRoot;
            this.serviceContext = serviceContext;
           
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            EndpointResourceDescription serviceEndpoint = this.serviceContext.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = serviceEndpoint.Port;

            this.listeningAddress = String.Format(
                CultureInfo.InvariantCulture,
                "http://+:{0}/{1}",
                port,
                String.IsNullOrWhiteSpace(this.appRoot)
                    ? String.Empty
                    : this.appRoot.TrimEnd('/') + '/');

            this.serverHandle = WebApp.Start(this.listeningAddress, appBuilder => this.startup.Configuration(appBuilder));

            string resultAddress = this.listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);

            ServiceEventSource.Current.Message("Listening on {0}", resultAddress);

            return Task.FromResult(resultAddress);
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            this.StopWebServer();

            return Task.FromResult(true);
        }

        public void Abort()
        {
            this.StopWebServer();
        }

        private void StopWebServer()
        {
            if (this.serverHandle != null)
            {
                try
                {
                    this.serverHandle.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    // no-op
                }
            }
        }
    }
}
