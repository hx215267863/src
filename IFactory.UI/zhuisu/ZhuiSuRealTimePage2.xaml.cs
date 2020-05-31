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
using IFactory.Domain.Models;
using IFactory.UI.Core;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Response.Alarm;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System.Windows.Markup;

namespace IFactory.UI.zhuisu
{
    /// <summary>
    /// ZhuiSuRealTimePage.xaml 的交互逻辑
    /// </summary>
    public partial class ZhuiSuRealTimePage2 : BasePage, IComponentConnector
    {
        
        public ZhuiSuRealTimePage2()
        {
            InitializeComponent();

            this.DataContext = this;
        }
        public int AlarmTemporaryDID { get; set; }
        public int? ProcessDID { get; set; }

        public ICommand ViewDetailCommand { get; set; }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<ZhuiSuItem>(new Action<ZhuiSuItem>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            ZhuiSuResponse zhuiSuResponse = LocalApi.Execute2(new ZhuiSuRequest()
            {
                ProcessDID = this.ProcessDID,
                PageNumber = this.pager.PageNumber,
                PageSize = 10
            });
            this.pager.Setup(zhuiSuResponse.ZhuiSus);
            this.dataGrid.ItemsSource = zhuiSuResponse.ZhuiSus;
        }

        private void ViewDetail(ZhuiSuItem item)
        {
            this.NavigationService.Navigate(new ZhuiSuRealTimePage()
            {
                AlarmTemporaryDID = item.Iden
            });
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }
        
    }
}

