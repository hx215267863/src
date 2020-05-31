using System;
using System.Windows;
using System.Windows.Input;
using IFactory.UI.Controls;
using IFactory.Domain.Models;
using IFactory.UI.Core;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System.Windows.Markup;

namespace IFactory.UI.zhuisu
{
    /// <summary>
    /// ZhuiSuRealTimePage.xaml 的交互逻辑
    /// </summary>
    public partial class ZhuiSuRealTimePage : BasePage, IComponentConnector
    {
        
        public ZhuiSuRealTimePage()
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
            ZhuiSuResponse zhuiSuResponse = LocalApi.Execute(new ZhuiSuRequest()
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

