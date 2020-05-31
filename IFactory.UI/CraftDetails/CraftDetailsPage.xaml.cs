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

namespace IFactory.UI.CraftDetails
{
    /// <summary>
    /// CraftDetailsPage.xaml 的交互逻辑
    /// </summary>
    public partial class CraftDetailsPage : BaseCraftPage, IComponentConnector
    {
        public CraftDetailsPage()
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
                    if (!(str == "3"))
                    {
                        if (!(str == "4"))
                            return;
                        BaseCraftDetailPage baseCraftDetailPage = new CraftDetailPage4();
                        baseCraftDetailPage.CraftDID = this.CraftDID;
                        baseCraftDetailPage.CraftNO = this.CraftNO;
                        this.detailPage.Navigate(baseCraftDetailPage);
                        this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs4.png", UriKind.Absolute)));
                    }
                    else
                    {
                        BaseCraftDetailPage baseCraftDetailPage = new CraftDetailPage3();
                        baseCraftDetailPage.CraftDID = this.CraftDID;
                        baseCraftDetailPage.CraftNO = this.CraftNO;
                        this.detailPage.Navigate(baseCraftDetailPage);
                        this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs3.png", UriKind.Absolute)));
                    }
                }
                else
                {
                    BaseCraftDetailPage baseCraftDetailPage = new CraftDetailPage2();
                    baseCraftDetailPage.CraftDID = this.CraftDID;
                    baseCraftDetailPage.CraftNO = this.CraftNO;
                    this.detailPage.Navigate(baseCraftDetailPage);
                    this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs2.png", UriKind.Absolute)));
                }
            }
            else
            {
                BasePage baseCraftDetailPage = new CraftDetailPage1();
                //baseCraftDetailPage.CraftDID = this.CraftDID;
                //baseCraftDetailPage.CraftNO = this.CraftNO;
                this.detailPage.Navigate(baseCraftDetailPage);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs1.png", UriKind.Absolute)));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasePage baseCraftDetailPage = new CraftDetailPage1();
            //baseCraftDetailPage.CraftDID = this.CraftDID;
            //baseCraftDetailPage.CraftNO = this.CraftNO;
            this.detailPage.Navigate(baseCraftDetailPage);
        }
    }
}
