using IFactory.Platform.Client;
using IFactory.Platform.Client.Config;
using IFactory.Platform.Common;
using IFactory.Platform.Common.Request;
using System.Configuration;
using System.Threading.Tasks;

namespace IFactory.UI.Core
{
    public class ClientHelper
    {
        public static void UpdateAppSetting(string name, string value)
        {
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string sectionName = "appSettings";
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection(sectionName);
            appSettingsSection.Settings.Remove(name);
            appSettingsSection.Settings.Add(name, value);
            configuration.Save();
        }

        public static T Execute<T>(IRequest<T> request) where T : BaseResponse
        {
            return new DefaultWebApiClient(WebApiConfig.Default.ServerUrl, WebApiConfig.Default.AppKey, WebApiConfig.Default.AppSecret).Execute<T>(request);
        }

        public static async Task<T> ExecuteAsync<T>(IRequest<T> request) where T : BaseResponse
        {
            return await new DefaultWebApiClient(WebApiConfig.Default.ServerUrl, WebApiConfig.Default.AppKey, WebApiConfig.Default.AppSecret).ExecuteAsync<T>(request);
        }
    }
}
