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
using IFactory.UI.Controls;
using System.Windows.Markup;
namespace IFactory.UI.zhuisu
{
    /// <summary>
    /// DataProductionRealTimePage.xaml 的交互逻辑
    /// </summary>
    public partial class DataProductionRealTimePage : BasePage, IComponentConnector
    {
        public DataProductionRealTimePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {

        }
    }
}
