using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Common;
using IFactory.Domain.Crafts.Base.Models;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Crafts;
using IFactory.Platform.Common.Response.Crafts;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Input;

namespace IFactory.UI.CraftDetails
{
    /// <summary>
    /// CraftDetailPage1.xaml 的交互逻辑
    /// </summary>
    public partial class CraftDetailPage12 : BasePage, IComponentConnector
    {
        public CraftDetailPage12()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public int AlarmTemporaryDID { get; set; }
        public int? ProcessDID { get; set; }

        public ICommand ViewDetailCommand { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<Detail1Item>(new Action<Detail1Item>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            Detail1Response detailResponse = LocalApi.ExecuteDetail12(new Detail1Request()
            {
                ProcessDID = this.ProcessDID,
                PageNumber = this.pager.PageNumber,
                PageSize = 10
            });

            this.pager.Setup(detailResponse.Detail1s);
            this.dataGrid.ItemsSource = detailResponse.Detail1s;
        }

        private void ViewDetail(Detail1Item item)
        {
            this.NavigationService.Navigate(new CraftDetailPage1()
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
