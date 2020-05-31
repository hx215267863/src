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
    /// CraftPIEFPage.xaml 的交互逻辑
    /// </summary>
    public partial class CraftPIEFPage : BaseCraftIndexPage, IComponentConnector
    {
        public CraftPIEFPage()
        {
            InitializeComponent();

            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 1.0,
                EndAngle = 44.0,
                Name = "UE001",
                Text = "冷却机"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 46.0,
                EndAngle = 89.0,
                Name = "UD001",
                Text = "容量机"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 91.0,
                EndAngle = 134.0,
                Name = "UC001",
                Text = "卸盘机"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 226.0,
                EndAngle = 269.0,
                Name = "UK001",
                Text = "上盘机"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 271.0,
                EndAngle = 314.0,
                Name = "DA001",
                Text = "RGV"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 316.0,
                EndAngle = 359.0,
                Name = "UA001",
                Text = "化成机"
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
