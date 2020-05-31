using Newtonsoft.Json;
using System.Web;
using System.Web.Security;

namespace IFactory.Platform.Core
{
    public class UserSession
    {
        public int ManagerId { get; set; }

        public static UserSession GetCurrentUserSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated ? JsonConvert.DeserializeObject<UserSession>(HttpContext.Current.User.Identity.Name) : new UserSession();
        }

        public void ManagerLogOff()
        {
            this.ManagerId = 0;
            FormsAuthentication.SetAuthCookie(JsonConvert.SerializeObject(this), false);
        }

        public void ManagerLogOn(int managerId)
        {
            this.ManagerId = managerId;
            FormsAuthentication.SetAuthCookie(JsonConvert.SerializeObject(this), false);
        }
    }
}
