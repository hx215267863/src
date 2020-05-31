using IFactory.UI.Core;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;


namespace IFactory.UI.UserManager
{
    /// <summary>
    /// ChangePasswordDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePasswordDialog : Window, IComponentConnector
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOldPassword.Password))
            {
                MessageBox.Show("请输入旧密码", "提示");
            }
            else if (string.IsNullOrEmpty(this.txtNewPassword.Password))
            {
                MessageBox.Show("请输入新密码", "提示");
            }
            else if (string.IsNullOrEmpty(this.txtNewPasswordConfirm.Password))
            {
                MessageBox.Show("请输入新密码确认", "提示");
            }
            else if (this.txtNewPassword.Password != this.txtNewPasswordConfirm.Password)
            {
                MessageBox.Show("两次输入的密码不一致", "提示");
            }
            else
            {
                ChangePasswordResponse passwordResponse =  LocalApi.Execute(
                    new ChangePasswordRequest() {
                        UserId = AppContext.Current.UserId,
                        OldPassword = this.txtOldPassword.Password,
                        NewPassword = this.txtNewPassword.Password });

                if (passwordResponse.IsError)
                {
                    int num5 = (int)MessageBox.Show(passwordResponse.ErrMsg, "提示");
                }
                else
                {
                    this.DialogResult = new bool?(true);
                    this.Close();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition((IInputElement)this)) || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }
    }
}
