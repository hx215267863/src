using IFactory.UI.Core;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// RoleAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class RoleAddDialog : Window, IComponentConnector
    {
        public RoleAddDialog()
        {
            InitializeComponent();
        }

        public class MenuNode
        {
            private IList<MenuNode> children;

            public string Text { get; set; }

            public string Code { get; set; }

            public bool IsChecked { get; set; }

            public IList<MenuNode> Children
            {
                get
                {
                    return this.children ?? (this.children = new List<MenuNode>());
                }
                set
                {
                    this.children = value;
                }
            }
        }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Remark { get; set; }

        public IList<MenuNode> Menus { get; set; }
        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.RoleName))
            {
                int num = (int)MessageBox.Show("请输入名称", "提示");
            }
            else
            {
                string str = string.Join(",", this.GetCheckedMenus(this.Menus).Where<RoleAddDialog.MenuNode>(m => !string.IsNullOrEmpty(m.Code)).Select(m => m.Code));
                RoleSaveResponse roleSaveResponse = LocalApi.Execute(new RoleSaveRequest() { RoleName = this.RoleName, RoleId = this.RoleId, Remark = Remark, PermissionCodes = str });
                this.DialogResult = new bool?(true);
                this.Close();
            }
        }

        private IList<MenuNode> GetCheckedMenus(IList<MenuNode> menus)
        {
            List<MenuNode> menuNodeList = new List<MenuNode>();
            foreach (MenuNode menu in menus)
            {
                if (menu.IsChecked)
                    menuNodeList.Add(menu);
                if (menu.Children.Count > 0)
                {
                    IList<MenuNode> checkedMenus = this.GetCheckedMenus(menu.Children);
                    menuNodeList.AddRange(checkedMenus);
                }
            }
            return menuNodeList;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this)) || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.RoleId > 0)
            {
                RoleGetResponse roleGetResponse = LocalApi.Execute(new RoleGetRequest() { RoleId = this.RoleId });
                this.dialogTitle.Content = "修改角色";
                if (roleGetResponse.Role != null)
                {
                    this.RoleName = roleGetResponse.Role.RoleName;
                    this.Remark = roleGetResponse.Role.Remark;
                    List<string> permissionCodes = ((IEnumerable<string>)(roleGetResponse.Role.PermissionCodes ?? "").Split(',')).Where<string>(m => !string.IsNullOrEmpty(m)).ToList();
                    this.Menus = this.BuildMenuCodes(PermissionNode.BuildPermissionNodes((LocalApi.Execute(new PermissionListRequest())).Permissions), permissionCodes);
                    permissionCodes = null;
                }
            }
            else
            {
                this.dialogTitle.Content = "添加角色";
                this.Menus = this.BuildMenuCodes(PermissionNode.BuildPermissionNodes((LocalApi.Execute(new PermissionListRequest())).Permissions), new List<string>());
            }
            this.DataContext = this;
        }

        public IList<RoleAddDialog.MenuNode> BuildMenuCodes(IList<PermissionNode> permissionNodes, List<string> permissionCodes)
        {
            List<RoleAddDialog.MenuNode> menuNodeList1 = new List<RoleAddDialog.MenuNode>();
            foreach (PermissionNode permissionNode in permissionNodes)
            {
                RoleAddDialog.MenuNode menuNode1 = new MenuNode();
                menuNode1.Text = permissionNode.Text;
                menuNode1.Code = permissionNode.Code;
                int num = permissionCodes.Contains(permissionNode.Code) ? 1 : 0;
                menuNode1.IsChecked = num != 0;
                IList<MenuNode> menuNodeList2 = this.BuildMenuCodes(permissionNode.Children, permissionCodes);
                menuNode1.Children = menuNodeList2;
                MenuNode menuNode2 = menuNode1;
                menuNodeList1.Add(menuNode2);
            }
            return menuNodeList1;
        }
    }
}
