using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.SystemParam;
using IFactory.Platform.Common.Response.SystemParam;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using ATL_MC.DAL.Service;
using ATL_MC.DAL.Model;

namespace IFactory.UI.SystemParamManager
{
    /// <summary>
    /// UserManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductParamManagerPage : BasePage, IComponentConnector
    {
        public ProductParamManagerPage()
        {
            InitializeComponent();

            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            this.EditCommand = (ICommand)new RouteCommand<ProductDto>(new Action<ProductDto>(this.EditProductParam));
            this.DeleteCommand = (ICommand)new RouteCommand<ProductDto>(new Action<ProductDto>(this.DeleteProductParam));
            this.DataContext = this;
        }

        public ICommand DeleteCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ProductService _productService = new ProductService();

        private void EditProductParam(ProductDto productDto)
        {
            // bool? nullable = new ProductParamAddDialog() { ITEM_CD = productParam.ITEM_CD }.ShowDialog();
            bool? nullable = new ProductParamAddDialog(productDto,false) .ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData(textBox.Text);
        }

        private void DeleteProductParam(ProductDto productDto)
        {

            if (MessageBox.Show("确认删除该产品信息吗？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            //  ProductParamDeleteResponse productParamDeleteResponse = LocalApi.Execute(new ProductParamDeleteRequest() { ITEM_CD = productParam.ITEM_CD });
            _productService.RemoveProductInfo(productDto.ID);

            this.RefreshData(textBox.Text);
        }

        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData(textBox.Text);
        }

        public void RefreshData(string item_cd)
        {
            var pageList = new ProductService().GetProductInfo(this.pager.PageNumber, 10, item_cd);
            this.pager.Setup(pageList);
            this.dataGrid.ItemsSource = pageList;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData(textBox.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //TODO:
            bool? nullable = new ProductParamAddDialog(new ProductDto (),true) .ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData(textBox.Text);
        }

        private void bthCheck_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshData(textBox.Text);
            //
        }
    }
}
