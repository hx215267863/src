using IFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IFactory.UI.Core
{
    public class PermissionNode
    {
        private IList<PermissionNode> children;

        public string Text { get; set; }

        public string Code { get; set; }

        public IList<PermissionNode> Children
        {
            get
            {
                return this.children ?? (this.children = (IList<PermissionNode>)new List<PermissionNode>());
            }
            set
            {
                this.children = value;
            }
        }

        public static IList<PermissionNode> BuildPermissionNodes(IList<PermissionModel> permissonModels)
        {
            List<PermissionNode> permissionNodeList = new List<PermissionNode>();
            foreach (PermissionModel permissionModel1 in permissonModels.Where<PermissionModel>((Func<PermissionModel, bool>)(m => !m.ParentId.HasValue)))
            {
                PermissionModel tm = permissionModel1;
                PermissionNode permissionNode = new PermissionNode();
                permissionNode.Code = tm.PermissionCode;
                permissionNode.Text = tm.PermissionName;
                foreach (PermissionModel permissionModel2 in permissonModels.Where<PermissionModel>((Func<PermissionModel, bool>)(m =>
                {
                    int? parentId = m.ParentId;
                    int permissionId = tm.PermissionId;
                    if (parentId.GetValueOrDefault() != permissionId)
                        return false;
                    return parentId.HasValue;
                })).ToList<PermissionModel>())
                    permissionNode.Children.Add(new PermissionNode()
                    {
                        Code = permissionModel2.PermissionCode,
                        Text = permissionModel2.PermissionName
                    });
                permissionNodeList.Add(permissionNode);
            }
            return (IList<PermissionNode>)permissionNodeList;
        }
    }
}
