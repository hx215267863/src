using System;
using System.Windows;
using System.Windows.Input;
using IFactory.Domain.Models;
using IFactory.UI.Core;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System.Windows.Markup;
using IFactory.UI.Controls;


namespace IFactory.UI.DataWareHouse
{
    /// <summary>
    /// ProductStatementNGPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductStatementNGPage : BasePage, IComponentConnector
    {
        public ProductStatementNGPage()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public int AlarmTemporaryDID { get; set; }
        public int? ProcessDID { get; set; }

        public ICommand ViewDetailCommand { get; set; }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<ProductNGItem>(new Action<ProductNGItem>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            ProductNGResponse temporaryListResponse = LocalApi.Execute(new ProductNGRequest()
            {
                ProcessDID = this.ProcessDID,
                PageNumber = this.pager.PageNumber,
                PageSize = 10
            });
            this.pager.Setup(temporaryListResponse.productNGs);
            this.dataGrid.ItemsSource = temporaryListResponse.productNGs;
        }

        private void ViewDetail(ProductNGItem item)
        {
            this.NavigationService.Navigate(new ProductStatementNGPage()
            {
                AlarmTemporaryDID = int.Parse(item.DeviceNo)
            });
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }
    }
}
