using IFactory.UI.Controls;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IFactory.UI.Debug
{
    /// <summary>
    /// LightController.xaml 的交互逻辑
    /// </summary>
    public partial class LightController : BasePage, IComponentConnector
    {
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }

        private void buttonController1_Click(object sender, RoutedEventArgs e)
        {
            int v1 = Convert.ToInt32(textBoxCH11.Text);
            int v2 = Convert.ToInt32(textBoxCH12.Text);
            int v3 = Convert.ToInt32(textBoxCH13.Text);
            int v4 = Convert.ToInt32(textBoxCH14.Text);
            MainWindow.m_MainWindow.m_MainCrtl.mLightController1.SetLightBox(v1, v2, v3, v4);
        }

        private void buttonController2_Click(object sender, RoutedEventArgs e)
        {
            int v1 = Convert.ToInt32(textBoxCH21.Text);
            int v2 = Convert.ToInt32(textBoxCH22.Text);
            int v3 = Convert.ToInt32(textBoxCH23.Text);
            int v4 = Convert.ToInt32(textBoxCH24.Text);
            MainWindow.m_MainWindow.m_MainCrtl.mLightController2.SetLightBox(v1, v2, v3, v4);
        }
    }
}
