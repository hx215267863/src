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
    /// CraftDetailsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SetAlarmPage : BasePage, IComponentConnector
    {
        public SetAlarmPage()
        {
            InitializeComponent();
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)(sender as Button).Tag;
            if (!(str == "1"))
            {
                if (!(str == "2"))
                {
                }
                else
                {
                    BasePage basesetAlarmPage = new AlarmAddFieldPage();
                    this.detailPage.Navigate(basesetAlarmPage);
                    this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tab_alarm2.png", UriKind.Absolute)));
                }
            }
            else
            {
                BasePage basesetAlarmPage = new AlarmAddRulePage();
                this.detailPage.Navigate(basesetAlarmPage);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tab_alarm1.png", UriKind.Absolute)));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasePage basesetAlarmPage = new AlarmAddRulePage();
            this.detailPage.Navigate(basesetAlarmPage);
        }
    }
}
