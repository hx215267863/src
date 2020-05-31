using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IFactory.UI
{
    /// <summary>
    /// ShowPictureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPictureWindow : Window, IComponentConnector
    {
        public ShowPictureWindow()
        {
            InitializeComponent();
        }

        public Uri ImageUri { get; set; }

        private void btnScreen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //指定本地图片文件路径
            this.image.Source = new BitmapImage(this.ImageUri);
        }
    }
}
