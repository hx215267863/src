using IFactory.UI.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// AlarmConfigMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmConfigMainPage : BasePage, IComponentConnector
    {
        public AlarmConfigMainPage()
        {
            InitializeComponent();
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)(sender as Button).Tag;
            if (!(str == "2"))
            {
                if (!(str == "1"))
                    return;
                this.detailPage.Navigate(new AlarmAddRulePage());
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tab_alarm1.png", UriKind.Absolute)));
            }
            else
            {
                this.detailPage.Navigate(new AlarmAddFieldPage());
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tab_alarm2.png", UriKind.Absolute)));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.detailPage.Navigate(new AlarmAddRulePage());
        }
    }
}
