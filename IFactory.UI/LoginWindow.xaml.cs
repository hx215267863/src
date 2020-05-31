/*
登陆界面窗口代码
*/

using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using IFactory.UI.Core;
using System.Collections.Generic;
using System.Linq;


namespace IFactory.UI
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window, IComponentConnector
    {
        public LoginWindow()
        {
            //界面初始化
            InitializeComponent();

            txtUserName.Focus();


#if DEBUG
            this.txtUserName.Text = "admin";
            this.txtPassword.Password = "admin";
            btnLogin_Click(null, null);

#endif
        }

        //界面关闭按键
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //界面最小化按键
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //界面最大化按键
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        //点击登陆按键
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserName.Text))
            {
                MessageBox.Show("请输入用户名", "提示");
            }
            else if (string.IsNullOrEmpty(this.txtPassword.Password))
            {
                MessageBox.Show("请输入密码", "提示");
            }
            //已输入用户名和密码
            else
            {
                LoginResponse loginResponse = LocalApi.Execute(new LoginRequest()
                {
                    UserName = this.txtUserName.Text,
                    Password = this.txtPassword.Password
                });



                //如果用户名和密码正确
                if (!loginResponse.IsError )
                {

                    AppContext.Current.Reset();
                    AppContext.Current.Name = loginResponse.Name;
                    AppContext.Current.UserId = loginResponse.UserId;
                    if (!string.IsNullOrEmpty(loginResponse.PermissionCodes))
                        AppContext.Current.PermissionCodes.AddRange(loginResponse.PermissionCodes.Split(','));

                    if (!string.IsNullOrEmpty(loginResponse.CraftDIDs))
                        AppContext.Current.CraftDIDs.AddRange(((IEnumerable<string>)loginResponse.CraftDIDs.Split(',')).Select<string, int>(m => int.Parse(m)));

                    //---哈哈哈

                    //显示主主界面同时关闭登陆界面
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.labelWN.Content = loginResponse.Name.ToString();
                    mainWindow.Show();
                    Application.Current.MainWindow = mainWindow;
                    this.Close();

                }
                else
                {
                    MessageBox.Show(loginResponse.ErrMsg, "提示");
                }
            }
        }
        private bool WithOutLogin()
        {
            bool flag = false;
#if DEBUG
            flag = true;
#endif
            return flag;
        }


        //可用鼠标拖动窗口
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.WindowState != WindowState.Normal
                || (!new Rect(new Point(0.0, 0.0), new Size(this.Width, this.Height * (7.0 / 225.0))).Contains(e.GetPosition(this)) || e.ChangedButton != MouseButton.Left))
                return;
            this.DragMove();
        }
    }


}
