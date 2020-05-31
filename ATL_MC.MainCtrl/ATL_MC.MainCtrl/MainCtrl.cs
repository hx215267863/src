using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATL_MC.IBG_LOG;
using ATL_MC.SR_1000;
using ATL_MC.KangShiDaLightController;
using BaslerCamera;
using ATL_MC.Vision;
using ATL_MC.BIS;
using MyExcel;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Xml;
using IFactory;
using ATL_MC.EpsonScaraRobotController;
using ATL_MC.KEYENCE.PLC;
using ATL_MC.MainCtrl.System;
using ATL_MC.DAL.Service;
using PVCommon.List;
using ATL_MC.DAL.Model;

namespace ATL_MC.MainCtrl
{
    public partial class MainCtrl
    {
        public const string Version = "V01.30.00.200112";//应客户要求，版本号采用这种格式
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern bool PostMessage(int hwnd, int msg, IntPtr wP, IntPtr lP);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        private void SendMsg(Int32 level, long moudle, string msg)
        {
            if (0 == RecvHandle)
            {
                RecvHandle = FindWindow(null, "MainWindow");
            }
            if (0 != RecvHandle)
            {
                IntPtr init = Marshal.StringToHGlobalAnsi(msg);
                IntPtr inita = new IntPtr(moudle);
                PostMessage(RecvHandle, level, init, inita);
            }
        }
        private void ShowMsg(string msg)
        {
            SendMsg(0x6666, 0, msg);
        }
        private int RecvHandle = 0;

        //*************************************************************************************************************************

        #region 负载         
        public KeyenceNetPLC _netPLC { get; set; }
        public DansoSixAxisRobotController _robotController { get; set; } = new DansoSixAxisRobotController();
        public SR _barcodeScanner { get; set; } = new ATL_MC.SR_1000.SR();
        public BaslerCamera.BaslerCamera _moveInCamera { get; set; } = new BaslerCamera.BaslerCamera();
        public BaslerCamera.BaslerCamera _trayACamera { get; set; } = new BaslerCamera.BaslerCamera();
        public BaslerCamera.BaslerCamera _trayBCamera { get; set; } = new BaslerCamera.BaslerCamera();
        public BaslerCamera.BaslerCamera _trayCCamera { get; set; } = new BaslerCamera.BaslerCamera();
        public BaslerCamera.BaslerCamera _trayDCamera { get; set; } = new BaslerCamera.BaslerCamera();
        public BaslerCamera.BaslerCamera _trayECamera { get; set; } = new BaslerCamera.BaslerCamera();
        //料盘
        public ATL_MC.KangShiDaLightController.KangShiDaLightController _lightController_TrayA { get; set; } = new ATL_MC.KangShiDaLightController.KangShiDaLightController();
        public ATL_MC.KangShiDaLightController.KangShiDaLightController _lightController_TrayB { get; set; } = new ATL_MC.KangShiDaLightController.KangShiDaLightController();
        public ATL_MC.KangShiDaLightController.KangShiDaLightController _lightController_TrayC { get; set; } = new ATL_MC.KangShiDaLightController.KangShiDaLightController();
        public ATL_MC.KangShiDaLightController.KangShiDaLightController _lightController_TrayD { get; set; } = new ATL_MC.KangShiDaLightController.KangShiDaLightController();
        public ATL_MC.KangShiDaLightController.KangShiDaLightController _lightController_TrayE { get; set; } = new ATL_MC.KangShiDaLightController.KangShiDaLightController();
        //拉带
        public ATL_MC.KangShiDaLightController.KangShiDaLightController _lightController_MoveIn { get; set; } = new ATL_MC.KangShiDaLightController.KangShiDaLightController();
        #endregion

        #region TODO:视觉,沿用所有参数,未改动
        public ATL_MC.Vision.Vision _vision { get; set; } = new ATL_MC.Vision.Vision();
        public ATL_MC.Vision.BatteryVisionConfig m_BatteryVisionConfig { get; set; } = new ATL_MC.Vision.BatteryVisionConfig();
        public ATL_MC.Vision.CameraConfig m_CameraConfig { get; set; } = new ATL_MC.Vision.CameraConfig();
        #endregion

        public ATL_MC.BIS.BIS _bis { get; set; } = new ATL_MC.BIS.BIS();
        public ATL_MC.CtrlIni.CtrlIni m_IniCtrl { get; set; } = new ATL_MC.CtrlIni.CtrlIni();
        public ATL_MC.Vision.Vision mVision { get; set; } = new ATL_MC.Vision.Vision();
        private ExcelHelper excel = new ExcelHelper();
        private bool bStopThread = false;
        private FileStream fs;
        private StreamWriter sw;
        private string Path_XLS;
        private ATL_MC.IBG_LOG.IBG_LOG mIBG_LOG = new ATL_MC.IBG_LOG.IBG_LOG();
        private StatusManager _statusManager = StatusManager.CreateInstance();

        PagedList.PagedList<ProductDto> list1;
        PagedList.PagedList<ProductDetailDto> list2;

        ProductService productService = new ProductService();

        public MainCtrl()
        {
            _netPLC = new KeyenceNetPLC(_statusManager.Get<SystemConfig, string>(p => p.KeyencePlcIP), _statusManager.Get<SimulateConfig, bool>(p => p.Simulate_PLC));
            _robotController = new DansoSixAxisRobotController(
                GetSysConfig(p => p.RobotIP),
                GetSysConfig(p => p.RobotPort),
                GetSimulate(p => p.Simulate_DansoRobot)
                );
            //TODO:临时测试
             list1 = new ProductService().GetProductInfo(1,10,"");
            list2 = new ProductService().GetProductdetailInfo(1, 10, "");
          var products=  productService.GetAllProductNum();
        }

        #region 机械中各设备初始化init()
        /// <summary>
        /// 机械中各设备初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            int iret = 0;
            bool status = false;

            #region 初始化目录相关
            //ljx
            Path_XLS = "D:/ProductionData/" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            if (!File.Exists(Path_XLS))
            {
                excel.InitExcelStyle(Path_XLS, "Production", "");
            }
            fs = new FileStream(@"D:/SysAlm.ALM", FileMode.Append | FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);

