using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using Microsoft.Diagnostics.EventListeners;

namespace Diagnostic.EventListeners.AppService
{
    public class AppServiceConfigurationProvider : IConfigurationProvider
    {
        private  List<KeyValuePair<string, string>> _configurationProperties = null;

        public bool HasConfiguration
        {
            get
            {
                if (_configurationProperties != null && _configurationProperties.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                 
            }
        }

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

            var keyValuePairs = this._configurationProperties ?? new List<KeyValuePair<string, string>>();

            keyValuePairs.Add(new KeyValuePair<string, string>("StorageAccountConnectionString",
                   accountConnectionString));
            keyValuePairs.Add(new KeyValuePair<string, string>("StorageTableName", storageTableName));

            this._configurationProperties = keyValuePairs;
        }
    }
}