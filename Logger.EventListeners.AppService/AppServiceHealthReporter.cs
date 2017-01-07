using System;
using Microsoft.Diagnostics.EventListeners;

namespace Diagnostic.EventListeners.AppService
{
    public class AppServiceHealthReporter : IHealthReporter
    {
        private string _entityIdentifier;
   
        public AppServiceHealthReporter(string entityIdentifier)
        {
            if (string.IsNullOrWhiteSpace(entityIdentifier))
            {
                throw new ArgumentException("entityIdentifier cannot be null or empty", nameof(entityIdentifier));
            }
            this._entityIdentifier = entityIdentifier;

        }

        public void ReportHealthy()
        {
            this.ReportHealth(string.Empty);
        }

        public void ReportProblem(string problemDescription)
        {
            this.ReportHealth(problemDescription);
        }

        private void ReportHealth(string problemDescription)
        {
          
        }
    }
}
