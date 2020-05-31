using System.Web.Mvc;

namespace IFactory.Platform.Core
{
    public class ServiceHelper
    {
        public static TService LoadService<TService>()
        {
            return DependencyResolver.Current.GetService<TService>();
        }
    }
}
