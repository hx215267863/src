using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ATL_MC.Vision;

namespace WpfApplicationTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ATL_MC.Vision.Vision aaa = new Vision();
        private ATL_MC.Vision.BatteryVisionConfig zzz = new BatteryVisionConfig();
        public MainWindow()
        {
            InitializeComponent();
            zzz.product = "123456";
            aaa.SetBatteryType(zzz);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
