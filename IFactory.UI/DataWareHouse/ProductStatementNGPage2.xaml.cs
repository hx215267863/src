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

using IFactory.Domain.Models;
using IFactory.UI.Core;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Response.Alarm;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System.Windows.Markup;

using IFactory.UI.Controls;




namespace IFactory.UI.DataWareHouse
{
    /// <summary>
    /// ProductStatementNGPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductStatementNGPage2 : BasePage, IComponentConnector
    {
        public ProductStatementNGPage2()
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
            ProductNGResponse temporaryListResponse = LocalApi.Execute2(new ProductNGRequest()
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
                AlarmTemporaryDID = item.MotorNG
            });
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }
    }
}
