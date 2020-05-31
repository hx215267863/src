using IFactory.Domain.Entities;
using PagedList;

namespace IFactory.Service
{
    public interface IRoleService : IBaseService<RoleInfo>
    {
        IPagedList<RoleInfo> GetPagedRoles(int pageNo, int pageSize);
    }
}
