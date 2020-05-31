using IFactory.Domain.Entities;

namespace IFactory.Service
{
    public interface IAppService : IBaseService<AppInfo>
    {
        AppInfo GetApp(string appKey);
    }
}
