using IFactory.Domain.Entities;

namespace IFactory.LocalService
{
    public interface IAppService : IBaseService<AppInfo>
    {
        AppInfo GetApp(string appKey);
    }
}
