using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Response.Alarm;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// RealTimeAlarmListPage.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeAlarmListPage : BasePage, IComponentConnector
    {
        public RealTimeAlarmListPage()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        /// <summary>
        /// 当前选择查看的工序ID，可为空，process表里的字段，MIB有6个process工序
        /// </summary>
        public int? ProcessDID { get; set; }

        public ICommand ViewDetailCommand { get; set; }

        static public int CraftDID { get; set; }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<AlarmTemporaryItem>(new Action<AlarmTemporaryItem>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            AlarmTemporaryListResponse temporaryListResponse = LocalApi.Execute(new AlarmTemporaryListRequest()
            {
                ProcessDID = this.ProcessDID,
                CraftsDid = CraftDID,
                PageNumber = this.pager.PageNumber,
                PageSize = 10
            });
            this.pager.Setup(temporaryListResponse.AlarmTemporaries);
            this.dataGrid.ItemsSource = temporaryListResponse.AlarmTemporaries;
        }

        private void ViewDetail(AlarmTemporaryItem item)
        {
            this.NavigationService.Navigate(new RealTimeAlarmDetailPage()
            {
                AlarmTemporaryDID = item.AlarmDid
            });
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }
    }
}
