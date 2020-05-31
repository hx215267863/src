using System;
using System.Configuration;

namespace IFactory.Platform.Client.Config
{
    public class WebApiConfigSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("appKey", IsRequired = true)]
        public string AppKey
        {
            get
            {
                return (string)base["appKey"];
            }
            set
            {
                base["appKey"] = value;
            }
        }

        [ConfigurationProperty("appSecret", IsRequired = true)]
        public string AppSecret
        {
            get
            {
                return (string)base["appSecret"];
            }
            set
            {
                base["appSecret"] = value;
            }
        }

        [ConfigurationProperty("serverUrl")]
        public string ServerUrl
        {
            get
            {
                return (string)base["serverUrl"];
            }
            set
            {
                base["serverUrl"] = value;
            }
        }

        [ConfigurationProperty("serverRootUrl")]
        public string ServerRootUrl
        {
            get
            {
                return (string)base["serverRootUrl"];
            }
            set
            {
                base["serverRootUrl"] = value;
            }
        }
    }
}
