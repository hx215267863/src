using IFactory.DB;
using IFactory.Common;
using IFactory.Data;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using IFactory.Domain.Common;

namespace IFactory.LocalService
{
    public class UserService
    {
        // Methods
        public UserService()
        {
        }

        public UserDB userBussiness = new UserBussiness();
        //public IList<UserModel> BuildUserModels(IEnumerable<UserInfo> userInfos)
        //{
        //    int[] roleIds = (from m in userInfos select m.RoleId).Distinct<int>().ToArray<int>();
        //    Dictionary<int, RoleInfo> dictionary = (from m in base.DataContext.RoleInfos
        //                                            where roleIds.Contains<int>(m.RoleId)
        //                                            select m).ToDictionary<RoleInfo, int, RoleInfo>(m => m.RoleId, m => m);
        //    List<UserModel> list = new List<UserModel>();
        //    foreach (UserInfo info in userInfos)
        //    {
        //        UserModel item = new UserModel
        //        {
        //            CreateTime = info.CreateTime,
        //            Gender = info.Gender,
        //            GenderDesc = !info.Gender.HasValue ? null : ((Enum)info.Gender).GetDescription(),
        //            Name = info.Name,
        //            Password = info.Password,
        //            RoleId = info.RoleId,
        //            RoleName = dictionary[info.RoleId].RoleName,
        //            UserId = info.UserId,
        //            UserName = info.UserName
        //        };
        //        list.Add(item);
        //    }
        //    return list;
        //}

        //public IList<PermissionInfo> GetAllPermissions()
        //{
        //    return (from m in base.DataContext.PermissionInfos
        //            orderby m.ParentId, m.DisplayOrder
        //            select m).ToList<PermissionInfo>();
        //}

        //public IPagedList<UserInfo> GetPagedUsers(int pageNo, int pageSize)
        //{
        //    return new PagedList.PagedList<UserInfo>(from m in base.DataContext.UserInfos
        //                                             orderby m.UserId
        //                                             select m, pageNo, pageSize);
        //}

        //public PermissionInfo GetPermission(int permissionId)
        //{
        //    return base.DataContext.PermissionInfos.Find(new object[] { permissionId });
        //}

        //public IList<PermissionInfo> GetPermissionsByParentId(int? parentId)
        //{
        //    return (from m in base.DataContext.PermissionInfos
        //            where m.ParentId == parentId
        //            orderby m.DisplayOrder
        //            select m).ToList();
        //}

        public UserInfo GetUserByUserName(string userName)
        {
            DataTable tb = userBussiness.GetUserByUserName(userName);
            UserInfo info = new UserInfo();
            if (tb != null && tb.Rows.Count > 0)
            {
                info.UserId = int.Parse(tb.Rows[0][0].ToString());
                info.UserName = tb.Rows[0][1].ToString();
                info.Password = tb.Rows[0][2].ToString();
                info.Name = tb.Rows[0][3].ToString();
                info.RoleId = int.Parse(tb.Rows[0][4].ToString());
                info.CreateTime = DateTime.Parse(tb.Rows[0][5].ToString());
                info.Gender = (Gender)Enum.Parse(typeof(Gender), tb.Rows[0][6].ToString());
                DateTime LastLoginTime;
                DateTime.TryParse(tb.Rows[0][7].ToString(), out LastLoginTime);
                info.LastLoginTime = LastLoginTime;
                info.CraftDIDs = tb.Rows[0][8].ToString();
                RoleInfo roleInfo = new RoleInfo();
                
            }
            return info;
        }

        //public IList<UserInfo> GetUsers(int[] userIds)
        //{
        //    return (from m in this.Table
        //            where userIds.Contains<int>(m.UserId)
        //            select m).ToList<UserInfo>();
        //}

        //public void Update(PermissionInfo permissionInfo)
        //{
        //    base.DataContext.SaveChanges();
        //}

        public void Update(UserInfo usinfo)
        {

        }

        //public void UpdatePermissions(IList<PermissionInfo> permissionInfos)
        //{
        //    base.DataContext.SaveChanges();
        //}
    }
}
