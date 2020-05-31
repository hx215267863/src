using IFactory.UI.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IFactory.UI.CraftDetails;
using IFactory.UI.Argument;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// CraftDetailsPage.xaml 的交互逻辑
    /// </summary>
    public partial class BtyArgumentPage : BaseCraftPage, IComponentConnector
    {
        public BtyArgumentPage()
        {
            InitializeComponent();
        }

        public string AlarmTemporaryDID { get; set; }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)(sender as Button).Tag;
            if (!(str == "1"))
            {
                if (!(str == "2"))
                {
                    if (!(str == "3"))
                    {
                        if (!(str == "4"))
                            return;
                        BtyArgumentPage4.strName = this.AlarmTemporaryDID;
                        BasePage baseCraftDetailPage = new BtyArgumentPage4();
                        this.detailPage.Navigate(baseCraftDetailPage);
                        this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs4.png", UriKind.Absolute)));
                    }
                    else
                    {
                        BtyArgumentPage3.strName = this.AlarmTemporaryDID;
                        BasePage baseCraftDetailPage = new BtyArgumentPage3();
                        this.detailPage.Navigate(baseCraftDetailPage);
                        this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs3.png", UriKind.Absolute)));
                    }
                }
                else
                {
                    BtyArgumentPage2.strName = this.AlarmTemporaryDID;
                    BasePage baseCraftDetailPage = new BtyArgumentPage2();
                    this.detailPage.Navigate(baseCraftDetailPage);
                    this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs2.png", UriKind.Absolute)));
                }
            }
            else
            {
                BtyArgumentPage1.strName = this.AlarmTemporaryDID;
                BasePage baseCraftDetailPage = new BtyArgumentPage1();
                this.detailPage.Navigate(baseCraftDetailPage);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs1.png", UriKind.Absolute)));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BtyArgumentPage1.strName = this.AlarmTemporaryDID;
            BasePage baseCraftDetailPage = new BtyArgumentPage1();
            this.detailPage.Navigate(baseCraftDetailPage);
        }
    }
}
