using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using PagedList;
using System.Collections.Generic;

namespace IFactory.LocalService
{
    public interface IUserService : IBaseService<UserInfo>
    {
        IList<UserInfo> GetUsers(int[] userIds);

        IPagedList<UserInfo> GetPagedUsers(int pageNo, int pageSize);

        IList<UserModel> BuildUserModels(IEnumerable<UserInfo> userInfos);

        UserInfo GetUserByUserName(string userName);

        IList<PermissionInfo> GetAllPermissions();

        PermissionInfo GetPermission(int permissionId);

        void UpdatePermissions(IList<PermissionInfo> permissionInfos);

        void Update(PermissionInfo permissionInfo);

        IList<PermissionInfo> GetPermissionsByParentId(int? parentId);
    }
}
