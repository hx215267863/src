using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFactory.Domain.Entities;
using IFactory.Domain.Common;
using System.Data.Common;
using PagedList;
using IFactory.Domain.Models;
using IFactory.Common;

namespace IFactory.DB
{
    public class UserDB : BaseFacade
    {
        public static bool logCell = true;

        public IList<UserModel> BuildUserModels(IEnumerable<UserInfo> userInfos)
        {
            int[] roleIds = (from m in userInfos select m.RoleId).Distinct<int>().ToArray<int>();
            Dictionary<int, RoleInfo> dictionary = (from m in GetRoles()
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

        public DataTable UserTableQuery()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from users order by userId";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        //添加用户信息
        public void InsertUsers(UserInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;

            if(info.Gender.HasValue)
            {
                string sql = string.Format(@"insert users (userName, password, name, roleId, createTime, gender, craft_dids)
                                            values('{0}', '{1}', '{2}', {3}, '{4}', {5}, '{6}'); "
                                            , info.UserName, info.Password, info.Name, info.RoleId, info.CreateTime.ToString()
                                            , (int)info.Gender, info.CraftDIDs);
                equipDB.ExecuteNonQuery(CommandType.Text, sql);
            }
            else
            {
                string sql = string.Format(@"insert users (userName, password, name, roleId, createTime, craft_dids)
                                            values('{0}', '{1}', '{2}', {3}, '{4}', {5}); "
                                            , info.UserName, info.Password, info.Name, info.RoleId, info.CreateTime.ToString()
                                            , info.CraftDIDs);
                equipDB.ExecuteNonQuery(CommandType.Text, sql);
            }
        }

        //删除用户信息
        public void DeleteUser(int UserID)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"delete from users  where userId = {0}"
              , (UserID));
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public List<UserInfo> GetUsers()
        {
            DataTable tb = UserTableQuery();

            List<UserInfo> lst = new List<UserInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    UserInfo info = new UserInfo();
                    info.UserId = int.Parse(row[0].ToString());
                    info.UserName = row[1].ToString();
                    info.Password = row[2].ToString();
                    info.Name = row[3].ToString();
                    info.RoleId = int.Parse(row[4].ToString());
                    info.CreateTime = DateTime.Parse(row[5].ToString());
                    if (row[6].ToString() == "")
                        info.Gender = null;
                    else
                        info.Gender = (Gender)Enum.Parse(typeof(Gender), row[6].ToString());
                    if (row[7].ToString() == "")
                        info.LastLoginTime = null;
                    else
                        info.LastLoginTime = DateTime.Parse(row[7].ToString());
                    info.CraftDIDs = row[8].ToString();

                    info.Role = GetRoleByRoleId(info.RoleId);

                    lst.Add(info);
                }
            }
            return lst;
        }

        public IPagedList<UserInfo> GetPagedUsers(int pageNo, int pageSize)
        {
            return new PagedList.PagedList<UserInfo>(from m in GetUsers()
                                                     orderby m.UserId
                                                     select m, pageNo, pageSize);
        }

