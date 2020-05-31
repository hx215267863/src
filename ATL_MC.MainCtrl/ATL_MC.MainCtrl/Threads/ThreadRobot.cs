using ATL_MC.EpsonScaraRobotController;
using ATL_MC.MainCtrl.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ATL_MC.MainCtrl
{
    /// <summary>
    /// 机械手线程 控制机械手
    /// </summary>
    public partial class MainCtrl
    {
        /// <summary>
        /// 获取各默认(示教)位置
        /// 注意调用该方法的时机
        /// </summary>
        /// <param name="enumDefaultPosition"></param>
        /// <param name="slot">EnumDefaultPosition为trayA~trayE盘时才启作用</param>
        /// <returns></returns>
        private SixAxisPose GetThePosition(EnumDefaultPosition enumDefaultPosition, int slot = 0)
        {
            //TODO:需要限制调用此方法的时机?
            if (enumDefaultPosition == EnumDefaultPosition.Catch)
            {
                var offsetX = GetSysStatus<double>(p => p.CatchOffsetX_Running);
                var offsetY = GetSysStatus<double>(p => p.CatchOffsetY_Running);
                var offsetAngle = GetSysStatus<double>(p => p.CatchOffsetAngle_Running);
                var deltaAngleBetweenRobotAndCamera = GetSysConfig<double>(p => p.DeltaAngleBetweenRobotAndCamera);
                //转成世界坐标系的偏差值
                offsetX = offsetX * Math.Cos(deltaAngleBetweenRobotAndCamera * 3.1415926535 / 180.0) + offsetY * Math.Sin(deltaAngleBetweenRobotAndCamera * 3.1415926535 / 180.0);
                offsetY = offsetY * Math.Cos(deltaAngleBetweenRobotAndCamera * 3.1415926535 / 180.0) - offsetX * Math.Sin(deltaAngleBetweenRobotAndCamera * 3.1415926535 / 180.0);
                SixAxisPose position = new SixAxisPose();
                position.xAxis = GetSysConfig<double>(p => p.RobotPose_Catch.xAxis) + offsetX;
                position.yAxis = GetSysConfig<double>(p => p.RobotPose_Catch.yAxis) + offsetY;
                position.zAxis = GetSysConfig<double>(p => p.RobotPose_Catch.zAxis);
                position.rxAxis = GetSysConfig<double>(p => p.RobotPose_Catch.rxAxis);
                position.ryAxis = GetSysConfig<double>(p => p.RobotPose_Catch.ryAxis);
                position.rzAxis = GetSysConfig<double>(p => p.RobotPose_Catch.rzAxis) + offsetAngle;
                position.fig = GetSysConfig<string>(p => p.RobotPose_Catch.fig);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.NG)
            {
                SixAxisPose position = new SixAxisPose();
                position.xAxis = GetSysConfig<double>(p => p.RobotPose_NG.xAxis);
                position.yAxis = GetSysConfig<double>(p => p.RobotPose_NG.yAxis);
                position.zAxis = GetSysConfig<double>(p => p.RobotPose_NG.zAxis);
                position.rxAxis = GetSysConfig<double>(p => p.RobotPose_NG.rxAxis);
                position.ryAxis = GetSysConfig<double>(p => p.RobotPose_NG.ryAxis);
                position.rzAxis = GetSysConfig<double>(p => p.RobotPose_NG.rzAxis);
                position.fig = GetSysConfig<string>(p => p.RobotPose_NG.fig);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.StandyA)
            {
                SixAxisPose position = new SixAxisPose();
                position.xAxis = GetSysConfig<double>(p => p.RobotPose_StandbyA.xAxis);
                position.yAxis = GetSysConfig<double>(p => p.RobotPose_StandbyA.yAxis);
                position.zAxis = GetSysConfig<double>(p => p.RobotPose_StandbyA.zAxis);
                position.rxAxis = GetSysConfig<double>(p => p.RobotPose_StandbyA.rxAxis);
                position.ryAxis = GetSysConfig<double>(p => p.RobotPose_StandbyA.ryAxis);
                position.rzAxis = GetSysConfig<double>(p => p.RobotPose_StandbyA.rzAxis);
                position.fig = GetSysConfig<string>(p => p.RobotPose_StandbyA.fig);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.StandyB)
            {
                SixAxisPose position = new SixAxisPose();
                position.xAxis = GetSysConfig<double>(p => p.RobotPose_StandbyB.xAxis);
                position.yAxis = GetSysConfig<double>(p => p.RobotPose_StandbyB.yAxis);
                position.zAxis = GetSysConfig<double>(p => p.RobotPose_StandbyB.zAxis);
                position.rxAxis = GetSysConfig<double>(p => p.RobotPose_StandbyB.rxAxis);
                position.ryAxis = GetSysConfig<double>(p => p.RobotPose_StandbyB.ryAxis);
                position.rzAxis = GetSysConfig<double>(p => p.RobotPose_StandbyB.rzAxis);
                position.fig = GetSysConfig<string>(p => p.RobotPose_StandbyB.fig);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.TrayA)
            {
                SixAxisPose position = GetProductParam<SixAxisPose>(p => p.ProductInfo(EnumTrayType.TrayA, slot).RobotPose_Put);                          
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.TrayB)
            {
                SixAxisPose position = GetProductParam<SixAxisPose>(p => p.ProductInfo(EnumTrayType.TrayB, slot).RobotPose_Put);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.TrayC)
            {
                SixAxisPose position = GetProductParam<SixAxisPose>(p => p.ProductInfo(EnumTrayType.TrayC, slot).RobotPose_Put);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.TrayD)
            {
                SixAxisPose position = GetProductParam<SixAxisPose>(p => p.ProductInfo(EnumTrayType.TrayD, slot).RobotPose_Put);
                return position;
            }
            else if (enumDefaultPosition == EnumDefaultPosition.TrayE)
            {
                SixAxisPose position = GetProductParam<SixAxisPose>(p => p.ProductInfo(EnumTrayType.TrayE, slot).RobotPose_Put);
                return position;
            }
            else
            {
                throw new Exception("无此类型位置数据");
            }
        }


        //机械手还需要处理的问题,
        //1.目前机械手是回到待机位,才会激活视觉处理和写入产量,并且是同步的,需要优化
        //2.需要考虑,报警,暂停等情况,恢复机械手时,判断机械手所处位置和是否抓着电池!
        //3.产量写入,PPM计算未处理!


        //机械手线程 控制机械手
        public void ThreadRobot()
        {
            Stopwatch sw = new Stopwatch();

            while (!bStopThread)
            {
                Thread.Sleep(5);
                //if (m_SysState.GetSysCriticalError())
                //    m_SysState.m_iScaraStep = 0;
                //if (m_SysState.GetStopFlag())
                //    m_SysState.m_iScaraStep = 0;
                //if (!m_SysState.GetSysStart())
                //    continue;
                //if (m_SysState.GetSysPause())
                //    continue;
                //if (m_SysState.GetSysError())
                //    continue;
                int threadStep = GetSysStatus<int>(p => p.Thread_RobotStep);
                switch (threadStep)
                {
                    case 0:
                        //TODO:缺死循环跳出机制
                        Thread.Sleep(500);
                        break;
                    case 1:
                        if (GetSysStatus(p => p.Sys_BatteryIsReady))
                        {
                            SetSysStatus(p => p.Sys_MoveInContinus = true);
                            if (EnumBisReturn_ResultType.NG == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                            {
                                threadStep = 100;
                            }
                            else
                            {
                                threadStep++;
                            }                    
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:机械手开始工作");
                        }
                        Thread.Sleep(20);
                        break;
                    case 2:
                        //检查三个条件=> Bis结果为TrayA,TrayA已准备好,视觉检测已准备好
                        if (GetSysStatus(p => p.PLC_Output_IsReady_TrayA) && GetSysStatus(p => p.VisionChecked_TrayA) && EnumBisReturn_ResultType.TrayA == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayA = false);
                            SetSysStatus(p => p.VisionChecked_TrayA = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池,目标：TrayA");
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.Catch),
                                   // GetThePosition(EnumDefaultPosition.TrayA,),
                                   GetThePosition(EnumDefaultPosition.TrayA, GetSysStatus(p => p.CurrentReleaseIndex_TrayA)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:完成抓放电池");
                            //待激活视觉检测功能
                            threadStep++;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayB) && GetSysStatus(p => p.VisionChecked_TrayB) && EnumBisReturn_ResultType.TrayB == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayB = false);
                            SetSysStatus(p => p.VisionChecked_TrayB = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池,目标：TrayB");
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.Catch),
                                  GetThePosition(EnumDefaultPosition.TrayB, GetSysStatus(p => p.CurrentReleaseIndex_TrayB)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:完成抓放电池");
                            //待激活视觉检测功能
                            threadStep++;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayC) && GetSysStatus(p => p.VisionChecked_TrayC) && EnumBisReturn_ResultType.TrayC == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayC = false);
                            SetSysStatus(p => p.VisionChecked_TrayC = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池,目标：TrayC");
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.Catch),
                               GetThePosition(EnumDefaultPosition.TrayC, GetSysStatus(p => p.CurrentReleaseIndex_TrayC)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:完成抓放电池");
                            //待激活视觉检测功能
                            threadStep++;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayD) && GetSysStatus(p => p.VisionChecked_TrayD) && EnumBisReturn_ResultType.TrayD == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayD = false);
                            SetSysStatus(p => p.VisionChecked_TrayD = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池,目标：TrayD");
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.Catch),
                            GetThePosition(EnumDefaultPosition.TrayD, GetSysStatus(p => p.CurrentReleaseIndex_TrayD)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:完成抓放电池");
                            //待激活视觉检测功能
                            threadStep++;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayE) && GetSysStatus(p => p.VisionChecked_TrayE) && EnumBisReturn_ResultType.TrayE == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayE = false);
                            SetSysStatus(p => p.VisionChecked_TrayE = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池,目标：TrayE");
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.Catch),
                                GetThePosition(EnumDefaultPosition.TrayE, GetSysStatus(p => p.CurrentReleaseIndex_TrayE)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:完成抓放电池");
                            //待激活视觉检测功能
                            threadStep++;
                        }
                        else
                        {
                            if (EnumBisReturn_ResultType.TrayA == GetSysStatus(p => p.EnumBisReturn_ResultType_Running) || EnumBisReturn_ResultType.TrayB == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                            {
                                
                                //抓到待机位A
                                _robotController.ROBOT_CatchAndPutBattery(
                                   0,//TODO:机械手运动模式未设置
                                   GetThePosition(EnumDefaultPosition.Catch),
                                   GetThePosition(EnumDefaultPosition.StandyA),
                                   GetSysConfig(p => p.ZAxiaSafePosition)
                                   );
                                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池到待机位A");
                                threadStep = 200;
                            }
                            else
                            {
                                //抓到待机位B
                                _robotController.ROBOT_CatchAndPutBattery(
                                   0,//TODO:机械手运动模式未设置
                                   GetThePosition(EnumDefaultPosition.Catch),
                                   GetThePosition(EnumDefaultPosition.StandyB),
                                   GetSysConfig(p => p.ZAxiaSafePosition)
                                   );
                                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始抓取电池到待机位B");
                                threadStep = 300;
                            }
                        }
                        break;
                    case 3:
                        
                        Task.Run(()=> {
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, $"ThreadRobot:{GetSysStatus(p => p.EnumBisReturn_ResultType_Running).ToString()}开始视觉检测");
                            //视觉检测,TODO:此处视觉处理是机械到回到待机位才开始,需要优化!!! 移至料盘线程？！
                            BaslerCamera.BaslerCamera baslerCamera = null;
                            long picWidth, picHeight = 0;
                            if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayA)
                            {
                                baslerCamera = _trayACamera;
                                picWidth = m_CameraConfig.CameraWidth_TrayA; //.lTrayA_CameraWidth;
                                picHeight = m_CameraConfig.CameraHeight_TrayA;
                                //拍照+保存相片+视觉处理    
                                TrayVisionHandle("TrayA", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                                SetSysStatus(p => p.VisionChecked_TrayA = true);
                                SetSysStatus(p => p.Sys_MoveInContinus = true);
                                //TODO:写入产量等操作 放到下一步
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayB)
                            {                              
                                baslerCamera = _trayBCamera;
                                picWidth = m_CameraConfig.CameraWidth_TrayB;
                                picHeight = m_CameraConfig.CameraHeight_TrayB;
                                TrayVisionHandle("TrayB", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                                SetSysStatus(p => p.VisionChecked_TrayB = true);
                                //TODO:写入产量等操作 放到下一步
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayC)
                            {                         
                                baslerCamera = _trayCCamera;
                                picWidth = m_CameraConfig.CameraWidth_TrayC;
                                picHeight = m_CameraConfig.CameraHeight_TrayC;
                                TrayVisionHandle("TrayC", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                                SetSysStatus(p => p.VisionChecked_TrayC = true);
                                //TODO:写入产量等操作 放到下一步
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayD)
                            {                               
                                baslerCamera = _trayDCamera;
                                picWidth = m_CameraConfig.CameraWidth_TrayD;
                                picHeight = m_CameraConfig.CameraHeight_TrayD;
                                TrayVisionHandle("TrayD", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                                SetSysStatus(p => p.VisionChecked_TrayD = true);
                                //TODO:写入产量等操作 放到下一步
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayE)
                            {                              
                                baslerCamera = _trayECamera;
                                picWidth = m_CameraConfig.CameraWidth_TrayE;
                                picHeight = m_CameraConfig.CameraHeight_TrayE;
                                TrayVisionHandle("TrayE", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                                SetSysStatus(p => p.VisionChecked_TrayE = true);
                                //TODO:写入产量等操作 放到下一步
                            }
                            else
                            {
                                throw new Exception("不存在此种情况");
                            }

                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, $"ThreadRobot:{GetSysStatus(p => p.EnumBisReturn_ResultType_Running).ToString()}视觉检测完成");

                            #region //换料,出料,计算产量,计算PPM等操作 
                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, $"ThreadRobot:{GetSysStatus(p => p.EnumBisReturn_ResultType_Running).ToString()}开始写入产量");
                            //换料,出料,计算产量,计算PPM等操作  
                            var qtyForCrib = GetProductParam(p => p.QTY_FOR_CRIB);//每lot数量
                            var qtyForTray = GetProductParam(p => p.QTY_FOR_TRAY);//每盘数量
                            if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayA)
                            {
                                var trayAReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayA);
                                if (qtyForTray >= 4)
                                {
                                    if (qtyForTray - trayAReleaseIndex == 2)//倒数第二格
                                    {
                                        //通知PLC TrayA准备换盘了
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayA), 1);
                                    }
                                    else if (qtyForTray - trayAReleaseIndex == 1)//最后一格
                                    {
                                        //TODO:换lot和换料盘等相关操作待定
                                        //通知PLC TrayA换盘
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayA), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayA = false);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    if (qtyForTray - trayAReleaseIndex == 1)//最后一格
                                    {
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayA), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayA = false);
                                    }
                                    else
                                    {

                                    }
                                }
                                SetSysStatus(p => p.CurrentReleaseIndex_TrayA++);
                                //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayB)
                            {
                                var TrayBReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayB);
                                if (qtyForTray >= 4)
                                {
                                    if (qtyForTray - TrayBReleaseIndex == 2)//倒数第二格
                                    {
                                        //通知PLC TrayB准备换盘了
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayB), 1);
                                    }
                                    else if (qtyForTray - TrayBReleaseIndex == 1)//最后一格
                                    {
                                        //TODO:换lot和换料盘等相关操作待定
                                        //通知PLC TrayB换盘
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayB), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayB = false);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    if (qtyForTray - TrayBReleaseIndex == 1)//最后一格
                                    {
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayB), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayB = false);
                                    }
                                    else
                                    {

                                    }
                                }
                                SetSysStatus(p => p.CurrentReleaseIndex_TrayB++);
                                //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayC)
                            {
                                var TrayCReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayC);
                                if (qtyForTray >= 4)
                                {
                                    if (qtyForTray - TrayCReleaseIndex == 2)//倒数第二格
                                    {
                                        //通知PLC TrayC准备换盘了
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayC), 1);
                                    }
                                    else if (qtyForTray - TrayCReleaseIndex == 1)//最后一格
                                    {
                                        //TODO:换lot和换料盘等相关操作待定
                                        //通知PLC TrayC换盘
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayC), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayC = false);
                                    }
                                    else
                                    {
                                        //TODO:正常情况,暂不处理,留位
                                    }
                                }
                                else
                                {
                                    if (qtyForTray - TrayCReleaseIndex == 1)//最后一格
                                    {
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayC), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayC = false);
                                    }
                                    else
                                    {
                                        //TODO:正常情况,暂不处理,留位
                                    }
                                }
                                SetSysStatus(p => p.CurrentReleaseIndex_TrayC++);
                                //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理                            
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayD)
                            {
                                var TrayDReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayD);
                                if (qtyForTray >= 4)
                                {
                                    if (qtyForTray - TrayDReleaseIndex == 2)//倒数第二格
                                    {
                                        //通知PLC TrayD准备换盘了
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayD), 1);
                                    }
                                    else if (qtyForTray - TrayDReleaseIndex == 1)//最后一格
                                    {
                                        //TODO:换lot和换料盘等相关操作待定
                                        //通知PLC TrayD换盘
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayD), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayD = false);
                                    }
                                    else
                                    {
                                        //TODO:正常情况,暂不处理,留位
                                    }
                                }
                                else
                                {
                                    if (qtyForTray - TrayDReleaseIndex == 1)//最后一格
                                    {
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayD), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayD = false);
                                    }
                                    else
                                    {
                                        //TODO:正常情况,暂不处理,留位
                                    }
                                }
                                SetSysStatus(p => p.CurrentReleaseIndex_TrayD++);
                                //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                            }
                            else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayE)
                            {
                                var TrayEReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayE);
                                if (qtyForTray >= 4)
                                {
                                    if (qtyForTray - TrayEReleaseIndex == 2)//倒数第二格
                                    {
                                        //通知PLC TrayE准备换盘了
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayE), 1);
                                    }
                                    else if (qtyForTray - TrayEReleaseIndex == 1)//最后一格
                                    {
                                        //TODO:换lot和换料盘等相关操作待定
                                        //通知PLC TrayE换盘
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayE), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayE = false);
                                    }
                                    else
                                    {
                                        //TODO:正常情况,暂不处理,留位
                                    }
                                }
                                else
                                {
                                    if (qtyForTray - TrayEReleaseIndex == 1)//最后一格
                                    {
                                        _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayE), 1);
                                        SetSysStatus(p => p.VisionChecked_TrayE = false);
                                    }
                                    else
                                    {
                                        //TODO:正常情况,暂不处理,留位
                                    }
                                }
                                SetSysStatus(p => p.CurrentReleaseIndex_TrayE++);
                                //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                            }
                            else
                            {
                                throw new Exception("不存在此种情况");
                            }

                            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, $"ThreadRobot:{GetSysStatus(p => p.EnumBisReturn_ResultType_Running).ToString()}产量写入完成");
                            #endregion

                        });
                        threadStep = 1;
                        break;
                    case 4:
                        
                        break;
                    case 5:


                        break;
                    case 6:


                        break;
                    case 7:


                        break;
                    case 100:
                        //NG流程     
                        if (!GetSysStatus(p => p.Robot_Input_NGBoxInplace) || GetSysStatus(p => p.Robot_Input_NGBoxIsFull))
                        {
                            break;
                        }
                        //TODO:NG流程模式未设置     
                        _robotController.ROBOT_CatchAndPutBattery(
                            0,
                            GetThePosition(EnumDefaultPosition.Catch),
                            GetThePosition(EnumDefaultPosition.NG),
                            GetSysConfig(p => p.ZAxiaSafePosition)
                            );
                        threadStep = 1;
                        break;
                    case 200:
                        //电池被抓到待机位A,等待条件准备好                      
                        if (GetSysStatus(p => p.PLC_Output_IsReady_TrayA) && GetSysStatus(p => p.VisionChecked_TrayA) && EnumBisReturn_ResultType.TrayA == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手从待机位A抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayA = false);
                            SetSysStatus(p => p.VisionChecked_TrayA = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.StandyA),
                                GetThePosition(EnumDefaultPosition.TrayA, GetSysStatus(p => p.CurrentReleaseIndex_TrayA)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            //跳到视觉检测
                            threadStep = 3;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayB) && GetSysStatus(p => p.VisionChecked_TrayB) && EnumBisReturn_ResultType.TrayB == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayB = false);
                            SetSysStatus(p => p.VisionChecked_TrayB = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.StandyA),
                                GetThePosition(EnumDefaultPosition.TrayB, GetSysStatus(p => p.CurrentReleaseIndex_TrayB)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            //跳到视觉检测
                            threadStep = 3;
                        }
                        else
                        {
                            //TODO:定义防呆功能,待定
                        }
                        break;
                    case 300:
                        if (GetSysStatus(p => p.PLC_Output_IsReady_TrayC) && GetSysStatus(p => p.VisionChecked_TrayC) && EnumBisReturn_ResultType.TrayC == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayC = false);
                            SetSysStatus(p => p.VisionChecked_TrayC = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.StandyB),
                                GetThePosition(EnumDefaultPosition.TrayC, GetSysStatus(p => p.CurrentReleaseIndex_TrayC)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            //跳到视觉检测
                            threadStep = 3;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayD) && GetSysStatus(p => p.VisionChecked_TrayD) && EnumBisReturn_ResultType.TrayD == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayD = false);
                            SetSysStatus(p => p.VisionChecked_TrayD = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.StandyB),
                                GetThePosition(EnumDefaultPosition.TrayD, GetSysStatus(p => p.CurrentReleaseIndex_TrayD)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            //跳到视觉检测
                            threadStep = 3;
                        }
                        else if (GetSysStatus(p => p.PLC_Output_IsReady_TrayE) && GetSysStatus(p => p.VisionChecked_TrayE) && EnumBisReturn_ResultType.TrayE == GetSysStatus(p => p.EnumBisReturn_ResultType_Running))
                        {
                            //机械手抓电池到Put位
                            SetSysStatus(p => p.PLC_Output_IsReady_TrayE = false);
                            SetSysStatus(p => p.VisionChecked_TrayE = false);
                            SetSysStatus(p => p.Sys_BatteryIsReady = false);
                            _robotController.ROBOT_CatchAndPutBattery(
                                0,//TODO:机械手运动模式未设置
                                GetThePosition(EnumDefaultPosition.StandyB),
                                GetThePosition(EnumDefaultPosition.TrayE, GetSysStatus(p => p.CurrentReleaseIndex_TrayE)),
                                GetSysConfig(p => p.ZAxiaSafePosition)
                                );
                            //跳到视觉检测
                            threadStep = 3;
                        }
                        else
                        {
                            //TODO:定义防呆功能,待定
                        }
                        break;
                }           
                if (threadStep != GetSysStatus(p => p.Thread_RobotStep))
                {
                    SetSysStatus(p => p.Thread_RobotStep = threadStep);
                }
            }
        }
    }
}
