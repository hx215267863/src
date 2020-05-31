using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.CodeDom.Compiler;
using System.Collections;
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
    /// MenuManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class MenuManagementPage : BasePage, IComponentConnector
    {
        public MenuManagementPage()
        {
            InitializeComponent();

            this.EditCommand = new RouteCommand<PermissionModel>(new Action<PermissionModel>(this.EditMenu));
            this.UpCommand = new RouteCommand<PermissionModel>(new Action<PermissionModel>(this.UpMenu));
            this.DownCommand = new RouteCommand<PermissionModel>(new Action<PermissionModel>(this.DownMenu));
            this.DataContext = this;
        }

        public ICommand DownCommand { get; set; }
        public ICommand UpCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private void EditMenu(PermissionModel user)
        {
            bool? nullable = new MenuEditDialog() { PermissionId = user.PermissionId }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void UpMenu(PermissionModel model)
        {
            PermissionOrderResponse permissionOrderResponse = LocalApi.Execute(new PermissionOrderRequest()
            {
                PermissionId = model.PermissionId,
                Direction = PermissionOrderRequest.DirectionType.Up
            });
            this.RefreshData();
        }

        private void DownMenu(PermissionModel model)
        {
            PermissionOrderResponse permissionOrderResponse = LocalApi.Execute(new PermissionOrderRequest()
            {
                PermissionId = model.PermissionId,
                Direction = PermissionOrderRequest.DirectionType.Down
            });
            this.RefreshData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            PermissionListResponse permissionListResponse = LocalApi.Execute(new PermissionListRequest());

            if (permissionListResponse.Permissions == null)
                return;
            List<PermissionModel> permissionModelList = new List<PermissionModel>();
            //重新排序
            foreach (PermissionModel permissionModel in permissionListResponse.Permissions.Where(m => !m.ParentId.HasValue))
            {
                PermissionModel tm = permissionModel;
                permissionModelList.Add(tm);  //先显示根对象
                permissionModelList.AddRange(permissionListResponse.Permissions.Where(m =>
                {
                    int? parentId = m.ParentId;
                    int permissionId = tm.PermissionId;
                    if (parentId.GetValueOrDefault() != permissionId)
                        return false;
                    return parentId.HasValue;
                }));  //再显示该根对象下的子对象
            }

            this.dataGrid.ItemsSource = permissionModelList;
            
        }
    }
}
