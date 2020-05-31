using IFactory.UI.Core;
using IFactory.Common;
using IFactory.Domain.Common;
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
    /// PersonalInfoEditDialog.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInfoEditDialog : Window, IComponentConnector
    {
        public PersonalInfoEditDialog()
        {
            InitializeComponent();
        }

        private UserModel model;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.model.Name))
            {
                int num = (int)MessageBox.Show("请输入姓名", "提示");
            }
            else
            {
                PersonalInfoUpdateResponse infoUpdateResponse = LocalApi.Execute(new PersonalInfoUpdateRequest() {
                        Gender = this.model.Gender,
                        Name = this.model.Name,
                        UserId = this.model.UserId });
                this.DialogResult = new bool?(true);
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this)) 
                || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = (LocalApi.Execute(new UserGetRequest()
            {
                UserId = AppContext.Current.UserId
            })).User;
            this.ddlGender.ItemsSource = Gender.Female.ToArrayList();
            this.DataContext = this.model;
        }
    }
}
