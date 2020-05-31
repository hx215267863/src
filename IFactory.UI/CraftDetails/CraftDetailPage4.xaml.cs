using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models.Crafts;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Crafts;
using IFactory.Platform.Common.Response.Crafts;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace IFactory.UI.CraftDetails
{
    /// <summary>
    /// CraftDetailPage4.xaml 的交互逻辑
    /// </summary>
    public partial class CraftDetailPage4 : BaseCraftDetailPage, IComponentConnector
    {
        public CraftDetailPage4()
        {
            InitializeComponent();
        }

        private void dateScaleBar_SelectedDateChanged(object sender, DateScaleBar.SelectedDateChangedEventArgs e)
        {
            this.RefreshData(e.SelectedDate);
        }

        public async void RefreshData(DateTime collectDate)
        {
            FacilityRunArgSumModel facilityRunArgSum = (await ClientHelper.ExecuteAsync<FacilityRunArgSumResponse>((IRequest<FacilityRunArgSumResponse>)new FacilityRunArgSumRequest() { CraftDID = this.CraftDID, CraftNO = this.CraftNO, CollectDate = collectDate })).FacilityRunArgSum;

            this.percentageBar1.UpdatePercentage(facilityRunArgSum.MCAutoRunTime, facilityRunArgSum.MCAutoRunTotalTime);
            this.percentageBar2.UpdatePercentage(facilityRunArgSum.MCBanCount, facilityRunArgSum.MCCount);
            this.percentageBar3.UpdatePercentage(facilityRunArgSum.MCRuningTime, facilityRunArgSum.MCRuningTotalTime);
            this.percentageBar4.UpdatePercentage(facilityRunArgSum.MCTotalBadCount, facilityRunArgSum.MCTotalCount);
            this.percentageBar5.UpdatePercentage(facilityRunArgSum.MCAutoRunWarningTime, facilityRunArgSum.MCAutoRunWarningTotalTime);
            this.percentageBar6.UpdatePercentage(facilityRunArgSum.MCOpenRunTime, facilityRunArgSum.MCOpenRunTotalTime);
            this.percentageBar7.UpdatePercentage(facilityRunArgSum.MCStopTime, facilityRunArgSum.MCStopTotalTime);
            this.percentageBar8.UpdatePercentage(facilityRunArgSum.MCWaitTime, facilityRunArgSum.MCWaitTotalTime);
        }

        private void BaseCraftDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshDateList();
        }

        private void dateScaleBar_DateRangeChanged(object sender, DateScaleBar.DateRangeChangedEventArgs e)
        {
            this.RefreshDateList();
        }

        private  void RefreshDateList()
        {
            FacilityRunArgDateListResponse dateListResponse =  LocalApi.Execute(new FacilityRunArgDateListRequest() { CraftDID = this.CraftDID, CraftNO = this.CraftNO, StartTime = this.dateScaleBar.StartDate, EndTime = this.dateScaleBar.EndDate });
            if (dateListResponse.IsError)
                return;
            this.dateScaleBar.SetDates(dateListResponse.CollectDates);
        }
    }
}
