using ATL_MC.MainCtrl.Enum;
using ATL_MC.MainCtrl.Polly;
using ATL_MC.MainCtrl.System;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ATL_MC.MainCtrl
{
    //传输带线程  控制:拉带 扫码枪 光源 相机 拉带视觉
    public partial class MainCtrl
    {
        /// <summary>
        /// 重试10次,间隔20ms
        /// </summary>
        public ISyncPolicy PolicyTypeA
        {
            get { return PolicyProfile.Build(10, 20); }
        }
        /// <summary>
        /// 获取系统状态
        /// </summary>
        /// <param name="func"></param>
        public T GetSysStatus<T>(Func<SystemStatus, T> func)
        {
            return _statusManager.Get<SystemStatus, T>(func);
        }
        /// <summary>
        /// 设置系统状态
        /// </summary>
        /// <param name="action"></param>
        public void SetSysStatus(Action<SystemStatus> action)
        {
            _statusManager.Set<SystemStatus>(action);
        }
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="func"></param>
        public T GetSysConfig<T>(Func<SystemConfig, T> func)
        {
            return _statusManager.Get<SystemConfig, T>(func);
        }

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="func"></param>
        public T GetProgramConfig<T>(Func<ProgramConfig, T> func)
        {
            return _statusManager.Get<ProgramConfig, T>(func);
        }


        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="func"></param>
        public T GetPLCAddr<T>(Func<PLCStatus, T> func)
        {
            return _statusManager.Get<PLCStatus, T>(func);
        }
        /// <summary>
        /// 获取机械手IO地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T GetRobotIO<T>(Func<RobotStatus, T> func)
        {
            return _statusManager.Get<RobotStatus, T>(func);
        }
        /// <summary>
        /// 获取产品参数
        /// </summary>
        /// <param name="func"></param>
        public T GetProductParam<T>(Func<ProductParam, T> func)
        {
            return _statusManager.Get<ProductParam, T>(func);
        }
        // GetProductParam<double>(p=>p.ProductInfo(EnumTrayType.TrayA,1).LIGHT_1);
        // GetProductParam<double>(p=>p.ProductInfo(EnumTrayType.TrayA,1).LIGHT_1);
        // GetProductParam<double>(p => p.MoveInLight_1);
        /// <summary>
        /// 获取系统状态
        /// </summary>
        /// <param name="func"></param>
        public T GetSimulate<T>(Func<SimulateConfig, T> func)
        {
            return _statusManager.Get<SimulateConfig, T>(func);
        }

        /// <summary>
        /// 软拍照
        /// </summary>
        /// <param name="baslerCamera"></param>
        /// <param name="picWidth"></param>
        /// <param name="picHeight"></param>
        /// <param name="threadName"></param>
        /// <returns></returns>
        private byte[] TakePicture(BaslerCamera.BaslerCamera baslerCamera, long picWidth, long picHeight, string threadName)
        {
            byte[] buff = new byte[picWidth * picHeight];
            PolicyTypeA.Execute(() =>
            {
                if (1 == baslerCamera.TakeSinglePicture(buff, buff.Length))
                {
                    throw new Exception(threadName + ":拍照失败");
                }
                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, threadName + ":拍照成功");
            });
            return buff;
        }
        /// <summary>
        /// 开灯,拉带trayType,slot无效
        /// </summary>
        /// <param name="kangShiDaLightController"></param>
        /// <param name="threadName">线程名称</param>
        /// <param name="process">工序位置</param>
        /// <param name="slot">槽位,0代表空盘</param>
        private void SetLightBox(ATL_MC.KangShiDaLightController.KangShiDaLightController kangShiDaLightController, string threadName, EnumProcess process, EnumTrayType trayType, int slot = 0)
        {
            //先开灯
            PolicyTypeA.Execute(() =>
            {
                if (process == EnumProcess.MoveIn)
                {
                    //拉带开灯
                    if (1 == kangShiDaLightController.SetLightBox(GetProductParam<int>(p => p.MoveInLight_1), GetProductParam<int>(p => p.MoveInLight_2)))
                    {
                        throw new Exception($"{threadName}:开灯失败");
                    }
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, $"{threadName}:{process.ToString()}工序,开灯成功");
                }
                else
                {
                    //tray盘开灯
                    if (1 == kangShiDaLightController.SetLightBox(
                        GetProductParam<int>(p => p.ProductInfo(trayType, slot).Brightness_1),
                        GetProductParam<int>(p => p.ProductInfo(trayType, slot).Brightness_2),
                        GetProductParam<int>(p => p.ProductInfo(trayType, slot).Brightness_3),
                        GetProductParam<int>(p => p.ProductInfo(trayType, slot).Brightness_4)
                       ))
                    {
                        throw new Exception($"{threadName}:{process.ToString()}工序,{slot}槽位开灯失败");
                    }
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, $"{threadName}:{process.ToString()}工序,{slot}槽位开灯成功");
                }
            });
        }

        /// <summary>
        /// 扫码
        /// </summary>
        /// <param name="threadStep"></param>
        private void ScanCode()
        {
            PolicyTypeA.Execute(() =>
            {
                if (_barcodeScanner.StartRead())
                {
                    int ret = 0;
                    string barcode = string.Empty;
                    ret = _barcodeScanner.GetBarCode(out barcode);
                    _barcodeScanner.StopRead();
                    if (0 == ret)
                    {
                        string error = "ERROR";
                        barcode = barcode.Replace("\r", "");
                        if (barcode.IndexOf(error) > -1)
                        {
                            SetSysStatus(p => p.EnumBarcodeScanner_ResultType = EnumBarcodeScanner_ResultType.Err);
                            SetSysStatus(p => p.BarCode_Current = "BarCode Err");
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadMoveIn:BarCode识别失败,NG料");
                            // threadStep = 100;     
                            // return BarcodeScanner_ResultType.Err;
                        }
                        else
                        {
                            SetSysStatus(p => p.EnumBarcodeScanner_ResultType = EnumBarcodeScanner_ResultType.OK);
                            SetSysStatus(p => p.BarCode_Current = barcode);
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadMoveIn:BarCode识别成功,BarCode:" + barcode);
                            // threadStep++;
                            // return BarcodeScanner_ResultType.OK;
                        }
                    }
                    else
                    {
                        SetSysStatus(p => p.EnumBarcodeScanner_ResultType = EnumBarcodeScanner_ResultType.Fail);
                        SetSysStatus(p => p.BarCode_Current = "BarCode Fail");
                        SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadMoveIn:电池扫码失败,NG料");
                        //threadStep = 200;
                        // return BarcodeScanner_ResultType.Fail;
                    }
                }
                else
                {
                    //访问失败,加入timeout时间
                    //TODO:扫码成功重新设置回timeout时间?
                    _barcodeScanner.Get();
                    throw new Exception("ThreadMoveIn:访问扫码器失败");
                }
            });
        }

        /// <summary>
        /// 拉带线程  控制:拉带、扫码枪、光源、相机、拉带视觉
        /// </summary>
        public void ThreadMoveIn()
        {
            // int iMoveInPicRetryFlag = 0;
            // Stopwatch sw = new Stopwatch();
            //double x_offset = 0.0, y_offset = 0.0, a_offset = 0.0;
            //  int RET = 0;
            while (!bStopThread)
            {
                //TODO:暂不考虑错误及物理按键情况
                Thread.Sleep(5);
                //if (m_SysState.m_SysCriticalError)
                //    m_SysState.Thread_MoveInStep = 0;

                //if (m_SysState.m_SysStop)
                //    m_SysState.m_iMoveInStep = 0;

                //if (!m_SysState.GetSysStart())
                //    continue;

                //if (m_SysState.GetSysPause())
                // continue;

                //if (m_SysState.GetSysError())
                // continue;

                //TODO:暂不考虑步骤时间计算
                //TODO:暂不考虑PPM计算
                //TODO:暂不考虑整体错误处理             

                int threadStep = GetSysStatus<int>(p => p.Thread_MoveInStep);
                switch (threadStep)
                {
                    case 0:
                        //TODO:缺死循环跳出机制
                        Thread.Sleep(500);
                        break;
                    case 1:
                        //检查PLC信号,电池是否到达扫码区域 && 机械手是否在拉带区域
                        if (GetSysStatus<bool>(p => p.PLC_Output_MoveInCanScan) && !GetSysStatus<bool>(p => p.Robot_Ouput_RobotInMoveInArea))
                        {
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadMoveIn:开始工作");
                            SetLightBox(_lightController_MoveIn, "ThreadMoveIn", EnumProcess.MoveIn, default(EnumTrayType));                          
                            threadStep++;
                        }
                        break;
                    case 2:
                        //1.拍照->视觉处理   
                        //2.扫码->bis请求  
                        //1,2都处理完后进行下一步

                        //照相
                        Func<BaslerCamera.BaslerCamera, long, long, string, byte[]> takePictureFunc = TakePicture;
                        //扫码
                        Action barcodeScanAction = ScanCode;

                        //Bis请求
                        Func<string, string> bisRequestFunc = null;
                        //视觉处理
                        Func<byte[], string> visionFunc = null;
                        IAsyncResult visionResult = null;
                        IAsyncResult bisResult = null;
                        long moveInPicWidth = m_CameraConfig.CameraWidth_MoveIn;
                        long moveInPicHeight = m_CameraConfig.CameraHeight_MoveIn;

                        var result1 = takePictureFunc.BeginInvoke(_moveInCamera, moveInPicWidth, moveInPicHeight, "ThreadMoveIn", new AsyncCallback(ar =>
                        {
                            //照相完成,进行视觉处理
                            byte[] buffPic = takePictureFunc.EndInvoke(ar);
                            visionFunc = buffer =>
                            {
                                double x_offset = 0.0;
                                double y_offset = 0.0;
                                double a_offset = 0.0;
                                string moveInPictureFilePath = string.Empty;
                                //视觉处理
                                if (0 == mVision.GetBatteryPos(buffer, moveInPicWidth, moveInPicHeight, out x_offset, out y_offset, out a_offset, moveInPictureFilePath, GetSimulate<bool>(p => p.Simulate_Vision)))
                                {
                                    //保存相片,TODO:相片保存路径，是否需要经过视觉处理后才取得？，如不需要视觉处理后才取得则可异步，后续处理
                                    Task.Run(() =>
                                    {
                                        mVision.SavePicJPEG(buffer, (int)moveInPicWidth, (int)moveInPicHeight, moveInPictureFilePath + ".jpg");
                                    });
                                    //TODO:偏差标准见规格书
                                    if ((Math.Abs(x_offset) > 60.0) || (Math.Abs(y_offset) > 60.0) || (Math.Abs(a_offset) > 45.0))
                                    {
                                        SYS_IBG_LOG(WARNINGERR, 0, 0, "ThreadMoveIn:电池定位偏差过大，请取走拉带上相机视野内的电池");
                                        _lightController_MoveIn.SetLightBox(0, 0, 0, 0);
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_BatteryDeviationTooLarge), 1);//告诉PLC,电池偏差过大报警
                                        return "BatteryDeviationTooLarge";
                                    }
                                    else
                                    {
                                        SetSysStatus(p => { p.CatchOffsetX_Current = x_offset; p.CatchOffsetY_Current = y_offset; p.CatchOffsetAngle_Current = a_offset; });
                                        return "OK";
                                    }
                                }
                                else
                                {
                                    //TODO:视觉处理失败,此处未使用Polly,重试方案后续处理
                                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadMoveIn:视觉检测到问题");
                                    return "视觉检测的结果";
                                }
                            };
                            visionResult = visionFunc.BeginInvoke(buffPic, null, null);
                        }), null);
                        var result2 = barcodeScanAction.BeginInvoke(new AsyncCallback(ar =>
                        {
                            //扫码完成,异步进行BIS申请 TODO:BIS数据申请
                            bisRequestFunc = barcode =>
                            {
                                var barcode1 = GetSysStatus<string>(p => p.BarCode_Current);
                                if (barcode1 == "BarCode Err")
                                {
                                    return "BarCode Err";
                                }
                                else if (barcode1 == "BarCode Fail")
                                {
                                    return "BarCode Fail";
                                }
                                else if (barcode1 == "视觉检测的结果")
                                {
                                    return "视觉检测的结果";
                                }
                                else
                                {
                                    //TODO:BIS数据申请,暂略,

                                    //TODO:根据BIS请求结果,赋值
                                    SetSysStatus(p => p.EnumBisReturn_ResultType_Current = EnumBisReturn_ResultType.TrayA);
                                    return "";
                                }
                            };
                            bisResult = bisRequestFunc.BeginInvoke(GetSysStatus<string>(p => p.BarCode_Current), null, null);
                        }), null);
                        var strBis = string.Empty;
                             var strVision = string.Empty;
                        try
                        {
                            //等待BIS及视觉处理结果
                            //TODO:此处会报错？后续处理
                            while (!(bisResult.IsCompleted || visionResult.IsCompleted))
                            {
                                Thread.Sleep(20);
                            }
                             strBis = bisRequestFunc.EndInvoke(bisResult);
                             strVision = visionFunc.EndInvoke(visionResult);
                        }
                        catch (Exception)
                        {

                            
                        }
                       
                       

                        if (strBis == "BarCode Err")
                        {
                            threadStep = 100;
                        }
                        else if (strBis == "BarCode Fail")
                        {
                            threadStep = 200;
                        }
                        else if (strVision == "BatteryDeviationTooLarge")
                        {
                            threadStep = 300;
                        }
                        else if (strVision == "视觉检测的结果")
                        {
                            //TODO:按检测结果处理
                        }
                        else
                        {
                            SetSysStatus(p=>p.Sys_BatteryIsReady=true);
                            //OK
                            threadStep++;
                        }
                        break;
                    case 3:
                        //等待机械手放好电池
                        if (GetSysStatus<bool>(p => p.Sys_MoveInContinus))
                        {
                            SetSysStatus(p => p.Sys_MoveInContinus = false);
                            threadStep++;
                        }
                        Thread.Sleep(20);
                        break;
                    case 4:
                        //储存变量
                        SetSysStatus(p => p.EnumBisReturn_ResultType_Running = p.EnumBisReturn_ResultType_Current);
                        SetSysStatus(p => p.BarCode_Running = p.BarCode_Current);
                        SetSysStatus(p => { p.CatchOffsetX_Running = p.CatchOffsetX_Current; p.CatchOffsetY_Running = p.CatchOffsetY_Current; p.CatchOffsetAngle_Running = p.CatchOffsetAngle_Current; }); ;
                        SetSysStatus(p => p.Sys_BatteryIsReady = true);
                        threadStep = 1;



                        //var resultType= GetSysStatus<EnumBisReturn_ResultType>(p => p.EnumBisReturn_ResultType);
                        ////储存变量
                        //if (resultType== EnumBisReturn_ResultType.TrayA)
                        //{
                        //    SetSysStatus(p=>p.BarCode_Running =p.BarCode_Current);
                        //    SetSysStatus(p => { p.CatchOffsetX_Running = p.CatchOffsetX_Current; p.CatchOffsetY_Running = p.CatchOffsetY_Current; p.CatchOffsetAngle_Running = p.CatchOffsetAngle_Current; }); ;
                        //    SetSysStatus(p=>p.Sys_BatteryIsReady=true);
                        //}
                        //else if (resultType == EnumBisReturn_ResultType.TrayB)
                        //{
                        //    SetSysStatus(p => p.BarCode_Running = p.BarCode_Current);
                        //    SetSysStatus(p => { p.CatchOffsetX_Running = p.CatchOffsetX_Current; p.CatchOffsetY_Running = p.CatchOffsetY_Current; p.CatchOffsetAngle_Running = p.CatchOffsetAngle_Current; }); ;
                        //    SetSysStatus(p => p.Sys_BatteryIsReady = true);
                        //}
                        //else if (resultType == EnumBisReturn_ResultType.TrayC)
                        //{
                        //    SetSysStatus(p => p.BarCode_Running = p.BarCode_Current);
                        //    SetSysStatus(p => { p.CatchOffsetX_Running = p.CatchOffsetX_Current; p.CatchOffsetY_Running = p.CatchOffsetY_Current; p.CatchOffsetAngle_Running = p.CatchOffsetAngle_Current; }); ;
                        //    SetSysStatus(p => p.Sys_BatteryIsReady = true);
                        //}
                        //else if (resultType == EnumBisReturn_ResultType.TrayD)
                        //{
                        //    SetSysStatus(p => p.BarCode_Running = p.BarCode_Current);
                        //    SetSysStatus(p => { p.CatchOffsetX_Running = p.CatchOffsetX_Current; p.CatchOffsetY_Running = p.CatchOffsetY_Current; p.CatchOffsetAngle_Running = p.CatchOffsetAngle_Current; }); ;
                        //    SetSysStatus(p => p.Sys_BatteryIsReady = true);
                        //}
                        //else if (resultType == EnumBisReturn_ResultType.TrayE)
                        //{
                        //    SetSysStatus(p => p.BarCode_Running = p.BarCode_Current);
                        //    SetSysStatus(p => { p.CatchOffsetX_Running = p.CatchOffsetX_Current; p.CatchOffsetY_Running = p.CatchOffsetY_Current; p.CatchOffsetAngle_Running = p.CatchOffsetAngle_Current; }); ;
                        //    SetSysStatus(p => p.Sys_BatteryIsReady = true);
                        //}
                        //else
                        //{
                        //    //NG
                        //    SetSysStatus(p => p.BarCode_NG = p.BarCode_Current);
                        //    SetSysStatus(p => { p.CatchOffsetX_NG = p.CatchOffsetX_Current; p.CatchOffsetY_NG = p.CatchOffsetY_Current; p.CatchOffsetAngle_NG = p.CatchOffsetAngle_Current; }); ;
                        //    SetSysStatus(p => p.Sys_BatteryIsReady = true);
                        //}

                        break;
                    case 5:


                        break;
                    case 100://ThreadMoveIn:BarCode识别失败,NG料


                        break;
                    case 200://ThreadMoveIn:扫码枪扫码失败,NG料


                        break;
                    case 300://ThreadMoveIn:电池偏差过大


                        break;
                }

                if (threadStep!=GetSysStatus(p => p.Thread_MoveInStep) )
                {
                    SetSysStatus(p => p.Thread_MoveInStep = threadStep);
                }  
            }
        }


    }
}
