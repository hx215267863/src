using IFactory.Common;
using IFactory.Data;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.Service
{
    public class UserService : BaseService<UserInfo>, IUserService, IBaseService<UserInfo>
    {
        // Methods
        public UserService(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public IList<UserModel> BuildUserModels(IEnumerable<UserInfo> userInfos)
        {
            int[] roleIds = (from m in userInfos select m.RoleId).Distinct<int>().ToArray<int>();
            Dictionary<int, RoleInfo> dictionary = (from m in base.DataContext.RoleInfos
                                                    where roleIds.Contains<int>(m.RoleId)
                                                    select m).ToDictionary<RoleInfo, int, RoleInfo>(m => m.RoleId, m => m);
            List<UserModel> list = new List<UserModel>();
            foreach (UserInfo info in userInfos)
            {
                UserModel item = new UserModel
                {
                    CreateTime = info.CreateTime,
                    Gender = info.Gender,
                    GenderDesc = !info.Gender.HasValue ? null : ((Enum)info.Gender).GetDescription(),
                    Name = info.Name,
                    Password = info.Password,
                    RoleId = info.RoleId,
                    RoleName = dictionary[info.RoleId].RoleName,
                    UserId = info.UserId,
                    UserName = info.UserName
                };
                list.Add(item);
            }
            return list;
        }

        public IList<PermissionInfo> GetAllPermissions()
        {
            return (from m in base.DataContext.PermissionInfos
                    orderby m.ParentId, m.DisplayOrder
                    select m).ToList<PermissionInfo>();
        }

        public IPagedList<UserInfo> GetPagedUsers(int pageNo, int pageSize)
        {
            return new PagedList.PagedList<UserInfo>(from m in base.DataContext.UserInfos
                                                     orderby m.UserId
                                                     select m, pageNo, pageSize);
        }

        public PermissionInfo GetPermission(int permissionId)
        {
            return base.DataContext.PermissionInfos.Find(new object[] { permissionId });
        }

        public IList<PermissionInfo> GetPermissionsByParentId(int? parentId)
        {
            return (from m in base.DataContext.PermissionInfos
                    where m.ParentId == parentId
                    orderby m.DisplayOrder
                    select m).ToList();
        }

        public UserInfo GetUserByUserName(string userName)
        {
            return (from m in this.Table
                    where m.UserName == userName
                    select m).FirstOrDefault<UserInfo>();
        }

        public IList<UserInfo> GetUsers(int[] userIds)
        {
            return (from m in this.Table
                    where userIds.Contains<int>(m.UserId)
                    select m).ToList<UserInfo>();
        }

        public void Update(PermissionInfo permissionInfo)
        {
            base.DataContext.SaveChanges();
        }

        public void UpdatePermissions(IList<PermissionInfo> permissionInfos)
        {
            base.DataContext.SaveChanges();
        }
    }



}
