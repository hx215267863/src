using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.UI.CraftIndex;
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
using System.Windows.Markup;
using System.Windows.Threading;
using IFactory.UI.AlarmMonitor;

namespace IFactory.UI
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : BasePage, IComponentConnector
    {
        
        public MainPage()
        {
            InitializeComponent();

            //检测对象显示界面
            /*
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 315.0,
                EndAngle = 405.0,
                Name = "IN2",
                Text = "收料机",

                Tag = new MainPage.StateCycleItemData()
                {
                    CraftIndexPageType = typeof(CraftInspection2Page)
                }
            });
            */

            this.stateCycle.ItemClick += new EventHandler<StateCycle.StateCycleItemClickEventArgs>(this.StateCycle_ItemClick);
            //定时器自动刷新（30秒一次）
            this.refreshTimer.Interval = new TimeSpan(0, 0, 30);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
        }

        private List<StateCycle.StateCycleItem> stateCycleItems = new List<StateCycle.StateCycleItem>();
        private DispatcherTimer refreshTimer = new DispatcherTimer();

        //定义变量
        public class StateCycleItemData
        {
            public Type CraftIndexPageType { get; set; }

            public int CraftDID { get; set; }

            public string CraftNO { get; set; }
        }

        public static string CompareCraftNO;
        //定时查询
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            this.RefreshStates();
            this.RefreshFacilityState();
        }

        //选择监控对象
        private void StateCycle_ItemClick(object sender, StateCycle.StateCycleItemClickEventArgs e)
        {
            /*
            MainPage.StateCycleItemData stateCycleItemData = (StateCycleItemData)e.StateCycleItem.Tag;
            if (stateCycleItemData == null)
                return;
            BaseCraftIndexPage baseCraftIndexPage = (BaseCraftIndexPage)Activator.CreateInstance(stateCycleItemData.CraftIndexPageType);
            CompareCraftNO = stateCycleItemData.CraftNO;
            baseCraftIndexPage.CraftDID = stateCycleItemData.CraftDID;
            HistoryAlarmListPage.CraftDID = stateCycleItemData.CraftDID;
            RealTimeAlarmListPage.CraftDID = stateCycleItemData.CraftDID;
            baseCraftIndexPage.CraftNO = stateCycleItemData.CraftNO;
            //对话框跳转
            this.NavigationService.Navigate(baseCraftIndexPage);
            */
        }

        //刷新状态
        public void RefreshStates()
        {
            foreach (CraftModel craft in (LocalApi.GetCraftsList(new CraftListRequest())).Crafts)
            {
                string craftShortNo = CommonHelper.GetCraftShortNO(craft.CraftNO);
                StateCycle.StateCycleItem stateCycleItem = this.stateCycleItems.FirstOrDefault<StateCycle.StateCycleItem>(m => m.Name == craftShortNo);
                if (stateCycleItem != null)
                {
                    MainPage.StateCycleItemData stateCycleItemData = (MainPage.StateCycleItemData)stateCycleItem.Tag;
                    if (stateCycleItemData != null)
                    {
                        stateCycleItemData.CraftDID = craft.CraftDID;
                        stateCycleItemData.CraftNO = craft.CraftNO;
                    }
                    stateCycleItem.State = craft.State;
                }
            }
            ProductionLineProbablyGetResponse probablyGetResponse = LocalApi.Execute(new ProductionLineProbablyGetRequest() { DID = 1 });
            if (probablyGetResponse.ProductionLineProbably != null)
                //this.lineProbably.BindData(probablyGetResponse.ProductionLineProbably);
            this.stateCycle.Setup(this.stateCycleItems);
        }

        private static CraftInspection2Page aaa = new CraftInspection2Page();
        //页面加载时刷新状态
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(aaa);
            this.RefreshStates();
            this.RefreshFacilityState();
        }
    }
}
