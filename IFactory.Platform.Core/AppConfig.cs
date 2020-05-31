using System.Configuration;

namespace IFactory.Platform.Core
{
    public class AppConfig
    {
        public static readonly AppConfig Current = new AppConfig();

        public int DebugManagerId { get; private set; }

        static AppConfig()
        {
            AppConfig.Current.DebugManagerId = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DebugManagerId"]) ? 0 : int.Parse(ConfigurationManager.AppSettings["DebugManagerId"]);
        }
    }
}
