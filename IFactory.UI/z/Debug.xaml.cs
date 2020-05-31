using IFactory.UI.Controls;
using System.Windows;
using System.Windows.Markup;
using System;

namespace IFactory.UI.Debug
{
    /// <summary>
    /// Debug.xaml 的交互逻辑
    /// </summary>
    public partial class Debug : BasePage, IComponentConnector
    {
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }
    }
}
