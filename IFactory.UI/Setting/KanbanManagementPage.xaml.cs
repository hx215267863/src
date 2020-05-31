using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Common;
using IFactory.Domain.Common;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Setting;
using IFactory.Platform.Common.Response.Setting;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// KanbanManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class KanbanManagementPage : BasePage, IComponentConnector
    {
        public KanbanManagementPage()
        {
            InitializeComponent();
        }

        private KanbanSettingModel model;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ddlExcellentRateReportTimeSection.ItemsSource = TimeSectionType.Day.ToArrayList();
            this.ddlAlarmReportTimeSection.ItemsSource = TimeSectionType.Day.ToArrayList();
            this.ddlProductionReportTimeSection.ItemsSource = TimeSectionType.Day.ToArrayList();
            this.ddlRefreshInterval.ItemsSource = new Dictionary<int, string>(){
                { 5,"5秒" }, { 10,"10秒" }, { 15,"15秒" }, { 30,"30秒" }, { 45,"45秒" }, { 60,"1分钟" }, { 120,"2分钟" }, { 180,"3分钟" }, { 300,"5分钟" }};
            this.LoadModel();
        }

        private void LoadModel()
        {
            this.model = (LocalApi.Execute(new KanbanSettingGetRequest())).KanbanSetting;
            this.DataContext = this.model;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.LoadModel();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if ((LocalApi.Execute(new KanbanSettingSaveRequest() { KanbanSettingId = this.model.KanbanSettingId, ExcellentRateReportTimeSection = this.model.ExcellentRateReportTimeSection, AlarmReportTimeSection = this.model.AlarmReportTimeSection, ProductionReportTimeSection = this.model.ProductionReportTimeSection, RefreshInterval = this.model.RefreshInterval })).IsError)
            {
                MessageBox.Show("保存失败", "提示");
            }
            else
            {
                MessageBox.Show("保存成功", "提示");
            }
        }
    }
}
