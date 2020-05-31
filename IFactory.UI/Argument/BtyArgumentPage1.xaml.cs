using System;
using System.Windows;
using System.Windows.Input;
using IFactory.UI.Controls;
using IFactory.Domain.Models;
using IFactory.UI.Core;
using IFactory.Platform.Common.Request.Setting;
using IFactory.Platform.Common.Response.Setting;
using System.Windows.Markup;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// SetArgumentPage.xaml 的交互逻辑
    /// </summary>
    public partial class BtyArgumentPage1 : BasePage, IComponentConnector
    {

        public static string strName { get; set; }

        public BtyArgumentPage1()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public ICommand ViewDetailCommand { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<ArgumentItem>(new Action<ArgumentItem>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            ArgumentResponse argumentResponse = LocalApi.ExecuteArgument1(new ArgumentRequest()
            {
                PageNumber = this.pager.PageNumber,
                PageSize = 10
            });
            this.pager.Setup(argumentResponse.Augus);
            this.dataGrid.ItemsSource = argumentResponse.Augus;
        }

        private void ViewDetail(ArgumentItem item)
        {
            //this.NavigationService.Navigate(new BtyArgumentPage()
            //{
                //AlarmTemporaryDID = item.Name
            //});
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }

    }
}