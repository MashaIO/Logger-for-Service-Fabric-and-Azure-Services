using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using Microsoft.Diagnostics.EventListeners;

namespace Diagnostic.EventListeners.AppService
{
    public class AppServiceConfigurationProvider : IConfigurationProvider
    {
        private readonly List<KeyValuePair<string, string>> _configurationProperties = null;

        public bool HasConfiguration => _configurationProperties != null;

        public AppServiceConfigurationProvider()
        {
            UseConfiguration();
        }

        public string GetValue(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            List<KeyValuePair<string, string>> cachedConfigurationProperties = this._configurationProperties;

            var kvp = cachedConfigurationProperties.Where(x => x.Key.Equals(name));
            
            var keyValuePairs = kvp as KeyValuePair<string, string>[] ?? kvp.ToArray();
            if (!keyValuePairs.Any())
            {
                return null;
            }

            return keyValuePairs.FirstOrDefault().Value;
        }

        private void UseConfiguration()
        {
            string accountConnectionString = ConfigurationManager.AppSettings["StorageAccountConnectionString"];
            string storageTableName = ConfigurationManager.AppSettings["StorageTableName"];

            this._configurationProperties.Add(new KeyValuePair<string, string>("StorageAccountConnectionString",
                accountConnectionString));
            this._configurationProperties.Add(new KeyValuePair<string, string>("StorageTableName", storageTableName));

        }
    }
}