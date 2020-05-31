using IFactory.Domain.Entities;
using PagedList;

namespace IFactory.LocalService
{
    public interface IRoleService : IBaseService<RoleInfo>
    {
        IPagedList<RoleInfo> GetPagedRoles(int pageNo, int pageSize);
    }
}