            //ljx
            string Date = DateTime.Now.ToString("yyyy-MM-dd") + "/";
            Directory.CreateDirectory("D:/Picture/");
            Directory.CreateDirectory("D:/Picture/template_liyang/");
            Directory.CreateDirectory("D:/Picture/CameraLocation/");
            Directory.CreateDirectory("D:/Picture/CameraLocation/Original/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraLocation/Result/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/");
            Directory.CreateDirectory("D:/Picture/CameraInspection/Empty/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/Full/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/Result/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/NG/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/0/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/1/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/1/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/2/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/3/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/4/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/5/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/6/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/7/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/8/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/9/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/10/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/11/" + Date);
            Directory.CreateDirectory("D:/Picture/CameraInspection/12/" + Date);
            #endregion

            #region PLC连接
            if (!_netPLC.Connect())
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接PLC失败");
                return 1;
            }
            else
            {
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接PLC成功，版本号" + Version);
            }
            #endregion

            #region 扫码枪连接

            if (!_barcodeScanner.ConnectPort(GetSysConfig(p => p.BarcodeScannerIP), GetSimulate(p => p.Simulate_BarcodeScanner)))
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接扫码器失败");
                return 3;
            }
            else
            {
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接扫码器成功");
            }
            #endregion

            #region 相机初始化
            iret = _moveInCamera.OpenCamera(GetSysConfig(p => p.CameraSN_MoveIn), GetSimulate(p => p.Simulate_MoveInCamera));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "初始化入料口相机失败");
                return 4;
            }
            else
            {
                if (0 == _moveInCamera.SetImageSize(m_CameraConfig.CameraWidth_MoveIn, m_CameraConfig.CameraHeight_MoveIn, m_CameraConfig.CameraWidthOffset_MoveIn, m_CameraConfig.CameraHeightOffset_MoveIn))
                {
                    _moveInCamera.SetExposureTime(m_BatteryVisionConfig.dSliveryMoveInCameraExposureTime);
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "初始化入料口相机成功");
                }
                else
                {
                    SYS_IBG_LOG(CRITICALERR, 0, 0, "配置入料口相机失败");
                    return 5;
                }
            }
            iret = _trayACamera.OpenCamera(GetSysConfig(p => p.CameraSN_TrayA), GetSimulate(p => p.Simulate_CameraSN_TrayA));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "初始化入料口相机失败");
                return 4;
            }
            else
            {
                if (0 == _trayACamera.SetImageSize(m_CameraConfig.CameraWidth_TrayA, m_CameraConfig.CameraHeight_TrayA, m_CameraConfig.CameraWidthOffset_TrayA, m_CameraConfig.CameraHeightOffset_TrayA))
                {
                    _trayACamera.SetExposureTime(m_BatteryVisionConfig.CameraExposureTime_TrayA);
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "初始化入料口相机成功");
                }
                else
                {
                    SYS_IBG_LOG(CRITICALERR, 0, 0, "配置入料口相机失败");
                    return 5;
                }
            }
            iret = _trayBCamera.OpenCamera(GetSysConfig(p => p.CameraSN_TrayB), GetSimulate(p => p.Simulate_CameraSN_TrayB));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "初始化入料口相机失败");
                return 4;
            }
            else
            {
                if (0 == _trayBCamera.SetImageSize(m_CameraConfig.CameraWidth_TrayB, m_CameraConfig.CameraHeight_TrayB, m_CameraConfig.CameraWidthOffset_TrayB, m_CameraConfig.CameraHeightOffset_TrayB))
                {
                    _trayBCamera.SetExposureTime(m_BatteryVisionConfig.CameraExposureTime_TrayB);
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "初始化入料口相机成功");
                }
                else
                {
                    SYS_IBG_LOG(CRITICALERR, 0, 0, "配置入料口相机失败");
                    return 5;
                }
            }
            iret = _trayCCamera.OpenCamera(GetSysConfig(p => p.CameraSN_TrayC), GetSimulate(p => p.Simulate_CameraSN_TrayC));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "初始化入料口相机失败");
                return 4;
            }
            else
            {
                if (0 == _trayCCamera.SetImageSize(m_CameraConfig.CameraWidth_TrayC, m_CameraConfig.CameraHeight_TrayC, m_CameraConfig.CameraWidthOffset_TrayC, m_CameraConfig.CameraHeightOffset_TrayC))
                {
                    _trayCCamera.SetExposureTime(m_BatteryVisionConfig.CameraExposureTime_TrayC);
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "初始化入料口相机成功");
                }
                else
                {
                    SYS_IBG_LOG(CRITICALERR, 0, 0, "配置入料口相机失败");
                    return 5;
                }
            }
            iret = _trayDCamera.OpenCamera(GetSysConfig(p => p.CameraSN_TrayD), GetSimulate(p => p.Simulate_CameraSN_TrayD));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "初始化入料口相机失败");
                return 4;
            }
            else
            {
                if (0 == _trayDCamera.SetImageSize(m_CameraConfig.CameraWidth_TrayD, m_CameraConfig.CameraHeight_TrayD, m_CameraConfig.CameraWidthOffset_TrayD, m_CameraConfig.CameraHeightOffset_TrayD))
                {
                    _trayDCamera.SetExposureTime(m_BatteryVisionConfig.CameraExposureTime_TrayD);
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "初始化入料口相机成功");
                }
                else
                {
                    SYS_IBG_LOG(CRITICALERR, 0, 0, "配置入料口相机失败");
                    return 5;
                }
            }
            iret = _trayECamera.OpenCamera(GetSysConfig(p => p.CameraSN_TrayE), GetSimulate(p => p.Simulate_CameraSN_TrayE));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "初始化入料口相机失败");
                return 4;
            }
            else
            {
                if (0 == _trayECamera.SetImageSize(m_CameraConfig.CameraWidth_TrayE, m_CameraConfig.CameraHeight_TrayE, m_CameraConfig.CameraWidthOffset_TrayE, m_CameraConfig.CameraHeightOffset_TrayE))
                {
                    _trayECamera.SetExposureTime(m_BatteryVisionConfig.CameraExposureTime_TrayE);
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "初始化入料口相机成功");
                }
                else
                {
                    SYS_IBG_LOG(CRITICALERR, 0, 0, "配置入料口相机失败");
                    return 5;
                }
            }
            #endregion

            #region 光源初始化
            iret = _lightController_MoveIn.InitLightController(GetSysConfig(p => p.LightControllerCOM_MoveIn), GetSimulate(p => p.Simulate_Lightcontroller_MoveIn));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接拉带光源控制器失败");
                return 8;
            }
            else
            {
                _lightController_MoveIn.SetLightBox(0, 0);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接拉带光源控制器成功");
            }
            iret = _lightController_TrayA.InitLightController(GetSysConfig(p => p.LightControllerCOM_TrayA), GetSimulate(p => p.Simulate_Lightcontroller_TrayA));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接TrayA光源控制器失败");
                return 8;
            }
            else
            {
                _lightController_TrayA.SetLightBox(0, 0);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接TrayA光源控制器成功");
            }
            iret = _lightController_TrayB.InitLightController(GetSysConfig(p => p.LightControllerCOM_TrayB), GetSimulate(p => p.Simulate_Lightcontroller_TrayB));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接TrayB光源控制器失败");
                return 8;
            }
            else
            {
                _lightController_TrayB.SetLightBox(0, 0);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接TrayB光源控制器成功");
            }
            iret = _lightController_TrayC.InitLightController(GetSysConfig(p => p.LightControllerCOM_TrayC), GetSimulate(p => p.Simulate_Lightcontroller_TrayC));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接TrayC光源控制器失败");
                return 8;
            }
            else
            {
                _lightController_TrayC.SetLightBox(0, 0);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接TrayC光源控制器成功");
            }
            iret = _lightController_TrayD.InitLightController(GetSysConfig(p => p.LightControllerCOM_TrayD), GetSimulate(p => p.Simulate_Lightcontroller_TrayD));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接TrayD光源控制器失败");
                return 8;
            }
            else
            {
                _lightController_TrayD.SetLightBox(0, 0);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接TrayD光源控制器成功");
            }
            iret = _lightController_TrayE.InitLightController(GetSysConfig(p => p.LightControllerCOM_TrayE), GetSimulate(p => p.Simulate_Lightcontroller_TrayE));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接TrayE光源控制器失败");
                return 8;
            }
            else
            {
                _lightController_TrayE.SetLightBox(0, 0);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接TrayE光源控制器成功");
            }
            #endregion

            #region 原等待机械手启动代码
            //if (!bSimulate.scararobot)
            //{
            //    //等待PLC给机械手启动完成信号
            //    UInt32 timeout = 0;
            //    status = false;
            //    while (!status)
            //    {
            //        _netPLCControl.GetScaraRunningStatus(out status);
            //        Thread.Sleep(50);
            //        timeout += 50;

            //        if (timeout > 2000)
            //        {
            //            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "等待机械手启动超时");
            //            break;
            //            //return 10;
            //        }
            //    }

            //    //辣鸡机械手启动了还要等会儿才能连上
            //    Thread.Sleep(1000);
            //}
            #endregion

            //TODO:机械手开机，需要重新考虑
            #region 机械手连接
            iret = _robotController.ROBOT_Connect();
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接机械手失败");
                return 11;
            }
            else
            {
                //TODO:机械手开机手，初始化数据，与PLC沟通的信号，IO等
                //9号位_发料盒装满信号为0给机械手
                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_NGBoxIsFull), 0);
                //机械手开机会自己重置所有IO，这里为了确认和报警重启
                _robotController.ROBOT_SetIO(GetRobotIO(p => p.Robot_Output_Vacuum), 0);
                _robotController.ROBOT_SetIO(GetRobotIO(p => p.Robot_Output_VacuumBreaker), 0);
                //TODO:机械手重启需要回零（待机位）？，然后告诉PLC，机械手位置？
                //TODO:回零后才告诉PLC还是连接上后告诉，需要考虑
                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_RobotIsReady), 1);
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接机械手成功");
            }
            #endregion

            #region BIS连接
            iret = _bis.OpenCom(GetSysConfig(p => p.BisCOM), GetSimulate(p => p.Simulate_gBis));
            if (0 != iret)
            {
                SYS_IBG_LOG(CRITICALERR, 0, 0, "连接BIS失败");
                return 12;
            }
            else
            {
                _bis.write("开始");
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "连接BIS成功");
            }
            #endregion

            #region 启动线程
            //传输带线程  控制:拉带 扫码枪 光源 相机 拉带视觉
            ThreadStart threadMoveInStart = new ThreadStart(ThreadMoveIn);
            Thread threadMoveIn = new Thread(threadMoveInStart);
            threadMoveIn.Start();

            ThreadStart threadStart2 = new ThreadStart(ThreadRobot);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();

            ThreadStart threadStart3 = new ThreadStart(ThreadTray);
            Thread thread3 = new Thread(threadStart3);
            thread3.Start();

            ThreadStart threadStart6 = new ThreadStart(ThreadHome);
            Thread thread6 = new Thread(threadStart6);
            thread6.Start();

            ThreadStart threadStart7 = new ThreadStart(ThreadIOFunction);
            Thread thread7 = new Thread(threadStart7);
            thread7.Start();

            //ljx
            ThreadStart threadStart11 = new ThreadStart(ThreadANDON);
            Thread thread11 = new Thread(threadStart11);
            thread11.Start();

            #endregion

            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "程序初始化完成");
            return 0;
        }
        #endregion

        /// <summary>
        /// 设置当前产品配置
        /// </summary>
        public void SetProductConfig()
        {
            //料盒最大堆叠数量 超过就需要下料
            //料盒电池槽列数      
            //TODO:BIS相关,暂不处理
            //_bis.SetLot( );
            _vision.SetBatteryType(m_BatteryVisionConfig);

            _vision.SetCameraConfig(m_CameraConfig);
            //把配置写入PLC
            //TODO:PLC相关设置,暂不处理
            //_netPLCControl.SetCount(m_ProductConfig.iMaxTrayCount);
            _statusManager.Set<SystemStatus>(p => p.IsProductSelected = true);
        }

        public void Close()
        {
            sw.Close();
            fs.Close();
            bStopThread = true;
            Thread.Sleep(500);
            _netPLC.Close();
            _moveInCamera.CloseCamera();
            _trayACamera.CloseCamera();
            _trayBCamera.CloseCamera();
            _trayCCamera.CloseCamera();
            _trayDCamera.CloseCamera();
            _trayECamera.CloseCamera();
            _lightController_TrayA.SetLightBox(0, 0, 0, 0);
            _lightController_TrayB.SetLightBox(0, 0, 0, 0);
            _lightController_TrayC.SetLightBox(0, 0, 0, 0);
            _lightController_TrayD.SetLightBox(0, 0, 0, 0);
            _lightController_TrayE.SetLightBox(0, 0, 0, 0);
            _lightController_MoveIn.SetLightBox(0, 0);
            _robotController.ROBOT_DisConnect();
            _barcodeScanner.DisconnectPort();
            _bis.Close();
            mIBG_LOG.Close();
        }
        /// <summary>
        /// 系统错误
        /// 1.停止记数
        /// 2.提示系统错误
        /// 3.系统暂停
        /// 4.关灯       
        /// 5.如果是致命错误,
        /// </summary>
        /// <param name="level"></param>
        private void ErrorSystem(ushort level)
        {
            //TODO:系统出错，如何处理，暂停上位机？告诉PLC？通知机械手？需要设置那些变量等，需要考虑
            //TODO:报错后，如何恢复，            
            //TODO:等问题,后续一起考虑
            TimeToStop();
            //_statusManager.Set<SystemStatus>(p => p.m_SysErrorFlag = true);
            //_statusManager.Set<SystemStatus>(p => p.m_SysPauseFlag = true);

            if (CRITICALERR == level)
            {
                //_statusManager.Set<SystemStatus>(p => p.m_SysCriticalError = true);
            }

            //报警了要立即关灯
            TurnOffAllLight();
        }
        /// <summary>
        /// 关灯
        /// </summary>
        private void TurnOffAllLight()
        {
            _lightController_TrayA.SetLightBox(0, 0, 0, 0);
            _lightController_TrayB.SetLightBox(0, 0, 0, 0);
            _lightController_TrayC.SetLightBox(0, 0, 0, 0);
            _lightController_TrayD.SetLightBox(0, 0, 0, 0);
            _lightController_TrayE.SetLightBox(0, 0, 0, 0);
            _lightController_MoveIn.SetLightBox(0, 0);
        }
        /// <summary>
        /// 恢复光源亮度
        /// </summary>
        private void RecoverAllLight()
        {
            _lightController_TrayA.Recover(4);
            _lightController_TrayB.Recover(4);
            _lightController_TrayC.Recover(4);
            _lightController_TrayD.Recover(4);
            _lightController_TrayE.Recover(4);
            _lightController_MoveIn.Recover(2);
        }
        /// <summary>
        /// 计时停止
        /// </summary>
        private void TimeToStop()
        {
            _statusManager.Set<SystemStatus>(p => p.TotalTime.Stop());

        }
        /// <summary>
        /// 计时开时
        /// </summary>
        private void TimeToStart()
        {
            _statusManager.Set<SystemStatus>(p => p.TotalTime.Start());
        }



        public bool GetSysRunning()
        {
            if (_statusManager.Get<SystemStatus, bool>(p => p.Sys_Running))
            {
                return true;
            }
            return false;
        }

        public int StartSystem()
        {
            //if (!_statusManager.Get<SystemStatus, bool>(p => p.m_SysHomeFlag))
            //{
            //    ShowMsg("没有回零完成，无法启动");
            //    return 1;
            //}
            //if (!_statusManager.Get<SystemStatus, bool>(p => p.m_SysErrorFlag))
            //{
            //    ShowMsg("系统存在错误，无法启动");
            //    return 2;
            //}
            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_SysCriticalError))
            //{
            //    ShowMsg("发生过严重故障，无法启动");
            //    return 3;
            //}
            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_iClear))
            //{
            //    ShowMsg("清料后，需要重开程序重新选择产品");
            //    _statusManager.Set<SystemStatus>(p => p.m_SysPauseFlag = true);
            //    return 4;
            //}

            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_iNGBoxFull))
            //{
            //    ShowMsg("NG料盒满，无法启动");
            //    return -1;
            //}
            //if (!_statusManager.Get<SystemStatus, bool>(p => p.m_iNGBoxInplace))
            //{
            //    ShowMsg("NG料不到位，无法启动");
            //    return -1;
            //}

            //if (IsDoorOpen())
            //{
            //    ShowMsg("门未完全关闭好，无法启动");
            //    return 5;
            //}
            //if (_robotController.GetPaused())
            //{
            //    ContinueRobot();
            //    Thread.Sleep(100);
            //    if (_robotController.GetPaused())
            //    {
            //        ShowMsg("Scara依然暂停，无法启动");
            //        return 6;
            //    }
            //}

            //if (!_statusManager.Get<SystemStatus, bool>(p => p.IsProductSelected))
            //{
            //    ShowMsg("未选择产品,无法启动");
            //    return 7;
            //}

            //if (!_statusManager.Get<SystemStatus, bool>(p => p.m_SysPauseFlag))
            //{
            //    //恢复灯

            //    RecoverAllLight();
            //    _statusManager.Set<SystemStatus>(p => p.m_SysPauseFlag = false);
            //    TimeToStart();
            //}
            //if (!_statusManager.Get<SystemStatus, bool>(p => p.m_SysStartFlag))
            //{

            //    if (_statusManager.Get<ProgramConfig, bool>(p => p.BIS_Config))
            //    {
            //        int count = _netPLC.GetCount();

            //        if (0 == count)
            //        {
            //            _statusManager.Set<SystemStatus>(p => p.m_SysStartFlag = true);
            //            _statusManager.Set<SystemStatus>(p => p.TotalTime.Start());
            //        }
            //        else
            //        {
            //            ShowMsg("请停止PLC，再回零重置料盘层数");
            //            return -1;
            //        }
            //    }
            //    else
            //    {
            //        _statusManager.Set<SystemStatus>(p => p.m_SysStartFlag = true);
            //        _statusManager.Set<SystemStatus>(p => p.TotalTime.Start());
            //    }
            //}

            //_netPLCControl.Start();


            //模拟PLC启动系统

            SetSysStatus(p => p.Sys_MoveInContinus = true);
            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "系统启动");
            return 0;
        }
        public int SetBis(bool fl)
        {
            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_SysStartFlag))
            //{
            //    if (_statusManager.Get<ProgramConfig, bool>(p => p.BIS_Config) != fl)
            //    {
            //        return 1;
            //    }
            //}
            //_statusManager.Set<ProgramConfig>(p => p.BIS_Config = fl);
            return 0;
        }
        public void GetData(out string barcode, out UInt32 count, out UInt32 okcount, out long time, out double ppm, out string product)
        {
            //TODO:测试用,需要删除
            barcode = "";
            count = 1;
            okcount = 1;
            time = 1;
            ppm = 1;
            product = m_BatteryVisionConfig.product;


            //TODO:此处应有多个二维码,
            //barcode = _statusManager.Get<SystemStatus, string>(p => p.BarCode_TrayA);
            //count = _statusManager.Get<SystemStatus, uint>(p => p.NowYield);
            //okcount = _statusManager.Get<SystemStatus, uint>(p => p.OKYeild);
            //time = _statusManager.Get<SystemStatus, long>(p => p.TotalTime.ElapsedMilliseconds);
            //// ppm = _statusManager.Get<SystemStatus, long>(p => p.ppm); //TODO:此处计算有误
            //ppm = 0.0;
            //product = m_BatteryVisionConfig.product;
        }
        public void PauseSystem()
        {
            //TimeToStop();
            //_statusManager.Set<SystemStatus>(p => p.m_SysPauseFlag = true);
            //SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "系统暂停");
        }
        public void StopSystem()
        {
            //TimeToStop();
            //_statusManager.Set<SystemStatus>(p => p.m_SysStop = true);
            //_netPLC.Stop();
            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "系统停止");
        }
        public void ResetSystem()
        {
            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_SysCriticalError))
            //{
            //    return;
            //}
            //_statusManager.Set<SystemStatus>(p => p.m_SysErrorFlag = false);
            //SendMsg(0x4500, 0, "");
        }
        /// <summary>
        /// 清料?
        /// </summary>
        /// <returns></returns>
        public int ClearBattery()
        {
            //_statusManager.Set<SystemStatus>(p => p.m_iClear = true);
            /*
            if(!m_SysState.GetSysStart())
            {
                return 1;
            }

            if (!m_SysState.GetSysPause())
            {
                return 2;
            }

            if (m_SysState.GetSysError())
            {
                return 3;
            }

            if (m_SysState.GetSysCriticalError())
            {
                return 4;
            }

            if( !m_SysState.GetSysHome())
            {
                return 5;
            }

            ulong IOIn = 0, IOOut = 0;
            m_SysState.SetScaraIO(IOIn, IOOut);
            if (0 != ((int)IOOut & (1 << m_SysConfig.Output_ADDR_Uacuum)))
            {
                return 6;
            }

            if (m_SysState.GetClear())
            {
                return 7;
            }

            m_SysState.SetClear(true);
            ThreadStart threadStart = new ThreadStart(ThreadClear);
            Thread thread = new Thread(threadStart);
            thread.Start();
            */

            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "清料");
            return 0;
        }
        /// <summary>
        /// 系统回零
        /// </summary>
        /// <returns></returns>
        public int SystemHome()
        {
            //if (1 != _statusManager.Get<SystemStatus, int>(p => p.Thread_HomeStep))
            //{
            //    return 1;
            //}
            //if (IsDoorOpen())
            //{
            //    return 2;
            //}
            //if (_robotController.GetPaused())
            //{
            //    ContinueRobot();
            //    Thread.Sleep(100);
            //    if (_robotController.GetPaused())
            //    {
            //        return 3;
            //    }
            //}
            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_SysStartFlag))
            //{
            //    return 4;
            //}
            //_statusManager.Set<SystemStatus>(p => p.m_SysHomeFlag = false);
            //_statusManager.Set<SystemStatus>(p => p.m_iRequestStartHome = true);
            //SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "系统回零");
            return 0;
        }
        /// <summary>
        /// 取得系统回零状态
        /// </summary>
        /// <returns></returns>
        public int GetHomeStatus()
        {
            //if (_statusManager.Get<SystemStatus, bool>(p => p.m_SysHomeFlag))
            //{
            //    return 0;
            //}
            //else
            //{
            //    if ((false == _statusManager.Get<SystemStatus, bool>(p => p.m_iRequestStartHome)) && (1 == _statusManager.Get<SystemStatus, int>(p => p.Thread_HomeStep)))
            //    {
            //        return 0;
            //    }
            //    return 1;
            //}
            return 0;
        }


        /// <summary>
        /// 机械手继续
        /// </summary>
        private void ContinueRobot()
        {
            //TODO:仅测试的IO地址
            _robotController.ROBOT_SetIO(15, 1);
            Thread.Sleep(80);
            //TODO:仅测试的IO地址
            _robotController.ROBOT_SetIO(15, 0);
        }
        /// <summary>
        /// 六轴机械手无法释放,弃用
        /// </summary>
        /// <returns></returns>
        public int FreeRobot()
        {
            //if (IsDoorOpen())
            //{
            //    ShowMsg("门未关闭，无法释放机械手");
            //    return 1;
            //}

            //if (_robotController.GetPaused())
            //{
            //    ContinueRobot();
            //    Thread.Sleep(100);
            //    if (_robotController.GetPaused())
            //    {
            //        ShowMsg("稍后请尝试释放机械手");
            //        return 3;
            //    }
            //}
            //_robotController.ROBOT_FreeRobot();
            return 0;
        }
        /// <summary>
        /// 六轴不能解锁,不需要使用锁定,弃用
        /// </summary>
        /// <returns></returns>
        public int LockRobot()
        {
            //if (IsDoorOpen())
            //{
            //    ShowMsg("门未关闭，无法锁定机械手");
            //    return 1;
            //}
            //if (_robotController.GetPaused())
            //{
            //    ContinueRobot();
            //    Thread.Sleep(100);
            //    if (_robotController.GetPaused())
            //    {
            //        ShowMsg("稍后请尝试锁定机械手");
            //        return 3;
            //    }
            //}
            //_robotController.ROBOT_LockRobot();
            return 0;
        }

        /// <summary>
        /// 料盘灯是否打开
        /// </summary>
        /// <returns></returns>
        private bool IsTTrayControllerOpen()
        {
            //bool result = false;
            //ulong ioin = 0;
            //ulong ioout = 0;
            //m_SysState.GetScaraIO(out ioin, out ioout);
            //if (0 != ((int)ioin & (1 << m_SysConfig.Input_ADDR_TrayControllerOpen)))
            //{
            //    result = true;
            //}
            //else
            //{
            //    result = false;
            //}
            //return result;
            return false;
        }

        //更新点集合参数
        public bool UpdateXML(string xmlPath, string root, string personName, string proTypeValue, string updateproValue, string updateValue)
        {
            try
            {
                //改变属性的值
                XmlDocument doc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;//忽略文档里面的注释
                XmlReader reader = XmlReader.Create(xmlPath, settings);
                doc.Load(reader);    //加载Xml文件  
                XmlNode xn = doc.SelectSingleNode(root);
                // 得到根节点的所有子节点
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode node in xnl)
                {
                    if (node.Name == personName)
                    {
                        XmlNodeList xnl2 = node.ChildNodes;
                        foreach (XmlNode node2 in xnl2)
                        {
                            if (((XmlElement)node2).GetAttribute(proTypeValue).ToString() == updateproValue)
                            {
                                node2.InnerText = updateValue;
                                break;
                            }
                        }
                        break;
                    }
                }
                //最后读取完毕后,记得要关掉reader.
                reader.Close();
                doc.Save(xmlPath);
                return true;
            }
            catch (Exception ex)
            {
                //缺日志
                throw new Exception("修改XML文件失败!" + ex.Message);
            }

        }

        public int LoadXmlConfig(string filename)
        {
            //UInt32 SetTotalYeild = 0;
            //UInt32 SetOKYeild = 0;
            //XmlDocument xml_doc = new XmlDocument();
            //if (!File.Exists(filename))
            //{
            //    ShowMsg("没有发现配置文件:" + filename);
            //    return 1;
            //}
            //xml_doc.Load(filename);
            //XmlNode node0 = xml_doc.SelectSingleNode("ParametersType");
            //XmlNode node1_1 = node0.SelectSingleNode("CameraParameter");
            //XmlNodeList list1 = node1_1.ChildNodes;

            //XmlNode node1_2 = node0.SelectSingleNode("ComParameter");
            //XmlNodeList list2 = node1_2.ChildNodes;

            //XmlNode node1_3 = node0.SelectSingleNode("Calibration");
            //XmlNodeList list3 = node1_3.ChildNodes;

            //XmlNode node1_4 = node0.SelectSingleNode("ScaraParameter");
            //XmlNodeList list4 = node1_4.ChildNodes;

            //XmlNode node1_5 = node0.SelectSingleNode("Others");
            //XmlNodeList list5 = node1_5.ChildNodes;

            //XmlNode node1_6 = node0.SelectSingleNode("VisionPara");
            //XmlNodeList list6 = node1_6.ChildNodes;

            //foreach (XmlNode no in list1)
            //{
            //    if (no.Attributes["Name"].InnerText == "拉带定位相机SN")
            //    {
            //        m_SysConfig.sMoveInCameraSN = no.InnerText;
            //    }
            //    else if (no.Attributes["Name"].InnerText == "料盒检测相机SN")
            //    {
            //        m_SysConfig.sTrayCameraSN = no.InnerText;
            //    }
            //    else if (no.Attributes["Name"].InnerText == "拉带定位相机分辨率(mm/pixel)")
            //    {
            //        m_CameraConfig.dMoveInCameraScale = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "料盒检测相机分辨率(mm/pixel)")
            //    {
            //        m_CameraConfig.dTrayCameraScale = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "银色电池拉带定位相机曝光时间us")
            //    {
            //        m_BatteryVisionConfig.dSliveryMoveInCameraExposureTime = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "黑色电池拉带定位相机曝光时间us")
            //    {
            //        m_BatteryVisionConfig.dBlackMoveInCameraExposureTime = Convert.ToDouble(no.InnerText);
            //    }
            //}

            //foreach (XmlNode no in list2)
            //{
            //    if (no.Attributes["Name"].InnerText == "拉带定位光源控制器串口号")
            //    {
            //        m_SysConfig.sLightController1COM = no.InnerText;
            //    }
            //    else if (no.Attributes["Name"].InnerText == "料盒检测光源控制器串口号")
            //    {
            //        m_SysConfig.sLightController2COM = no.InnerText;
            //    }
            //    else if (no.Attributes["Name"].InnerText == "PLC与PC通讯串口号")
            //    {
            //        m_SysConfig.sKeyencePlcCOM = no.InnerText;
            //    }
            //}

            //foreach (XmlNode no in list3)
            //{
            //    if (no.Attributes["Name"].InnerText == "机械手取电池示教点X坐标")
            //    {
            //        m_SysConfig.dScaraCatchPosX = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手取电池示教点Y坐标")
            //    {
            //        m_SysConfig.dScaraCatchPosY = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手取电池示教点Z坐标")
            //    {
            //        m_SysConfig.dScaraCatchPosZ = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手取电池示教点U坐标")
            //    {
            //        m_SysConfig.dScaraCatchPosU = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "电池中心相机横坐标")
            //    {
            //        m_BatteryVisionConfig.dMoveInTargetX = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "电池中心相机纵坐标")
            //    {
            //        m_BatteryVisionConfig.dMoveInTargetY = Convert.ToDouble(no.InnerText);
            //    }
            //}

            //foreach (XmlNode no in list4)
            //{
            //    if (no.Attributes["Name"].InnerText == "机械手待机点X坐标")
            //    {
            //        m_SysConfig.dScaraStandbyPosX = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手待机点Y坐标")
            //    {
            //        m_SysConfig.dScaraStandbyPosY = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手待机点Z坐标")
            //    {
            //        m_SysConfig.dScaraStandbyPosZ = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手待机点U坐标")
            //    {
            //        m_SysConfig.dScaraStandbyPosU = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手移动抬起高度Z")
            //    {
            //        m_SysConfig.dScaraSaveZ = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手NG电池位置X坐标")
            //    {
            //        m_SysConfig.dScaraNGPosX = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手NG电池位置Y坐标")
            //    {
            //        m_SysConfig.dScaraNGPosY = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手NG电池位置Z坐标")
            //    {
            //        m_SysConfig.dScaraNGPosZ = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手NG电池位置U坐标")
            //    {
            //        m_SysConfig.dScaraNGPosU = Convert.ToDouble(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "破真空延时时间")
            //    {
            //        m_SysConfig.lScaraVaccumBreakDelay = Convert.ToInt32(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "机械手与入料口相机坐标系角度差")
            //    {
            //        m_SysConfig.dDeltaAngleBetweenScaraAndCamera = Convert.ToDouble(no.InnerText);
            //    }
            //}

            //foreach (XmlNode no in list5)
            //{
            //    if (no.Attributes["Name"].InnerText == "连接扫码器")
            //    {
            //        bSimulate.barcodescanner = (0 == Convert.ToInt32(no.InnerText));
            //    }
            //    if (no.Attributes["Name"].InnerText == "当前产量")
            //    {
            //        SetTotalYeild = (uint)Convert.ToInt32(no.InnerText);
            //    }
            //    if (no.Attributes["Name"].InnerText == "OK产量")
            //    {
            //        SetOKYeild = (uint)Convert.ToInt32(no.InnerText);
            //    }

            //    //add by ly 2019-11-14
            //    m_SysState.SetYeild(SetTotalYeild, SetOKYeild);
            //}

            //foreach (XmlNode no in list6)
            //{
            //    if (no.Attributes["Name"].InnerText == "定位视觉边界参数Row1")
            //    {
            //        m_CameraConfig.lLocationParametersRow1 = Convert.ToInt32(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "定位视觉边界参数Row2")
            //    {
            //        m_CameraConfig.lLocationParametersRow2 = Convert.ToInt32(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "定位视觉边界参数Col1")
            //    {
            //        m_CameraConfig.lLocationParametersCol1 = Convert.ToInt32(no.InnerText);
            //    }
            //    else if (no.Attributes["Name"].InnerText == "定位视觉边界参数Col2")
            //    {
            //        m_CameraConfig.lLocationParametersCol2 = Convert.ToInt32(no.InnerText);
            //    }
            //}
            return 0;
        }

        public int LoadXML(string filename)
        {
            XmlDocument xml_doc = new XmlDocument();

            if (!File.Exists(filename))
            {
                ShowMsg("没有发现视觉配置文件:" + filename);
                return 1;
            }

            xml_doc.Load(filename);

            XmlNode node0 = xml_doc.SelectSingleNode("ParametersType");

            XmlNode node1_1 = node0.SelectSingleNode("StaticParameter");

            XmlNodeList list1 = node1_1.ChildNodes;


            XmlNode node1_2 = node0.SelectSingleNode("GeneralParameter");

            XmlNodeList list2 = node1_2.ChildNodes;
            double[] para = new double[list1.Count + list2.Count];

            foreach (XmlNode no in list1)
            {
                if (no.Attributes["Name"].InnerText == "Parm0")
                {
                    para[0] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm1")
                {
                    para[1] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm2")
                {
                    para[2] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm3")
                {
                    para[3] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm4")
                {
                    para[4] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm5")
                {
                    para[5] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm6")
                {
                    para[6] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm7")
                {
                    para[7] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm8")
                {
                    para[8] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm9")
                {
                    para[9] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm10")
                {
                    para[10] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm11")
                {
                    para[11] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm12")
                {
                    para[12] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm13")
                {
                    para[13] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm14")
                {
                    para[14] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm15")
                {
                    para[15] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm16")
                {
                    para[16] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm17")
                {
                    para[17] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm18")
                {
                    para[18] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm19")
                {
                    para[19] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm20")
                {
                    para[20] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm21")
                {
                    para[21] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm22")
                {
                    para[22] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm23")
                {
                    para[23] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm24")
                {
                    para[24] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm25")
                {
                    para[25] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm26")
                {
                    para[26] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm27")
                {
                    para[27] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm28")
                {
                    para[28] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm29")
                {
                    para[29] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm30")
                {
                    para[30] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm31")
                {
                    para[31] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm32")
                {
                    para[32] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm33")
                {
                    para[33] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm34")
                {
                    para[34] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm35")
                {
                    para[35] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm36")
                {
                    para[36] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm37")
                {
                    para[37] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm38")
                {
                    para[38] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm39")
                {
                    para[39] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm40")
                {
                    para[40] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm41")
                {
                    para[41] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm42")
                {
                    para[42] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm43")
                {
                    para[43] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm44")
                {
                    para[44] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm45")
                {
                    para[45] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm46")
                {
                    para[46] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm47")
                {
                    para[47] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm48")
                {
                    para[48] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm49")
                {
                    para[49] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm50")
                {
                    para[50] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm51")
                {
                    para[51] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm52")
                {
                    para[52] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm53")
                {
                    para[53] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm54")
                {
                    para[54] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm55")
                {
                    para[55] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm56")
                {
                    para[56] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm57")
                {
                    para[57] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm58")
                {
                    para[58] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm59")
                {
                    para[59] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm60")
                {
                    para[60] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm61")
                {
                    para[61] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm62")
                {
                    para[62] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm63")
                {
                    para[63] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm64")
                {
                    para[64] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm65")
                {
                    para[65] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm66")
                {
                    para[66] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm67")
                {
                    para[67] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm68")
                {
                    para[68] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm69")
                {
                    para[69] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm70")
                {
                    para[70] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm71")
                {
                    para[71] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm72")
                {
                    para[72] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm73")
                {
                    para[73] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm74")
                {
                    para[74] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm75")
                {
                    para[75] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm76")
                {
                    para[76] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm77")
                {
                    para[77] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm78")
                {
                    para[78] = Convert.ToDouble(no.InnerText);
                }

                else if (no.Attributes["Name"].InnerText == "Parm79")
                {
                    para[79] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm80")
                {
                    para[80] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm81")
                {
                    para[81] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm82")
                {
                    para[82] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm83")
                {
                    para[83] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm84")
                {
                    para[84] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm85")
                {
                    para[85] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm86")
                {
                    para[86] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm87")
                {
                    para[87] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm88")
                {
                    para[88] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm89")
                {
                    para[89] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm90")
                {
                    para[90] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm91")
                {
                    para[91] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm92")
                {
                    para[92] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm93")
                {
                    para[93] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm94")
                {
                    para[94] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm95")
                {
                    para[95] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm96")
                {
                    para[96] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm97")
                {
                    para[97] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm98")
                {
                    para[98] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm99")
                {
                    para[99] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm100")
                {
                    para[100] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm101")
                {
                    para[101] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm102")
                {
                    para[102] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm103")
                {
                    para[103] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm104")
                {
                    para[104] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm105")
                {
                    para[105] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm106")
                {
                    para[106] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm107")
                {
                    para[107] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm108")
                {
                    para[108] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm109")
                {
                    para[109] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm110")
                {
                    para[110] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm111")
                {
                    para[111] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm112")
                {
                    para[112] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm113")
                {
                    para[113] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm114")
                {
                    para[114] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm115")
                {
                    para[115] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm116")
                {
                    para[116] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm117")
                {
                    para[117] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm118")
                {
                    para[118] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm119")
                {
                    para[119] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm120")
                {
                    para[120] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm121")
                {
                    para[121] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm122")
                {
                    para[122] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm123")
                {
                    para[123] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm124")
                {
                    para[124] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm125")
                {
                    para[125] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm126")
                {
                    para[126] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm127")
                {
                    para[127] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm128")
                {
                    para[128] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm129")
                {
                    para[129] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm130")
                {
                    para[130] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm131")
                {
                    para[131] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm132")
                {
                    para[132] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm133")
                {
                    para[133] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm134")
                {
                    para[134] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm135")
                {
                    para[135] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm136")
                {
                    para[136] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm137")
                {
                    para[137] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm138")
                {
                    para[138] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm139")
                {
                    para[139] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm140")
                {
                    para[140] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm141")
                {
                    para[141] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm142")
                {
                    para[142] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm143")
                {
                    para[143] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm144")
                {
                    para[144] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Parm145")
                {
                    para[145] = Convert.ToDouble(no.InnerText);
                }
            }
            foreach (XmlNode no in list2)
            {
                if (no.Attributes["Name"].InnerText == "料盒方向(00:极耳朝拉带01:极耳朝空料盘)")
                {
                    para[list1.Count + 0] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "单像素实际大小")
                {
                    para[list1.Count + 1] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "料盒的数量")
                {
                    para[list1.Count + 2] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "拍照次数")
                {
                    para[list1.Count + 3] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "空料盒的灰度值(p=2)(0-255)")
                {
                    para[list1.Count + 4] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "料盒边缘灰度值(0-255)")
                {
                    para[list1.Count + 5] = Convert.ToDouble(no.InnerText);
                }
                else if (no.Attributes["Name"].InnerText == "Tray盘有无棱边(0:无棱边 1:有棱边)")
                {
                    para[list1.Count + 6] = Convert.ToDouble(no.InnerText);
                }
            }

            //设置Tray盘相机曝光时间，add by liyang 2019-10-21
            //mTrayCamera.SetExposureTime(para[35]);

            //Tray盘拍照曝光时间
            //m_BatteryVisionConfig.dTrayCameraExposureTime = para[35];
            //异物检测曝光时间
            //m_BatteryVisionConfig.dTrayCameraForiegnDetectExposureTime = para[78];

            //m_BatteryVisionConfig.dBatteryInTrayDetectExposureTime = para[143];

            _vision.SetPara(para);

            XmlNode node1_3 = node0.SelectSingleNode("RemodelParameter");

            XmlNodeList list3 = node1_3.SelectNodes("Group");

            foreach (XmlNode zz in list3)
            {
                XmlNode III = zz.SelectSingleNode("I");
                XmlNode MMM = zz.SelectSingleNode("M");
                XmlNode NNN = zz.SelectSingleNode("N");
                XmlNode PPP = zz.SelectSingleNode("P");

                int index = Convert.ToInt32(III.InnerText);
                string[] sm = MMM.InnerText.Split(',');
                string[] sn = NNN.InnerText.Split(',');
                string[] sp = PPP.InnerText.Split(',');

                int[] m = new int[sm.Length];
                int[] n = new int[sn.Length];
                int[] p = new int[sp.Length];

                for (int i = 0; i < sm.Length; i++)
                {
                    m[i] = Convert.ToInt32(sm[i]);
                }

                for (int i = 0; i < sn.Length; i++)
                {
                    n[i] = Convert.ToInt32(sn[i]);
                }

                for (int i = 0; i < sp.Length; i++)
                {
                    p[i] = Convert.ToInt32(sp[i]);
                }

                _vision.SetMNP(index, m, n, p);
            }

            return 0;
        }

        //ljx
        public void ThreadANDON()
        {
            while (true)
            {
                Thread.Sleep(20);
                if (!GetProgramConfig(p => p.ANDONFlag))
                {
                    continue;
                }
                int control = int.Parse(m_IniCtrl.GetANDONIni("ANDON", "CONTROL"));
                if (control == 1)
                {
                    //_netPLCControl.WriteRegister(77, 1);
                }
                DateTime t1 = DateTime.Parse(m_IniCtrl.GetANDONIni("ANDON", "UPDATE"));
                TimeSpan t = DateTime.Now - t1;
                int overSecond = (int)t.TotalSeconds;
                if (overSecond > 180) //超过3分钟停机?
                {
                    //TODO:PLC与ANDON相关还未知
                    //_netPLCControl.WriteRegister(77, 1);
                }
                Thread.Sleep(300);
            }
        }



        public void ThreadAlarm()
        {
            while (true)
            {
                //TODO:报警线程,还需要考虑
                //if (!m_SysState.GetPLCWarning())
                //{
                //    try
                //    {
                //        //获取PLC报警地址
                //        //根据地址，从mysql获取中文
                //        string AlarmContent = "PLC报警";
                //        //上传ANDON
                //        sw.WriteLine(";;" + DateTime.Now.ToString("dd-MM-yyyy;HH:mm:ss") + ";;;;;;;;;;" + AlarmContent);
                //        LocalApi.InsertAlarmExecute(AlarmContent);
                //    }
                //    catch (Exception err)
                //    {
                //        SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "写PLC报警出错," + err.ToString());
                //    }
                //}

                Thread.Sleep(100);
            }
        }
    }
}