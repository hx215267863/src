using IFactory.UI.AlarmMonitor;
using IFactory.UI.Controls;
using IFactory.Common;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Threading;
using IFactory.UI.UserManager;

namespace IFactory.UI.CraftIndex
{
    /// <summary>
    /// CraftInspection1Page.xaml 的交互逻辑
    /// </summary>
    public partial class CraftInspection1Page : BaseCraftIndexPage, IComponentConnector
    {

        public delegate void NextPrimeDelegate();

        private string[] AlarmCheck { get; set; }
        private string[] FacilityDid { get; set; }

        private int[] Vulnerable_U{get;set;}

        private int[] Vulnerable_E{ get; set; }

        private int[] AlarmDid { get; set; }

        public int AlarmFlag = 1;

        public int VulnerableFlag = 0;

        public int LastDid = 0;

        public string code { get; set; }

        public CraftInspection1Page()
        {
            InitializeComponent();

            ///开机载入当前画面时候才可以进行此赋值
            this.CraftDID = 4;
            this.CraftNO = "STF_IN1";

            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 27.0,
                //1.0,
                EndAngle = 67.0,
                //35.0,
                Name = "UE001",
                Text = "搬运臂"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 69.0,
                //37.0,
                EndAngle = 109.0,
                //71.0,
                Name = "UF001",
                Text = "顶侧封封印厚度测量"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 111.0,//73.0,
                EndAngle = 151.0,//107.0,
                Name = "UG001",
                Text = "OK出料/NG入料盒"
            });
            /*
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 109.0,
                EndAngle = 143.0,
                Name = "UD001",
                Text = ""
            });
            */
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 209.0,
                EndAngle = 249.0,
                Name = "UA001",
                Text = "CCD定位/扫码"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 251.0,
                EndAngle = 291.0,
                Name = "UC001",
                Text = "电芯正面检测"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 293.0,
                EndAngle = 333.0,
                Name = "UB001",
                Text = "电芯背面检测"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 335.0,
                EndAngle = 385.0,//35.0,//359.0,
                Name = "UD001",
                Text = "Hi-pot测试"
            });
            
            this.stateCycle.ItemClick += new EventHandler<StateCycle.StateCycleItemClickEventArgs>(this.StateCycle_ItemClick);
            this.refreshTimer.Interval = new TimeSpan(0, 0, 4);
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
            //工序状态
            if (e.StateCycleItem.Tag == null || e.StateCycleItem.State != 3)
                return;
            int num = (int)e.StateCycleItem.Tag;
            this.NavigationService.Navigate(new RealTimeAlarmListPage()
            {
                ProcessDID = new int?(num)
            });
        }

        public void RefreshStates()
        {
            foreach (ProcessModel process in (LocalApi.Execute(new ProcessListRequest() { CraftDID = this.CraftDID })).Processes)
            {
                string shortNo = CommonHelper.GetProcessShortNO(process.ProcessNO);
                StateCycle.StateCycleItem stateCycleItem = this.stateCycleItems.FirstOrDefault<StateCycle.StateCycleItem>(m => m.Name == shortNo);
                if (stateCycleItem != null)
                {
                    //设备状态
                    stateCycleItem.State = process.State;
                    stateCycleItem.Tag = process.ProcessDID;
                }
            }
            this.stateCycle.Setup(this.stateCycleItems);           
            ThreadPool.QueueUserWorkItem(o =>
            {
                code = FactoryCheckDialog.EndProductNo;
                CraftProbablyGetResponse probablyGetResponse = LocalApi.Execute(new CraftProbablyGetRequest() { CraftDID = this.CraftDID ,code = this.code});
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    if (probablyGetResponse.CraftProbably != null)
                        this.craftProbably.BindData(probablyGetResponse.CraftProbably);
                }));
               
            });     
           
            PLCStateListResponse stateListResponse = LocalApi.Execute(new PLCStateListRequest() { CraftDID = this.CraftDID });

            if (stateListResponse.PLCStates == null)
                return;
            this.plcList.BindData(stateListResponse.PLCStates);

            AlarmCheckResponse alarmCheckResponse = LocalApi.AlarmExecute(new AlarmCheckRequest() { });
            DataProductionResponse DataProductionResponse = LocalApi.VulnerableExecute(new DataProductionRequest() { });
            Vulnerable_U = DataProductionResponse.CheckVulnerables.Select(m =>m.Used).ToArray();
            Vulnerable_E = DataProductionResponse.CheckVulnerables.Select(m =>m.Expect).ToArray();
            AlarmCheck = alarmCheckResponse.AlarmCheck.Select(m => m.AlarmCheck).ToArray();
            FacilityDid = alarmCheckResponse.AlarmCheck.Select(m => m.FacilityDid).ToArray();
            AlarmDid = alarmCheckResponse.AlarmCheck.Select(m => m.AlarmDid).ToArray();
            for(int i = 0;i < Vulnerable_E.Length;i++)
            {
                if(Vulnerable_U[i] == Vulnerable_E[i])
                {
                    VulnerableFlag = i + 1;
                    Box(VulnerableFlag);
                }
            }
            if (AlarmFlag == 0)
            {
                if (FacilityDid[0] == "701")
                {
                    if (AlarmCheck[0] == "0x0D0D01")
                    {
                        MessageBox.Show("电芯检测批量不合格", "警告");
                        AlarmFlag = 1;
						if (AlarmDid.Length != 0)
                        {
                            LastDid = AlarmDid[0];
                        }
                        else
                        {
                            LastDid = 0;
                        }
                    }
                }
            }
            if(AlarmDid.Length != 0)
            {
                if (AlarmDid[0] != LastDid)
                {
                    AlarmFlag = 0;
                }
            }
        }

        private void BaseCraftIndexPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshStates();
            this.RefreshFacilityState();
        }

        private void btnCraftDetails_Click(object sender, RoutedEventArgs e)
        {
            //CraftDetailsPage craftDetailsPage = new CraftDetailsPage();
            //craftDetailsPage.CraftDID = this.CraftDID;
            //craftDetailsPage.CraftNO = this.CraftNO;
            //this.NavigationService.Navigate(craftDetailsPage);
        }

        private void Box(int Flag)
        {
            switch(Flag)
            {
                case 1:
                    MessageBox.Show("气嘴使用次数已到达预期次数","易损件警告");
                    break;
            }        
        }
        
    }
}
