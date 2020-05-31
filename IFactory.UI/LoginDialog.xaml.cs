using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;
using IFactory.UI.Controls;
using IFactory.UI.UserManager;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// AlarmUnitAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDialog : Window, IComponentConnector
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void Far_button_Click(object sender, RoutedEventArgs e)
        {
            UserCheckDialog userCheck= new UserCheckDialog();
            userCheck.Show();
            this.Close();
        }

        private void Local_button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LW = new LoginWindow();
            LW.Show();
            LW.WindowState = WindowState.Maximized;
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this)) || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
