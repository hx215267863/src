/*
主界面窗口代码
*/
using IFactory.UI.Core;
using IFactory.UI.Situation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IFactory.UI.UserManager;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Request.Product;
using IFactory.Domain.Entities;
using IFactory.UI.CraftIndex;
using System.Data;
using ATL_MC.MainCtrl;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using IFactory.UI.Controls;
using IFactory.UI.FarCtrl;
using IFactory.UI.SystemParam;
using System.Diagnostics;
using ATL_MC.CtrlIni;
using System.IO;


namespace IFactory.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static  ATL_MC.MainCtrl.MainCtrl m_MainCtrl = null;
        public static MainWindow m_MainWindow = null;

        object obj_getdata = new object();
        public UInt32[] ProtuctionNum = null;

        public bool userCheckFlag = false;
        private bool CritE = false;

        static public bool LoginFlag = false;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            ((HwndSource)PresentationSource.FromVisual(this)).AddHook(myHook);
        }

        private IntPtr myHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (0x4000 == msg)
            {
                //收到普通报警信息
                string ss = Marshal.PtrToStringAnsi(wParam);
                int moudle = (int)lParam;

                if (!CritE)
                {
                    labelStatus.Content = ss;
                    MessageBox.Show(ss);
                }
            }
            else if (0x4001 == msg)
            {
                //收到普通报警信息
                string ss = Marshal.PtrToStringAnsi(wParam);
                int moudle = (int)lParam;

                if (!CritE)
                {
                    labelStatus.Content = ss;
                }
            }
            else if (0x4002 == msg)
            {
                //收到严重报警信息
                string ss = Marshal.PtrToStringAnsi(wParam);
                labelStatus.Content = ss;
                CritE = true;
            }
            else if (0x4500 == msg)
            {
                //收到报警清除信息 
                labelStatus.Content = "";
            }
            else if(0x6666== msg)
            {
                string ss = Marshal.PtrToStringAnsi(wParam);
                MessageBox.Show(ss);
            }

            return IntPtr.Zero;
        }

        private MainPage mMainPage = new MainPage();
        public MainWindow()
        {
            InitializeComponent();
            m_MainWindow = this;

            //如果表不存在，则创建表格
            LocalApi.CreateAlarmExecute();
            LocalApi.CreateCapacityOfProductionExecute();
            LocalApi.CreateProductionDataExecute();
            Directory.CreateDirectory("D:/ProductionData/");

            this.mainPage.Navigate(mMainPage);
            //this.mainPage.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
            //this.userCheckFlag = false;
            UserCheckDialog.btnStatus = "身份验证";
            labelTime.Content = DateTime.Now.ToString();
            labelWN.Content = "尚未登录";//getOperator();
            labelVersion.Content = "你好" + MainCtrl.Version;
            this.VersionTimer.Interval = new TimeSpan(1, 0, 0);
            this.statusCheckTimer.Interval = new TimeSpan(0, 0, 30);
            this.ANDONTimer.Interval = new TimeSpan(0, 0, 5);
            this.refreshTimer.Interval = new TimeSpan(0, 0, 1);
            this.statusTimer.Interval = new TimeSpan(0, 1, 0);
            this.VersionTimer.Tick += new EventHandler(this.VersionTimer_Tick);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.statusTimer.Tick += new EventHandler(this.StatusTimer_Tick);
            this.statusCheckTimer.Tick += new EventHandler(this.statusCheckTimer_Tick);
            this.ANDONTimer.Tick += new EventHandler(this.ANDONTimer_Tick);
            this.refreshTimer.Start();
            this.statusTimer.Start();
            this.statusCheckTimer.Start();
            this.ANDONTimer.Start();
            this.VersionTimer.Start();

            ProtuctionNum = new uint[61];

            m_MainCtrl = new ATL_MC.MainCtrl.MainCtrl();

            if (false)
            {
                //m_MainCtrl.bSimulate.camera1 = true;
                //m_MainCtrl.bSimulate.camera2 = true;
                //m_MainCtrl.bSimulate.scararobot = true;
                //m_MainCtrl.bSimulate.lightcontroller1 = true;
                //m_MainCtrl.bSimulate.lightcontroller2 = true;
                //m_MainCtrl.bSimulate.PLC = true;
                //m_MainCtrl.bSimulate.barcodescanner = true;
                //m_MainCtrl.bSimulate.Bis = true;
            }
            else
            {
                //m_MainCtrl.bSimulate.camera1 = false;
                //m_MainCtrl.bSimulate.camera2 = false;
                //m_MainCtrl.bSimulate.scararobot = false;
                //m_MainCtrl.bSimulate.lightcontroller1 = false;
                //m_MainCtrl.bSimulate.lightcontroller2 = false;
                //m_MainCtrl.bSimulate.PLC = false;
                //m_MainCtrl.bSimulate.barcodescanner = false;
                //m_MainCtrl.bSimulate.Bis = false;
            }


            //m_MainCtrl.pcg.EmptyAndFullTrayVision = false;
            //m_MainCtrl.pcg.TrayPosFix = false;
            //m_MainCtrl.pcg.BIS_Config = true;

            m_MainCtrl.LoadXmlConfig("D://Config.xml");



            switch (m_MainCtrl.Init())
            {
                case 0:
                    break;

                case 1:
                    MessageBox.Show("连接PLC失败");
                    break;

                case 2:
                    MessageBox.Show("连接PLC失败");
                    break;

                case 3:
                    MessageBox.Show("连接扫码器失败");
                    break;

                case 4:
                    MessageBox.Show("连接拉带相机失败");
                    break;

                case 5:
                    MessageBox.Show("连接拉带相机失败");
                    break;

                case 6:
                    MessageBox.Show("连接料盘相机失败");
                    break;

                case 7:
                    MessageBox.Show("连接料盘相机失败");
                    break;

                case 8:
                    MessageBox.Show("连接光源控制器1失败");
                    break;

                case 9:
                    MessageBox.Show("连接光源控制器2失败");
                    break;

                case 10:
                    MessageBox.Show("启动机械手失败");
                    break;

                case 11:
                    MessageBox.Show("连接机械手失败");
                    break;
                case 12:
                    MessageBox.Show("连接BIS失败");
                    break;
                default:
                    break;
            }
        }

        public void UpdataDataFromCtrl()
        {
            string barcode;
            UInt32 total,ok;
            long time;
            double drate = 0.0; ;
            double ppm;
            string product;
            lock(obj_getdata)
            {
                m_MainCtrl.GetData(out barcode, out total, out ok, out time, out ppm, out product);
            }

            //add by ly 2019-11-12 for get correct ppm
            ppm = (double)(ProtuctionNum[60] - ProtuctionNum[0]);

            if (0 != total)
            {
                drate = (double)ok / (double)total;
            }

            CraftInspection2Page.m_CraftInspection2Page.SetData(barcode, total, "ST5-0001", 10000000,labelWN.Content.ToString(), ppm, ok, drate, product);
        }

        private List<IFactory.UI.Core.MenuNode> mainNodes = new List<IFactory.UI.Core.MenuNode>();
        private IFactory.UI.Core.MenuNode lastSelectedMainNode;
        private IFactory.UI.Core.MenuNode lastSelectdChildNode;
        private DispatcherTimer refreshTimer = new DispatcherTimer();
        private DispatcherTimer statusTimer = new DispatcherTimer();
        private DispatcherTimer statusCheckTimer = new DispatcherTimer();
        private DispatcherTimer ANDONTimer = new DispatcherTimer();
        private DispatcherTimer VersionTimer = new DispatcherTimer();
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadMenus();
            UpdateVersionMessage();
        }

        private void VersionTimer_Tick(object sender, EventArgs e)
        {
            UpdateVersionMessage();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            //刷新前1分钟的产量
            for (int i=0; i<60; i++)
            {
                ProtuctionNum[i] = ProtuctionNum[i+1];
            }
            //获取最新产量
            //ProtuctionNum[60] = m_MainCtrl.m_SysState.GetYeild();


            this.RefreshInfo();
            if (UserCheckDialog.sendStaffID == null)
            {
                //labelWN.Content = "";
            }
            else
            {
                //labelWN.Content = UserCheckDialog.sendStaffID;
            }
            button_check_user.Content = UserCheckDialog.btnStatus;
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            if(this.userCheckFlag)
            {
                button_check_user.Content = "注销";
            }
        }

        private void statusCheckTimer_Tick(object sender, EventArgs e)
        {
            if (this.userCheckFlag)
            {
                string Flag = CheckOpenStatus();
                if (Flag == "0")
                {
                    //button_check_user.Content = "身份验证";
                    UserCheckDialog.btnStatus = "身份验证";
                    UserCheckDialog.sendStaffID = "";
                    this.userCheckFlag = false;
                    MessageBox.Show("已被注销，请重新进行人员验证", "告警");
                }
            }
        }

        //ljx
        private void ANDONTimer_Tick(object sender, EventArgs e)
        {
            string barcode;
            UInt32 total, ok;
            long time;
            double drate = 0.0; ;
            double ppm;
            string product;
            uint g, r, y;
            uint state;
            try
            {
                lock (obj_getdata)
                {
                    m_MainCtrl.GetData(out barcode, out total, out ok, out time, out ppm, out product);
                }
                //g = m_MainCtrl.mKEYENCE_PLC.GetGreenLight();
                //r = m_MainCtrl.mKEYENCE_PLC.GetRedLight();
                //y = m_MainCtrl.mKEYENCE_PLC.GetYellowLight();
                //state = m_MainCtrl.mKEYENCE_PLC.GetDTTYPE();
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "DTTYPE", state.ToString());
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "CAP", ok.ToString());
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "NG", (total-ok).ToString());
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "PPM", ppm.ToString());
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "G", g.ToString());
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "R", r.ToString());
                //m_MainCtrl.m_IniCtrl.SetStopTypeIni("WINDING", "Y", y.ToString());
                //m_MainCtrl.m_IniCtrl.SetANDONIni("ANDON", "UPDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" ));


                //m_MainCtrl.mKEYENCE_PLC.WriteRegister(78, 1);
                //每5秒钟，记录一次产能
                double rate = 0.0;
                if(total != 0)
                {
                    rate = ((double)((double)ok / (double)total))*100;
                }

                
                //string date = m_MainCtrl.m_IniCtrl.GetIni(@"D:\Debug.ini", "Global", "Date");
                //if(DateTime.Now.Day.ToString() != date)
                //{
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "Total", "0");
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "OK", "0");
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "Time", DateTime.Now.Hour.ToString());
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "Date", DateTime.Now.Day.ToString());
                //    MainWindow.m_MainCtrl.UpdateXML("D:\\Config.xml", "ParametersType", "Others", "Name", "当前产量", "0");
                //    MainWindow.m_MainCtrl.UpdateXML("D:\\Config.xml", "ParametersType", "Others", "Name", "OK产量", "0");
                //    MainWindow.m_MainCtrl.m_SysState.SetYeild(0, 0);
                //    m_MainCtrl.GetData(out barcode, out total, out ok, out time, out ppm, out product);
                //}
                //string hour = m_MainCtrl.m_IniCtrl.GetIni(@"D:\Debug.ini", "Global", "Time");
                //string totalCount = m_MainCtrl.m_IniCtrl.GetIni(@"D:\Debug.ini", "Global", "Total");
                //string okCount = m_MainCtrl.m_IniCtrl.GetIni(@"D:\Debug.ini", "Global", "OK");
                //if (DateTime.Now.Hour.ToString() == hour)
                //{
                //    total = total - uint.Parse(totalCount);
                //    ok = ok - uint.Parse(okCount);
                //}else
                //{
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "Total", total.ToString());
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "Time", DateTime.Now.Hour.ToString());
                //    m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "OK", ok.ToString());
                //    total = total - uint.Parse(totalCount);
                //    ok = ok - uint.Parse(okCount);
                //}
                if (total != 0)
                {
                    rate = ((double)((double)ok / (double)total)) * 100;
                }
                else
                {
                    rate = 0;
                }
                LocalApi.InsertCapacityExecute((int)total, (int)ok, rate);
            }
            catch (Exception gp)
            {
                //m_MainCtrl.SYS_IBG_LOG(0, 0, 0, "写产能错误");
            }
        }
        private int bool2int(bool flag)
        {
            if(flag)
            {
                return 1;
            }else
            {
                return 0;
            }
        }


        //加载菜单
        private void LoadMenus()
        {
            this.BuildMenus(AppContext.Current.GetPermissionNodesAsync());
        }

        public void BuildMenus(IList<PermissionNode> permissionNodes)
        {
            Style style1 = this.FindResource("MainButtonStyle") as Style;
            Style style2 = this.FindResource("ChildButtonStyle") as Style;
            Style style3 = this.FindResource("ChildButtonSelectedStyle") as Style;
            double height = this.menus.Height;
            foreach (PermissionNode permissionNode in permissionNodes)
            {
                IFactory.UI.Core.MenuNode menuNode1 = new IFactory.UI.Core.MenuNode();
                Button button1 = new Button();
                button1.Style = style1;
                button1.Click += new RoutedEventHandler(this.MainMenuButton_Click);
                button1.Content = permissionNode.Text;
                menuNode1.Button = button1;
                menuNode1.PermissionNode = permissionNode;
                button1.Tag = menuNode1;
                this.menus.Children.Add(button1);
                this.mainNodes.Add(menuNode1);
                if (permissionNode.Children.Count > 0)
                {
                    ScrollViewer scrollViewer = new ScrollViewer();
                    scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    StackPanel stackPanel = new StackPanel();
                    scrollViewer.Content = stackPanel;
                    scrollViewer.Visibility = Visibility.Collapsed;
                    foreach (PermissionNode child in permissionNode.Children)
                    {
                        IFactory.UI.Core.MenuNode menuNode2 = new IFactory.UI.Core.MenuNode();
                        Button button2 = new Button();
                        button2.Style = style2;
                        button2.Click += new RoutedEventHandler(this.ChildMenuButton_Click);
                        button2.Content = child.Text;
                        menuNode2.Button = button2;
                        menuNode2.PermissionNode = child;
                        button2.Tag = menuNode2;
                        menuNode1.Children.Add(menuNode2);
                        stackPanel.Children.Add(button2);
                    }
                    menuNode1.ChildPanel = scrollViewer;
                    this.menus.Children.Add(scrollViewer);
                }
                height -= button1.Height + button1.Margin.Top + button1.Margin.Bottom;
            }
            foreach (IFactory.UI.Core.MenuNode mainNode in this.mainNodes)
            {
                if (mainNode.ChildPanel != null)
                    mainNode.ChildPanel.Height = height;
            }
            if (this.mainNodes.Count <= 0)
                return;
            this.lastSelectedMainNode = this.mainNodes.First<IFactory.UI.Core.MenuNode>();
            if (this.lastSelectedMainNode.ChildPanel == null)
                return;
            this.lastSelectedMainNode.ChildPanel.Visibility = Visibility.Visible;
            this.lastSelectdChildNode = this.lastSelectedMainNode.Children.First<IFactory.UI.Core.MenuNode>();
            this.lastSelectdChildNode.Button.Style = style3;
        }

        //关闭按键
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否退出IFactory？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                m_MainCtrl.Close();
                System.Environment.Exit(0);
                Application.Current.Shutdown();
            }
        }

        //切换用户按键
        private void btnSwitchUser_Click(object sender, RoutedEventArgs e)
        {
        }

        //用户登出（退出程序）
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("是否退出IFactory？","提示",MessageBoxButton.OKCancel,MessageBoxImage.Question)==MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
            
        }

        private static BasePage basePageOneKer = null;
        private static BasePage baseDiagnostic = null;
        private static BasePage baseProductParam = null;
        private static BasePage baseOneChangePage = null;

        private static BasePage baseSystemParam = null;

        //private static Uri baseOneChangePage = null;

        //点击子菜单
        private void ChildMenuButton_Click(object sender, RoutedEventArgs e)
        {

            MainPage.StateCycleItemData StateCycleItemData = new MainPage.StateCycleItemData();
            Button button = (Button)sender;
            MenuNode tag = (MenuNode)button.Tag;
            if ((tag != this.lastSelectdChildNode) && (this.lastSelectdChildNode != null))
            {
                this.lastSelectdChildNode.Button.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
            this.lastSelectdChildNode = tag;
            tag.Button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/menu_child_button_bg.png", UriKind.Absolute)));
            switch (tag.PermissionNode.Code)
            {
                //本机概况
                case "RemoteMonitor.ProductOverview":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        //this.mainPage.Navigate(new Uri("CraftIndex/CraftInspection1Page.xaml", UriKind.Relative));
                        this.mainPage.Navigate(new Uri("CraftIndex/CraftInspection1Page.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("CraftIndex/CraftInspection2Page.xaml", UriKind.Relative));
                    break;

                //用户管理
                case "UserManager.User":
                    this.mainPage.Navigate(new Uri("UserManager/UserManagerPage.xaml", UriKind.Relative));
                    break;



                //新报警管理 lipl
                case "AlarmInfo.AlarmInfo":
                    this.mainPage.Navigate(new Uri("AlarmInfo/AlarmInfoPage.xaml", UriKind.Relative));
                    break;

                //参数设置管理 产品信息维护 lipl
                case "SystemParam.ProductParam":
                    if (null == baseProductParam)
                    {
                        baseProductParam = new IFactory.UI.SystemParamManager.ProductParamManagerPage();
                    }
                    this.mainPage.Navigate(baseProductParam);             
                    //this.mainPage.Navigate(new Uri("SystemParam/ProductParamManagerPage.xaml", UriKind.Relative));
                    break;

                case "SystemParam.ProductLightParam":
                    break;

                //参数设置管理 产品槽位坐标设置 lipl
                case "SystemParam.SystemParam":
                    if( null == baseSystemParam )
                    {
                        baseSystemParam = new SystemParamManagerPage();
                    }
                    this.mainPage.Navigate(baseSystemParam);

                    //this.mainPage.Navigate(new Uri("SystemParam/SystemParamManagerPage.xaml", UriKind.Relative));
                    break;

                //个人管理
                case "UserManager.Personal":
                    this.mainPage.Navigate(new Uri("UserManager/PersonalInfoPage.xaml", UriKind.Relative));
                    break;

                //角色管理
                case "UserManager.Role":
                    this.mainPage.Navigate(new Uri("UserManager/RoleManagerPage.xaml", UriKind.Relative));
                    break;

                //实时报警
                case "AlarmMonitor.RealTime":
                    this.mainPage.Navigate(new Uri("AlarmMonitor/RealTimeAlarmListPage.xaml", UriKind.Relative));
                    break;

                //历史报警
                case "AlarmMonitor.History":
                    this.mainPage.Navigate(new Uri("AlarmMonitor/HistoryAlarmListPage.xaml", UriKind.Relative));
                    break;

                //报警设置
                case "AlarmMonitor.Set":
                    this.mainPage.Navigate(new Uri("AlarmMonitor/SetAlarmPage.xaml", UriKind.Relative));
                    break;

                //生产数据
                case "DataList.DataProductionRealTime":
                    this.mainPage.Navigate(new Uri("zhuisu/DataProductionRealPage.xaml", UriKind.Relative));
                    break;

                //生产产能
                case "DataWareHouse.DataCapacity":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataCapacityPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataCapacityPage2.xaml", UriKind.Relative));
                    break;

                //生产优率
                case "DataWareHouse.DataQulity":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataQualityPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataQualityPage2.xaml", UriKind.Relative));
                    break;

                //PPM
                case "DataWareHouse.PPM":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/PPMPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/PPMPage2.xaml", UriKind.Relative));
                    break;

                //报警记录
                case "AlarmMonitor.AlarmCounts":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataAlarmPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataAlarmPage2.xaml", UriKind.Relative));
                    break;

                //测量尺寸
                case "DataWareHouse.Ave":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/AVEPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/AVEPage2.xaml", UriKind.Relative));
                    break;

                //良品数
                case "DataWareHouse.OK":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataOKPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataOKPage2.xaml", UriKind.Relative));
                    break;

                //坏品数
                case "DataWareHouse.NG":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataNGPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/DataNGPage2.xaml", UriKind.Relative));
                    break;

                //菜单管理
                case "SystemSetting.Menus":
                    this.mainPage.Navigate(new Uri("UserManager/MenuManagementPage.xaml", UriKind.Relative));
                    break;
                      
                //生产报表
                case "DataWareHouse.ProductStatement":
                    //if (MainPage.CompareCraftNO == "STF_IN1")
                        //this.mainPage.Navigate(new Uri("DataWareHouse/ProductStatementPage.xaml", UriKind.Relative));
                    //if (MainPage.CompareCraftNO == "STF_IN2")
                        //this.mainPage.Navigate(new Uri("DataWareHouse/ProductStatementPage2.xaml", UriKind.Relative));

                    break;

                //生产异常报表
                case "DataWareHouse.ProductStatementNG":
                   // if (MainPage.CompareCraftNO == "STF_IN1")
                        //this.mainPage.Navigate(new Uri("DataWareHouse/ProductStatementNGPage.xaml", UriKind.Relative));
                    //if (MainPage.CompareCraftNO == "STF_IN2")
                        //this.mainPage.Navigate(new Uri("DataWareHouse/ProductStatementNGPage2.xaml", UriKind.Relative));
                    break;

                //实时追溯
                case "DataList.ZhuiSuRealTime":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("ZhuiSu/ZhuiSuRealTimePage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("ZhuiSu/ZhuiSuRealTimePage2.xaml", UriKind.Relative));

                    break;

                //历史追溯
                case "DataList.ZhuiSuHistory":
                    this.mainPage.Navigate(new Uri("ZhuiSu/ZhuiSuHistoryPage.xaml", UriKind.Relative));
                    break;

                //历史生产数据
                case "DataList.DataProductionHistory":
                        this.mainPage.Navigate(new Uri("zhuisu/DataProductionHistoryPage.xaml", UriKind.Relative));
                    break;

                //易损件统计
                case "Maintain.VulnerableStatistic":
                    this.mainPage.Navigate(new Uri("Maintain/VulnerableStatisticPage.xaml", UriKind.Relative));
                    break;

                //冷热待机
                case "Situation.Standby":
                    {
                        StandbyPage page12 = new StandbyPage();
                        this.mainPage.Navigate(page12);
                        break;
                    }

                //设备管理
                case "SystemSetting.Alarmunit":
                    this.mainPage.Navigate(new Uri("Setting/AlarmUnitPage.xaml", UriKind.Relative));
                    break;

                //概况预设
                case "SystemSetting.GeneralPresupposition":
                    this.mainPage.Navigate(new Uri("Setting/ProductionLineManagentPage.xaml", UriKind.Relative));
                    break;

                //参数设置
                case "SystemSetting.SetArgument":
                    //this.mainPage.Navigate(new Uri("Setting/SetArgumentPage.xaml", UriKind.Relative));
                    break;

                //工作日历
                case "SystemSetting.WorkCalendar":
                    if (MainPage.CompareCraftNO == "STF_IN1")
                        this.mainPage.Navigate(new Uri("DataWareHouse/WorkCalendarPage.xaml", UriKind.Relative));
                    if (MainPage.CompareCraftNO == "STF_IN2")
                        this.mainPage.Navigate(new Uri("DataWareHouse/WorkCalendarPage2.xaml", UriKind.Relative));
                    break;

                //一键启动
                case "FarCtrl.OneKey":
                    if (null == basePageOneKer)
                    {
                        basePageOneKer = new OneKey2();
                    }
                    this.mainPage.Navigate(basePageOneKer);
                    //this.mainPage.Navigate(new Uri("FarCtrl/OneKeyPage2.xaml", UriKind.Relative));
                    

                    break;

                //一键换型
                case "FarCtrl.OneChange":
                    if (null == baseOneChangePage)
                    {
                        baseOneChangePage = new OneChange();
                        //baseOneChangePage = new Uri("FarCtrl/OneChangePage.xaml", UriKind.Relative);
                    }

                    this.mainPage.Navigate(baseOneChangePage);
                    //this.mainPage.Navigate(baseOneChangePage);
                    //this.mainPage.Navigate(new Uri("FarCtrl/OneChangePage.xaml", UriKind.Relative));
                    break;

                case "Diagnostic":
                    if(null == baseDiagnostic)
                    {
                        baseDiagnostic = new IFactory.UI.Diagnostic.Diagnostic();
                    }
                    this.mainPage.Navigate(baseDiagnostic);
                    //this.mainPage.Navigate(new Uri("Diagnostic/Diagnostic.xaml", UriKind.Relative));
                    break;
            }
        }
        //主菜单的选择和其子菜单名是否显示
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            IFactory.UI.Core.MenuNode menuNode = (IFactory.UI.Core.MenuNode)((FrameworkElement)sender).Tag;
            if (this.lastSelectedMainNode != menuNode && this.lastSelectedMainNode.ChildPanel != null)
                this.lastSelectedMainNode.ChildPanel.Visibility = Visibility.Collapsed;
            if (menuNode.ChildPanel != null)
                menuNode.ChildPanel.Visibility = Visibility.Visible;
            this.lastSelectedMainNode = menuNode;
            if (menuNode.Children.Count != 0 || this.lastSelectdChildNode == null)
                return;
            this.lastSelectdChildNode.Button.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            this.lastSelectdChildNode = null;
        }

        //最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //最大化
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        //private static BasePage pageCraftInspection2 = null;
        //private static CraftInspection2Page pageCraftInspection2 = null;
        
        //左上角监控对象列表
        private void btnGoHome_Click(object sender, RoutedEventArgs e)
        {
           // if(null == pageCraftInspection2)
            //{
              //  pageCraftInspection2 = new CraftInspection2Page();
            //}
            //this.mainPage.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
            //this.mainPage.Navigate(new Uri("CraftIndex/CraftInspection2Page.xaml", UriKind.Relative));
            this.mainPage.Navigate(CraftInspection2Page.m_CraftInspection2Page);
        }

        //未知按键
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (!this.mainPage.CanGoBack)
                return;
            this.mainPage.GoBack();
        }

        //非最大化模式下窗口可以用鼠标拖动
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.WindowState != WindowState.Normal || (!new Rect(new Point(0.0, 0.0), new Size(this.Width, this.Height * (7.0 / 225.0))).Contains(e.GetPosition(this)) || e.ChangedButton != MouseButton.Left))
                return;
            this.DragMove();
        }

        //未知功能
        public void UpdateFicilityState(int? state)
        {
            if (!state.HasValue)
                return;
            int? nullable = state;
            int num = 3;
            if ((nullable.GetValueOrDefault() == num ? (nullable.HasValue ? 1 : 0) : 0) != 0)
                this.imgFacilityState.Source = new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/state_abnormal.png", UriKind.Absolute));
            else
                this.imgFacilityState.Source = new BitmapImage(new Uri("pack://application:,,,/IFactory.UI;component/Assets/state_normal.png", UriKind.Absolute));
        }

        private void button_check_users(object sender, RoutedEventArgs e)
        {
            //FactoryCheckDialog factoryCheck = new FactoryCheckDialog();
            //factoryCheck.Show();
            if(button_check_user.Content.ToString() == "身份验证")
            {
                UserCheckDialog userCheck = new UserCheckDialog();
                userCheck.Show();
            }
            else
            {
                UserCheckDialog.btnStatus = "身份验证";
                UserCheckDialog.sendStaffID = "";
                this.userCheckFlag = false;
            }
            //this.Close();
        }

        private void button_check_barcodes(object sender, RoutedEventArgs e)
        {
            BarCodeCheckDialog barcodeCheck = new BarCodeCheckDialog();
            barcodeCheck.Show();
            //this.Close();
        }

        private void button_check_factorys(object sender, RoutedEventArgs e)
        {
            FactoryCheckDialog factoryCheck = new FactoryCheckDialog();
            factoryCheck.Show();
            //this.Close();
        }

        public void RefreshInfo()
        {
            labelTime.Content = DateTime.Now.ToString();
            //labelWN.Content = "00030000";
        }

        public string getOperator()
        {
            string Operator = "";
            DateTime today = DateTime.Now.Date;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = "select Operator from craft_probably where time > '" + today + "' order by time desc limit 1";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            if(bt.Rows.Count != 0)
            {
                Operator = bt.Rows[0][0].ToString();
            }
            else
            {
                Operator = "";
            }
            return Operator;
        }

        public string CheckOpenStatus()
        {
            string Flag = "";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = "select Flag from if_ifactory_client_open";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            if (bt.Rows.Count != 0)
            {
                Flag = bt.Rows[0][0].ToString();
            }
            else
            {
                Flag = "None";
            }
            return Flag;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_MainCtrl.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //m_MainCtrl.Test();
        }

        private void UpdateVersionMessage()
        {
            string VersionPLC = "";
            string VersionHMI = "";
            string VersionPC = "";
            //VersionPLC += m_MainCtrl.getStringFromPLC(83, 6);
            //VersionPLC += m_MainCtrl.getStringFromPLC(80, 3);
            //m_MainCtrl.m_IniCtrl.SetIni(@"D:\ANDON\SoftWareVersion.ini", "PLC", "Version", VersionPLC);
            //VersionHMI += m_MainCtrl.getStringFromPLC(123, 6);
            //VersionHMI += m_MainCtrl.getStringFromPLC(120, 3);
            //m_MainCtrl.m_IniCtrl.SetIni(@"D:\ANDON\SoftWareVersion.ini", "HMI", "Version", VersionHMI);
            //VersionPC += m_MainCtrl.getVersionPC();
            //m_MainCtrl.m_IniCtrl.SetIni(@"D:\ANDON\SoftWareVersion.ini", "PC", "Version", VersionPC);
        }
    }
}
