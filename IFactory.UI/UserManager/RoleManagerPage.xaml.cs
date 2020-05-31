using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// RoleManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class RoleManagerPage : BasePage, IComponentConnector
    {
        public RoleManagerPage()
        {
            InitializeComponent();

            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            this.EditCommand = new RouteCommand<RoleModel>(new Action<RoleModel>(this.EditRole));
            this.DeleteCommand = new RouteCommand<RoleModel>(new Action<RoleModel>(this.DeleteRole));
            this.DataContext = this;
        }

        public ICommand DeleteCommand { get; set; }

        public ICommand EditCommand { get; set; }
        

        private void EditRole(RoleModel user)
        {
            bool? nullable = new RoleAddDialog() { RoleId = user.RoleId }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void DeleteRole(RoleModel model)
        {
            if (MessageBox.Show("确认删除该角色吗？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            RoleDeleteResponse roleDeleteResponse = LocalApi.Execute(new RoleDeleteRequest() { RoleId = model.RoleId });
            this.RefreshData();
        }

        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }

        public void RefreshData()
        {
            RoleListResponse roleListResponse = LocalApi.Execute(new RoleListRequest() { PageNumber = this.pager.PageNumber, PageSize = 10 });
            this.pager.Setup(roleListResponse.Roles);
            this.dataGrid.ItemsSource = roleListResponse.Roles;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new RoleAddDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }
    }
}
