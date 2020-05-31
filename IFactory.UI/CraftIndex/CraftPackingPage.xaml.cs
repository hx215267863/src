using IFactory.UI.AlarmMonitor;
using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.UI.CraftDetails;
using IFactory.Common;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace IFactory.UI.CraftIndex
{
    /// <summary>
    /// CraftPackingPage.xaml 的交互逻辑
    /// </summary>
    public partial class CraftPackingPage : BaseCraftIndexPage, IComponentConnector
    {
        public CraftPackingPage()
        {
            InitializeComponent();

            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 1.0,
                EndAngle = 35.0,
                Name = "UA001",
                Text = "顶封"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 37.0,
                EndAngle = 71.0,
                Name = "UB001",
                Text = "喷隐形码"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 73.0,
                EndAngle = 107.0,
                Name = "UC001",
                Text = "侧封"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 109.0,
                EndAngle = 143.0,
                Name = "UD001",
                Text = "出料"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 217.0,
                EndAngle = 251.0,
                Name = "UE001",
                Text = "上料检测"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 253.0,
                EndAngle = 287.0,
                Name = "UF001",
                Text = "折极耳"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 289.0,
                EndAngle = 323.0,
                Name = "UG001",
                Text = "铝膜冲坑/扫码"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 325.0,
                EndAngle = 359.0,
                Name = "UH001",
                Text = "电芯入壳"
            });
            this.stateCycle.ItemClick += new EventHandler<StateCycle.StateCycleItemClickEventArgs>(this.StateCycle_ItemClick);
            this.refreshTimer.Interval = new TimeSpan(0, 0, 30);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
        }

        private List<StateCycle.StateCycleItem> stateCycleItems = new List<StateCycle.StateCycleItem>();
        private DispatcherTimer refreshTimer = new DispatcherTimer();

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            this.RefreshStates();
            this.RefreshFacilityState();
        }

        private void StateCycle_ItemClick(object sender, StateCycle.StateCycleItemClickEventArgs e)
        {
            if (e.StateCycleItem.Tag == null || e.StateCycleItem.State != 3)
                return;
            int num = (int)e.StateCycleItem.Tag;
            this.NavigationService.Navigate(new RealTimeAlarmListPage()
            {
                ProcessDID = new int?(num)
            });
        }

        public async void RefreshStates()
        {
            foreach (ProcessModel process in (IEnumerable<ProcessModel>)(await ClientHelper.ExecuteAsync<ProcessListResponse>((IRequest<ProcessListResponse>)new ProcessListRequest() { CraftDID = this.CraftDID })).Processes)
            {
                string shortNo = CommonHelper.GetProcessShortNO(process.ProcessNO);
                StateCycle.StateCycleItem stateCycleItem = this.stateCycleItems.FirstOrDefault<StateCycle.StateCycleItem>(m => m.Name == shortNo);
                if (stateCycleItem != null)
                {
                    stateCycleItem.State = process.State;
                    stateCycleItem.Tag = process.ProcessDID;
                }
            }
            this.stateCycle.Setup(this.stateCycleItems);
            CraftProbablyGetResponse probablyGetResponse = await ClientHelper.ExecuteAsync<CraftProbablyGetResponse>((IRequest<CraftProbablyGetResponse>)new CraftProbablyGetRequest() { CraftDID = this.CraftDID });
            if (probablyGetResponse.CraftProbably != null)
                this.craftProbably.BindData(probablyGetResponse.CraftProbably);
            PLCStateListResponse stateListResponse = await ClientHelper.ExecuteAsync<PLCStateListResponse>((IRequest<PLCStateListResponse>)new PLCStateListRequest() { CraftDID = this.CraftDID });
            if (stateListResponse.PLCStates == null)
                return;
            this.plcList.BindData(stateListResponse.PLCStates);
        }

        private void BaseCraftIndexPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshStates();
            this.RefreshFacilityState();
        }

        private void btnCraftDetails_Click(object sender, RoutedEventArgs e)
        {
            CraftDetailsPage craftDetailsPage = new CraftDetailsPage();
            craftDetailsPage.CraftDID = this.CraftDID;
            craftDetailsPage.CraftNO = this.CraftNO;
            this.NavigationService.Navigate(craftDetailsPage);
        }
    }
}