        private DataTable getUserByUserName(string userName)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from users t where
                                                ('{0}' = '' or t.userName = '{1}')"
                                        , userName, userName);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        //获取用户信息
        public UserInfo GetUserByUserName(string userName)
        {
            DataTable tb = getUserByUserName(userName);
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
                info.Role = GetRoleByRoleId(info.RoleId);
            }
            return info;
        }

        private DataTable getRoles()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from roles";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        private List<RoleInfo> GetRoles()
        {
            DataTable tb = getRoles();
            List<RoleInfo> lst = new List<RoleInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    RoleInfo info = new RoleInfo();
                    info.RoleId = int.Parse(row[0].ToString());
                    info.RoleName = row[1].ToString();
                    info.PermissionCodes = row[2].ToString();
                    info.CreateTime = DateTime.Parse(row[3].ToString());
                    info.Remark = row[4].ToString();
                    lst.Add(info);
                }
            }
            return lst;
        }

        private DataTable getRoleByRoleId(int roleid)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from roles t where ( t.roleId = {0})"
                                        , roleid);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public RoleInfo GetRoleByRoleId(int roleid)
        {
            DataTable tb = getRoleByRoleId(roleid);
            RoleInfo info = new RoleInfo();
            if (tb != null && tb.Rows.Count > 0)
            {
                info.RoleId = int.Parse(tb.Rows[0][0].ToString());
                info.RoleName = tb.Rows[0][1].ToString();
                info.PermissionCodes = tb.Rows[0][2].ToString();
                info.CreateTime = DateTime.Parse(tb.Rows[0][3].ToString());
                info.Remark = tb.Rows[0][4].ToString();
                return info;
            }
            else
            {
                throw new Exception("执行 getRoleByRoleId 查询到空值");
            }
            
        }

        public void UpdateRoles(RoleInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"update roles t 
                    set
			        t.roleName = '{1}',
			        t.permissionCodes = '{2}',
			        t.remark = '{3}'
                    where t.roleId = {0}"
              , info.RoleId, info.RoleName, info.PermissionCodes, info.Remark);
            equipDB.ExecuteScalar(CommandType.Text, sql);
        }

        public void DeleteRole(int RoleId)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"delete from roles  where roleId = {0}"
              , (RoleId));
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void InsertRoles(RoleInfo role)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"insert roles (roleName, permissionCodes, createTime, remark)
                                            values('{0}', '{1}', '{2}', '{3}'); "
                                            , role.RoleName, role.PermissionCodes, role.CreateTime, role.Remark);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void UpdateUsers(UserInfo info)
        {
            if (!logCell) return;
            Database equipDB = dataProvider.EQUIPDataBase;
            if(info.Gender.HasValue)
            {
                string sql = string.Format(
                @"update users t 
                    set
			        t.userName = '{1}',
			        t.password = '{2}',
			        t.name = '{3}',
                    t.roleId = {4},
			        t.createTime = '{5}',
			        t.gender = {6},
			        t.craft_dids = '{7}'
                    where t.userId = {0}"
              , info.UserId, info.UserName, info.Password, info.Name, info.RoleId, info.CreateTime.ToString()
              , (int)info.Gender, info.CraftDIDs);
                equipDB.ExecuteScalar(CommandType.Text, sql);
            }
            else
            {
                string sql = string.Format(
                @"update users t 
                    set
			        t.userName = '{1}',
			        t.password = '{2}',
			        t.name = '{3}',
                    t.roleId = {4},
			        t.createTime = '{5}',
			        t.craft_dids = '{6}'
                    where t.userId = {0}"
              , info.UserId, info.UserName, info.Password, info.Name, info.RoleId, info.CreateTime.ToString()
              , info.CraftDIDs);
                equipDB.ExecuteScalar(CommandType.Text, sql);
            }
        }

        public void UpdateUserLastLoginTime(UserInfo info)
        {
            if (!logCell) return;
            Database equipDB = dataProvider.EQUIPDataBase;
            if(info.Gender.HasValue)
            {
                string sql = string.Format(
                @"update users t 
                    set
			        t.userName = '{1}',
			        t.password = '{2}',
			        t.name = '{3}',
                    t.roleId = {4},
			        t.createTime = '{5}',
			        t.gender = {6},
                    t.LastLoginTime = '{7}',
			        t.craft_dids = '{8}'
                    where t.userId = {0}"
              , info.UserId, info.UserName, info.Password, info.Name, info.RoleId, info.CreateTime.ToString()
              , (int)info.Gender, info.LastLoginTime.ToString(), info.CraftDIDs);
                equipDB.ExecuteScalar(CommandType.Text, sql);
            }
            else
            {
                string sql = string.Format(
                @"update users t 
                    set
			        t.userName = '{1}',
			        t.password = '{2}',
			        t.name = '{3}',
                    t.roleId = {4},
			        t.createTime = '{5}',
                    t.LastLoginTime = '{6}',
			        t.craft_dids = '{7}'
                    where t.userId = {0}"
              , info.UserId, info.UserName, info.Password, info.Name, info.RoleId, info.CreateTime.ToString()
              , info.LastLoginTime.ToString(), info.CraftDIDs);
                equipDB.ExecuteScalar(CommandType.Text, sql);
            }
        }

        private DataTable getAllPermissions()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from permissions Order by ParentId, DisplayOrder";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public IList<PermissionInfo> GetAllPermissions()
        {
            List<PermissionInfo> lst = new List<PermissionInfo>();
            DataTable tb = getAllPermissions();
            if(tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    PermissionInfo info = new PermissionInfo();

                    info.PermissionId = int.Parse(row[0].ToString());
                    info.PermissionName = row[1].ToString();
                    info.PermissionCode = row[2].ToString();
                    info.Remark = row[3].ToString();
                    info.DisplayOrder = int.Parse(row[4].ToString());
                    if (string.IsNullOrEmpty(row[5].ToString()))
                        info.ParentId = null;
                    else
                        info.ParentId = int.Parse(row[5].ToString());
                    info.Depth = int.Parse(row[6].ToString());
                    
                    lst.Add(info);
                }

                foreach (PermissionInfo info in lst)
                {
                    if (info.ParentId != null)
                    {
                        info.Parent = lst.Where(m => m.PermissionId == info.ParentId).First();
                    }
                }

                foreach (PermissionInfo info1 in lst)
                {
                    foreach (PermissionInfo info2 in lst)
                    {
                        if (info1.PermissionId == info2.ParentId)
                        {
                            info1.Children.Add(info2);
                        }
                    }
                }
            }

            return lst;
        }

        private DataTable getUserByUserID(int UserId)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from users t where t.userId = {0}"
                                        , UserId);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public UserInfo GetUserByUserID(int UserId)
        {
            DataTable tb = getUserByUserID(UserId);
            UserInfo info = new UserInfo();
            if (tb != null && tb.Rows.Count > 0)
            {
                info.UserId = int.Parse(tb.Rows[0][0].ToString());
                info.UserName = tb.Rows[0][1].ToString();
                info.Password = tb.Rows[0][2].ToString();
                info.Name = tb.Rows[0][3].ToString();
                info.RoleId = int.Parse(tb.Rows[0][4].ToString());
                info.CreateTime = DateTime.Parse(tb.Rows[0][5].ToString());
                if (tb.Rows[0][6].ToString() != "")
                    info.Gender = (Gender)Enum.Parse(typeof(Gender), tb.Rows[0][6].ToString());
                if (tb.Rows[0][7].ToString() != "")
                    info.LastLoginTime = DateTime.Parse(tb.Rows[0][7].ToString());
                info.CraftDIDs = tb.Rows[0][8].ToString();
                info.Role = GetRoleByRoleId(info.RoleId);
                return info;
            }
            else
            {
                throw new Exception("执行 getUserByUserID 查询到空值");
            }
        }

        private DataTable getPermission(int permissionId)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from permissions t where t.PermissionId = {0}"
                                        , permissionId);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public PermissionInfo GetPermission(int permissionId)
        {
            List<PermissionInfo> lst = GetAllPermissions().ToList();
            foreach(PermissionInfo var in lst)
            {
                if (var.PermissionId == permissionId)
                    return var;
            }
            return null;
        }

        public void UpdatePermissions(PermissionInfo info)
        {
            if (!logCell) return;
            Database equipDB = dataProvider.EQUIPDataBase;
            if(info.ParentId.HasValue)
            {
                string sql = string.Format(
                @"update permissions t 
                    set
			        t.PermissionName = '{1}',
			        t.PermissionCode = '{2}',
			        t.Remark = '{3}',
                    t.DisplayOrder = {4},
			        t.ParentId = {5},
			        t.Depth = {6}
                    where t.PermissionId = {0}"
              , info.PermissionId, info.PermissionName, info.PermissionCode, info.Remark, info.DisplayOrder, info.ParentId, info.Depth);
                equipDB.ExecuteScalar(CommandType.Text, sql);
            }
            else
            {
                string sql = string.Format(
                @"update permissions t 
                    set
			        t.PermissionName = '{1}',
			        t.PermissionCode = '{2}',
			        t.Remark = '{3}',
                    t.DisplayOrder = {4},
			        t.Depth = {5}
                    where t.PermissionId = {0}"
              , info.PermissionId, info.PermissionName, info.PermissionCode, info.Remark, info.DisplayOrder, info.Depth);
                equipDB.ExecuteScalar(CommandType.Text, sql);
            }
        }

        public void UpdatePermissions(IList<PermissionInfo> permissionInfos)
        {
            foreach(PermissionInfo var in permissionInfos)
            {
                UpdatePermissions(var);
            }
        }

        public IList<PermissionInfo> GetPermissionsByParentId(int? parentId)
        {
            List<PermissionInfo> lst = GetAllPermissions().ToList();
            return (from m in lst
                    where m.ParentId == parentId
                    orderby m.DisplayOrder
                    select m).ToList();
        }

        public void InsertUsersInfo(UserInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"insert usersInfo (craftwork, process, quarters, segments, staffid, time)
                                        values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'); "
                                        , info.craftwork, info.process, info.quarters, info.segments, info.staffid, DateTime.Now);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void InsertfactoryInfo(UserInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@" update factoryinfo set factoryID = '{0}', fano = '{1}', end_product_no = '{2}', time = Now(); "
                                        , info.factoryID, info.fano, info.end_product_no);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }


    }
}
