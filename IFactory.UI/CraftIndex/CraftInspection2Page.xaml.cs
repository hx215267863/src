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
using IFactory.UI;

namespace IFactory.UI.CraftIndex
{
    /// <summary>
    /// CraftInspection2Page.xaml 的交互逻辑
    /// </summary>
    public partial class CraftInspection2Page : BaseCraftIndexPage, IComponentConnector
    {
        public static CraftInspection2Page m_CraftInspection2Page = null;
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
        public CraftInspection2Page()
        {
            InitializeComponent();

            m_CraftInspection2Page = this;

            ///开机载入当前画面时候才可以进行此赋值
            this.CraftDID = 12;
            this.CraftNO = "STF_IN2";


            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 1.0,// 1.0,
                EndAngle = 35.0,//35.0,
                Name = "UE001",
                Text = "搬运臂"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 37.0,//98.0,
                //37.0,
                EndAngle = 71.0,//144.0,
                //71.0,
                Name = "UF001",
                Text = "满盒出机构"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 73.0,//217.0,
                EndAngle = 107.0,//251.0,
                Name = "UG001",
                Text = "空盒入机构"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 109.0,//217.0,
                EndAngle = 143.0,//251.0,
                Name = "UH001",
                Text = "硬件机构"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 217,// 253.0,
                EndAngle = 251.0,//287.0,
                Name = "UA001",
                Text = "入料拉带"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 253,//289.0,
                EndAngle = 287,//323.0,
                Name = "UB001",
                Text = "扫码"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 289.0,//325.0,
                EndAngle = 323.0,//359.0,
                Name = "UC001",
                Text = "CCD定位"
            });
            this.stateCycleItems.Add(new StateCycle.StateCycleItem()
            {
                StartAngle = 325.0,//325.0,
                EndAngle = 359.0,//359.0,
                Name = "UD001",
                Text = "机械手"
            });

            this.stateCycle.ItemClick += new EventHandler<StateCycle.StateCycleItemClickEventArgs>(this.StateCycle_ItemClick);
            this.refreshTimer.Interval = new TimeSpan(0, 0, 4);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();

            this.refreshTimer.Interval = new TimeSpan(0, 0, 0,0,500);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimerCtrl_Tick);
            this.refreshTimer.Start();
        }

        private List<StateCycle.StateCycleItem> stateCycleItems = new List<StateCycle.StateCycleItem>();

        private DispatcherTimer refreshTimer = new DispatcherTimer();
        private DispatcherTimer refreshTimerCur = new DispatcherTimer();
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            this.RefreshStates();
            this.RefreshFacilityState();
        }

        private void RefreshTimerCtrl_Tick(object sender, EventArgs e)
        {
            RefreshCurCtrl();
        }

        private void RefreshCurCtrl()
        {
            MainWindow.m_MainWindow.UpdataDataFromCtrl();

            ThreadPool.QueueUserWorkItem(o =>
            {
                code = FactoryCheckDialog.EndProductNo;

                //CraftProbablyGetResponse probablyGetResponse = LocalApi.Execute(new CraftProbablyGetRequest() { CraftDID = this.CraftDID ,code = this.code});
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    /*
                    if (probablyGetResponse.CraftProbably != null)
                    {
                        this.craftProbably.BindData(probablyGetResponse.CraftProbably);
                    }
                    */
                    if (mCraftProbably != null)
                    {
                        this.craftProbably.BindData(mCraftProbably);
                    }
                }));

            });
        }

        private void StateCycle_ItemClick(object sender, StateCycle.StateCycleItemClickEventArgs e)
        {
            //工序状态
            if (e.StateCycleItem.Tag == null || e.StateCycleItem.State != 2)
                return;
            int num = (int)e.StateCycleItem.Tag;
            this.NavigationService.Navigate(new RealTimeAlarmListPage()
            {
                ProcessDID = new int?(num)
            });
        }

        private CraftProbablyModel mCraftProbably = null;

        public void SetData(string BarCode,UInt32 NowYield,string DeviceName, UInt32 TargetYield,string UseName,
            double PPM, UInt32 OKCount,double OKRate,string Code)
        {
            if (mCraftProbably == null)
            {
                mCraftProbably = new CraftProbablyModel();
            }

            mCraftProbably.BatteryBarCode = BarCode;
            mCraftProbably.NowYield = NowYield.ToString();
            mCraftProbably.DeviceName = DeviceName;
            mCraftProbably.TargetYield = TargetYield.ToString();
            mCraftProbably.UseName = UseName;
            mCraftProbably.PPM = PPM.ToString("0.0000");
            mCraftProbably.OKCount = OKCount.ToString();
            mCraftProbably.OKRate = OKRate.ToString("0.0000%");
            mCraftProbably.Code = Code;
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
