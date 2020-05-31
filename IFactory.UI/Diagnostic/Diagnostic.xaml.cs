using IFactory.UI.Controls;
using System.Windows;
using System.Windows.Markup;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IFactory.UI.Diagnostic;

namespace IFactory.UI.Diagnostic
{
    /// <summary>
    /// CraftDetailsPage.xaml 的交互逻辑
    /// </summary>
    public partial class Diagnostic : BaseCraftPage, IComponentConnector
    {
        
        private static BasePage baseCraftDetailPage1 = new IO();
        private static BasePage baseCraftDetailPage2 = new KeyencePLC();
        private static BasePage baseCraftDetailPage3 = new LightController();
        private static BasePage baseCraftDetailPage4 = new ScaraTest();
        private static BasePage baseCraftDetailPage5 = new Teach();

        public Diagnostic()
        {
            InitializeComponent();
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)(sender as Button).Tag;
          
            if (str == "1")
            {
                //BasePage baseCraftDetailPage = new IO();
                this.detailPage.Navigate(baseCraftDetailPage1);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs1.png", UriKind.Absolute)));
            }
            else if(str == "2")
            {
                //BasePage baseCraftDetailPage = new KeyencePLC();
                this.detailPage.Navigate(baseCraftDetailPage2);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs2.png", UriKind.Absolute)));
            }

            else if (str == "3")
            {
                //BasePage baseCraftDetailPage = new LightController();
                this.detailPage.Navigate(baseCraftDetailPage3);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs3.png", UriKind.Absolute)));
            }

            else if (str == "4")
            {
                //BasePage baseCraftDetailPage = new ScaraTest();
                this.detailPage.Navigate(baseCraftDetailPage4);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs4.png", UriKind.Absolute)));
            }

            else if (str == "5")
            {
                //BasePage baseCraftDetailPage = new Teach();
                this.detailPage.Navigate(baseCraftDetailPage5);
                this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs5.png", UriKind.Absolute)));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //BasePage baseCraftDetailPage = new IO();
            this.detailPage.Navigate(baseCraftDetailPage1);
            this.header.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/tabs1.png", UriKind.Absolute)));
        }
    }
}
