using System.Configuration;

namespace IFactory.Platform.Client.Config
{
    public class WebApiConfig
    {
        public static WebApiConfig Default { get; private set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string ServerUrl { get; set; }

        public string ServerRootUrl { get; set; }

        static WebApiConfig()
        {
            WebApiConfig.Default = new WebApiConfig();
            WebApiConfigSectionHandler configSectionHandler = ConfigurationManager.GetSection("webApi.config") as WebApiConfigSectionHandler;
            WebApiConfig.Default.AppKey = configSectionHandler.AppKey;
            WebApiConfig.Default.AppSecret = configSectionHandler.AppSecret;
            WebApiConfig.Default.ServerUrl = configSectionHandler.ServerUrl;
            WebApiConfig.Default.ServerRootUrl = configSectionHandler.ServerRootUrl;
        }
    }
}
