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
using ATL_MC.DAL.Model;
using ATL_MC.DAL.Service;

namespace IFactory.UI.SystemParam
{
    /// <summary>
    /// UserManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class SystemParamManagerPage : BasePage, IComponentConnector
    {
        public SystemParamManagerPage()
        {
            InitializeComponent();

            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            this.EditCommand = (ICommand)new RouteCommand<ProductDetailDto>(new Action<ProductDetailDto>(this.EditSystemParam));
            this.DeleteCommand = (ICommand)new RouteCommand<ProductDetailDto>(new Action<ProductDetailDto>(this.DeleteSystemParam));
            this.DataContext = this;
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ProductService _productService = new ProductService();
        private void EditSystemParam(ProductDetailDto model)
        {
            bool? nullable = new SystemParamAddDialog(model, false) { }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData(textBox.Text);
        }

        private void DeleteSystemParam(ProductDetailDto model)
        {
            if (MessageBox.Show("确认删除该坐标信息吗？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            // SystemParamDeleteResponse userDeleteResponse = LocalApi.Execute(new SystemParamDeleteRequest() { ITEM_CD = model.ITEM_CD ,SLOT_SITE = model.SLOT_SITE});

            _productService.Removeproductdetail(model.ID);

            this.RefreshData(textBox.Text);
        }

        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData(textBox.Text);
        }

        public void RefreshData(string item_cd)
        {
            //SystemParamListResponse systemParamListResponse = LocalApi.Execute(new SystemParamListRequest()
            //{
            //    PageNumber = this.pager.PageNumber,
            //    PageSize = 10,
            //    ITEM_CD = item_cd
            //}
            //);
            var pageList = _productService.GetProductdetailInfo(this.pager.PageNumber, 10, textBox.Text.Trim());
            this.pager.Setup(pageList);
            this.dataGrid.ItemsSource = pageList;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData(textBox.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new SystemParamAddDialog(new ProductDetailDto(),true) .ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData(textBox.Text);
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshData(textBox.Text);
            //;
        }
    }
}
