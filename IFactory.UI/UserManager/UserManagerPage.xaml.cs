using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// UserManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagerPage : BasePage, IComponentConnector
    {
        public UserManagerPage()
        {
            InitializeComponent();

            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            this.EditCommand = (ICommand)new RouteCommand<UserModel>(new Action<UserModel>(this.EditUser));
            this.DeleteCommand = (ICommand)new RouteCommand<UserModel>(new Action<UserModel>(this.DeleteUser));
            this.DataContext = this;
        }

        public ICommand DeleteCommand { get; set; }

        public ICommand EditCommand { get; set; }
        

        private void EditUser(UserModel user)
        {
            bool? nullable = new UserAddDialog() { UserId = user.UserId }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void DeleteUser(UserModel model)
        {
            if (MessageBox.Show("确认删除该用户吗？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            UserDeleteResponse userDeleteResponse = LocalApi.Execute(new UserDeleteRequest() { UserId = model.UserId });
            this.RefreshData();
        }

        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }

        public void RefreshData()
        {
            UserListResponse userListResponse = LocalApi.Execute(new UserListRequest() { PageNumber = this.pager.PageNumber, PageSize = 10 });
            this.pager.Setup(userListResponse.Users);
            this.dataGrid.ItemsSource = userListResponse.Users;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new UserAddDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }
    }
}
