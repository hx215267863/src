using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// ProductionLineManagentPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductionLineManagentPage : BasePage, IComponentConnector
    {
        public ProductionLineManagentPage()
        {
            InitializeComponent();

            this.EditCommand = new RouteCommand<ProductionLineProbablyModel>(new Action<ProductionLineProbablyModel>(this.EditProductionLineProbably));
            this.CraftListCommand = new RouteCommand<ProductionLineProbablyModel>(new Action<ProductionLineProbablyModel>(this.ProductionLineList));
            this.DataContext = this;
        }

        public ICommand CraftListCommand { get; set; }

        public ICommand EditCommand { get; set; }
        

        private void EditProductionLineProbably(ProductionLineProbablyModel user)
        {
            bool? nullable = new ProductionLineEditDialog() { ProductionLineProbablyDID = user.DID }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void ProductionLineList(ProductionLineProbablyModel model)
        {
            this.NavigationService.Navigate(new CraftManagentPage());
        }

        public void RefreshData()
        {
            this.dataGrid.ItemsSource = new ProductionLineProbablyModel[1]
            {
                (LocalApi.Execute( new ProductionLineProbablyGetRequest(){ DID = 1 })
                ).ProductionLineProbably
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }
    }
}
