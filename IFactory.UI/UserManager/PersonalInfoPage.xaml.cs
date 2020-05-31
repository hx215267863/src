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
using System.Windows.Markup;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// PersonalInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInfoPage : BasePage, IComponentConnector
    {
        public PersonalInfoPage()
        {
            InitializeComponent();
        }

        public UserModel model;

        public void RefreshData()
        {
            UserGetResponse userGetResponse = LocalApi.Execute(new UserGetRequest() { UserId = AppContext.Current.UserId });
            if (userGetResponse.User == null)
                return;
            this.dataGrid.ItemsSource = (new UserModel[1] { userGetResponse.User });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnEditInfo_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new PersonalInfoEditDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordDialog().ShowDialog();
        }
    }
}
