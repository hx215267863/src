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
    /// CraftManagentPage.xaml 的交互逻辑
    /// </summary>
    public partial class CraftManagentPage : BasePage, IComponentConnector
    {
        public CraftManagentPage()
        {
            InitializeComponent();

            this.EditCommand = new RouteCommand<CraftDetailModel>(new Action<CraftDetailModel>(this.EditCraft));
            this.DataContext = this;
        }

        public ICommand EditCommand { get; set; }
        
        private void EditCraft(CraftDetailModel craft)
        {
            bool? nullable = new CraftEditDialog() { CraftDID = craft.CraftDID }.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        public void RefreshData()
        {
            this.dataGrid.ItemsSource = (LocalApi.Execute(new CraftDetailListRequest())).CraftDetails;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }
    }
}
