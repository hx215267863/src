using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace IFactory.UI.CraftDetails
{
    /// <summary>
    /// CraftDetailPage2.xaml 的交互逻辑
    /// </summary>
    public partial class CraftDetailPage2 : BaseCraftDetailPage, IComponentConnector
    {
        public CraftDetailPage2()
        {
            InitializeComponent();
        }

        private  void BaseCraftDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProductionTypeListResponse typeListResponse =  LocalApi.Execute(new ProductionTypeListRequest() { CraftDID = this.CraftDID });
            this.ddlProductNo.ItemsSource = typeListResponse.ProductionTypes;
            if (typeListResponse.ProductionTypes.Count <= 0)
                return;
            this.ddlProductNo.SelectedIndex = 0;
        }

        private void ddlProductNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ShowProductionType(((IList<ProductionTypeModel>)this.ddlProductNo.ItemsSource)[this.ddlProductNo.SelectedIndex]);
        }

        public void ShowProductionType(ProductionTypeModel productionTypeInfo)
        {
            this.txtBarCodeLen.Content = ("条码长度：" + productionTypeInfo.BarCodeLen);
            this.txtDefaultBarCode.Content = ("默认条码：" + productionTypeInfo.DefaultBarCode);
            this.txtMaxScope.Content = ("最大范围：" + productionTypeInfo.MaxScope);
            this.txtMaxWeight.Content = ("最大重量：" + productionTypeInfo.MaxWeight);
            this.txtMinScope.Content = ("最小范围：" + productionTypeInfo.MinScope);
            this.txtMinWeight.Content = ("最小重量：" + productionTypeInfo.MinWeight);
            this.txtPrefixData.Content = ("条码前缀：" + productionTypeInfo.PrefixData);
            this.txtPrefixLen.Content = ("条码前缀长度：" + productionTypeInfo.PrefixLen);
        }
    }
}
