using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Visifire.Charts;
using IFactory.UI.Controls;
using System.Windows.Markup;
using IFactory.Platform.Common.Response.Crafts;
using IFactory.Platform.Common.Request.Crafts;
using IFactory.Domain.Common;
using IFactory.Common;
using System.Windows.Media.Imaging;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Request.Product;
using System.Threading;
using System.Windows.Threading;
using System.Xml;
using System.IO;

namespace IFactory.UI.FarCtrl
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class OneKey2 : BasePage, IComponentConnector
    {
        private DispatcherTimer refreshTimer = new DispatcherTimer();
        private BitmapImage image = null;
        public OneKey2()
        {
            InitializeComponent();

            this.refreshTimer.Interval = new TimeSpan(0, 0, 1);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
        
            //add by ly 2019-11-4,如果图片不存在会怎样？
            //if (!File.Exists(MainWindow.m_MainCtrl.m_SysState.strMoveInPic))
            //{

            //}
            //else
            //{
            //    try
            //    {
            //        BitmapImage imageMoveIn = new BitmapImage(new Uri(MainWindow.m_MainCtrl.m_SysState.strMoveInPic, UriKind.Absolute));//打开图片
            //        imageMoveInPic.Source = imageMoveIn;
            //    }catch(Exception err)
            //    {
            //        MainWindow.m_MainCtrl.SYS_IBG_LOG(0,0,0,"不知道干嘛崩溃了。。。" + err.ToString());
            //    } 
            //}

            //if (!File.Exists(MainWindow.m_MainCtrl.m_SysState.strTrayPic))
            //{

            //}
            //else
            //{
            //    try
            //    {
            //        BitmapImage imageTray = new BitmapImage(new Uri(MainWindow.m_MainCtrl.m_SysState.strTrayPic, UriKind.Absolute));//打开图片
            //        imageTrayPic.Source = imageTray;
            //    }catch(Exception err)
            //    {
            //        MainWindow.m_MainCtrl.SYS_IBG_LOG(0, 0, 0, "不知道干嘛崩溃了。。。" + err.ToString());
            //    }
            //}
            

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        public int isReady { get; set; }

        static public int isReady_change { get; set; }

        private void Init()
        {
            //OneKeyResponse oneKeyResponse = LocalApi.OneKey2(new OneKeyRequest() {});
            //isReady = oneKeyResponse.oneKeys.Select(m=>m.OneKey_flag).ToArray()[0];
            btnStart.Content = "启动";
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string st = btnStart.Content.ToString();
            if ("启动" == st)
            {
                if (0 == MainWindow.m_MainCtrl.StartSystem())
                {
                    btnStart.Content = "暂停";
                }           
            }
            else
            {
                btnStart.Content = "启动";
                MainWindow.m_MainCtrl.PauseSystem();
            }
        }

        OneKeyResponse oneKeyResponse = LocalApi.IsReady2(new OneKeyRequest() { });

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl.StopSystem();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow.m_MainCtrl.ResetSystem();   
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if( 0 == MainWindow.m_MainCtrl.SystemHome() )
            {
                ThreadStart threadStart1 = new ThreadStart(CheckHomeStatus);
                Thread thread1 = new Thread(threadStart1);
                thread1.Start();
                btnHome.Content = "初始化中...";
            }
        }

        public void CheckHomeStatus()
        {
            while(true)
            {
                if ( 0 == MainWindow.m_MainCtrl.GetHomeStatus() )
                {

                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                        {
                            btnHome.Content = "初始化";
                        }));
                    });
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Test
            

            if ( 0 != MainWindow.m_MainCtrl.ClearBattery())
            {
                MessageBox.Show("不具备清料条件");
            }
        }

        private void btnZeroYeild_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl.UpdateXML("D:\\Config.xml", "ParametersType", "Others", "Name", "当前产量", "0");
            MainWindow.m_MainCtrl.UpdateXML("D:\\Config.xml", "ParametersType", "Others", "Name", "OK产量", "0");
            //MainWindow.m_MainCtrl.m_SysState.SetYeild(0, 0);
        }
    }
}
