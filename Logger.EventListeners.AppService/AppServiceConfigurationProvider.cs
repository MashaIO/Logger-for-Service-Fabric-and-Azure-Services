using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Diagnostics.EventListeners;
using EventLevel = Microsoft.Diagnostics.Tracing.EventLevel;

namespace Diagnostic.EventListeners.AppService
{
    public class AppServiceConfigurationProvider : IConfigurationProvider
    {

        private EventLevel _eventLevels;

        public EventLevel EventLevel    => _eventLevels;

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
            string eventLevel = ConfigurationManager.AppSettings["EventLevel"];

            var keyValuePairs = this._configurationProperties ?? new List<KeyValuePair<string, string>>();

            keyValuePairs.Add(new KeyValuePair<string, string>("StorageAccountConnectionString",
                   accountConnectionString));
            keyValuePairs.Add(new KeyValuePair<string, string>("StorageTableName", storageTableName));
            _eventLevels = GetLevelForEvents(eventLevel);

            this._configurationProperties = keyValuePairs;
        }


        private EventLevel GetLevelForEvents(string eventLevel)
        {
            EventLevel level = EventLevel.LogAlways;
            switch (eventLevel)
            {
                case "Verbose":
                    level = EventLevel.Verbose;
                    break;
                case "Informational":
                    level = EventLevel.Informational;
                    break;
                case "Warning":
                    level = EventLevel.Warning;
                    break;
                case "Error":
                    level = EventLevel.Error;
                    break;
                case "Critical":
                    level = EventLevel.Critical;
                    break;
            }
            return level;
        }
    }
}