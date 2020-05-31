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
    //料盘线程
    public partial class MainCtrl
    {
        /// <summary>
        /// 料盘拍照+相片保存+视觉检测
        /// </summary>
        /// <param name="trayType">TrayA,TrayB...</param>
        /// <param name="threadName"></param>
        /// <param name="vision"></param>
        /// <param name="baslerCamera"></param>
        /// <param name="picWidth"></param>
        /// <param name="picHeight"></param>
        /// <returns></returns>
        private void TrayVisionHandle(string trayType, string threadName, Vision.Vision vision, BaslerCamera.BaslerCamera baslerCamera, long picWidth, long picHeight)
        {
            //TODO:料盘的偏差暂无法使用
            double x, y, a = 0.0;
            //TODO:tray相片储存路径
            string trayPictureFilePath = string.Empty;
            byte[] buff = TakePicture(baslerCamera, picWidth, picHeight, threadName);
            if (0 == vision.CheckBatteryStatus(buff, picWidth, picHeight, 0, trayPictureFilePath, out x, out y, out a, GetSimulate<bool>(p => p.Simulate_Vision), true))
            {               
                //保存相片,TODO:相片保存路径，是否需要经过视觉处理后才取得？，如不需要视觉处理后才取得则可异步，后续处理
                Task.Run(() =>
                {
                    mVision.SavePicJPEG(buff, (int)picWidth, (int)picHeight, trayPictureFilePath + ".jpg");
                });
            }
            else
            {
                //TODO:视觉处理只有0,1两个状态?,需要报警具体信息?!
                SYS_IBG_LOG(WARNINGERR, 0, 0, $"{threadName}:{trayType}料盘有异物");
                //TODO:发现异物,需要暂停? 需要告诉PLC 机械手??

            }
        }

        /// <summary>
        /// 料盘线程
        /// </summary>
        public void ThreadTray()
        {
            Stopwatch sw = new Stopwatch();
            while (!bStopThread)
            {
                Thread.Sleep(5);

                //if (m_SysState.GetSysCriticalError())
                //    m_SysState.m_iTrayChangeStep = 0;
                //if (m_SysState.GetStopFlag())
                //    m_SysState.m_iTrayChangeStep = 0;
                //if (!m_SysState.GetSysStart())
                //    continue;
                //if (m_SysState.GetSysPause())
                //continue;
                //if (m_SysState.GetSysError())
                //continue;

                int threadStep = GetSysStatus<int>(p => p.Thread_TrayStep);
                switch (threadStep)
                {
                    case 0:
                        //TODO:缺死循环跳出机制
                        Thread.Sleep(500);
                        break;
                    case 1:
                        if (!GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayA) && GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayA))
                        {
                            Task.Run(() =>
                            {
                                //TODO:10秒未换好盘,需要做特殊处理?
                                int index = 500;
                                while (index >= 0)
                                {
                                    Thread.Sleep(20);
                                    index--;
                                    if (GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayA) && !GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayA))
                                    {
                                        SetLightBox(_lightController_TrayA, "ThreadTray", EnumProcess.TrayA, EnumTrayType.TrayA, 0);
                                        TrayVisionHandle("TrayA", "ThreadTray", mVision, _trayACamera, m_CameraConfig.CameraWidth_TrayA, m_CameraConfig.CameraHeight_TrayA);
                                        SetSysStatus(p => p.VisionChecked_TrayA = true);
                                        break;
                                    }
                                }
                            });
                        }
                        if (!GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayB) && GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayB))
                        {
                            Task.Run(() =>
                            {
                                //TODO:10秒未换好盘,需要做特殊处理?
                                int index = 500;
                                while (index >= 0)
                                {
                                    Thread.Sleep(20);
                                    index--;
                                    if (GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayB) && !GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayB))
                                    {
                                        TrayVisionHandle("TrayB", "ThreadTray", mVision, _trayBCamera, m_CameraConfig.CameraWidth_TrayB, m_CameraConfig.CameraHeight_TrayB);
                                        SetSysStatus(p => p.VisionChecked_TrayB = true);
                                        break;
                                    }
                                }
                            });
                        }

                        if (!GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayC) && GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayC))
                        {
                            Task.Run(() =>
                            {
                                //TODO:10秒未换好盘,需要做特殊处理?
                                int index = 500;
                                while (index >= 0)
                                {
                                    Thread.Sleep(20);
                                    index--;
                                    if (GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayC) && !GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayC))
                                    {
                                        TrayVisionHandle("TrayC", "ThreadTray", mVision, _trayCCamera, m_CameraConfig.CameraWidth_TrayC, m_CameraConfig.CameraHeight_TrayC);
                                        SetSysStatus(p => p.VisionChecked_TrayC = true);
                                        break;
                                    }
                                }
                            });
                        }
                        if (!GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayD) && GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayD))
                        {
                            Task.Run(() =>
                            {
                                //TODO:10秒未换好盘,需要做特殊处理?
                                int index = 500;
                                while (index >= 0)
                                {
                                    Thread.Sleep(20);
                                    index--;
                                    if (GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayD) && !GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayD))
                                    {
                                        TrayVisionHandle("TrayD", "ThreadTray", mVision, _trayDCamera, m_CameraConfig.CameraWidth_TrayD, m_CameraConfig.CameraHeight_TrayD);
                                        SetSysStatus(p => p.VisionChecked_TrayD = true);
                                        break;
                                    }
                                }
                            });
                        }
                        if (!GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayE) && GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayE))
                        {
                            Task.Run(() =>
                            {
                                //TODO:10秒未换好盘,需要做特殊处理?
                                int index = 500;
                                while (index >= 0)
                                {
                                    Thread.Sleep(20);
                                    index--;
                                    if (GetSysStatus<bool>(p => p.PLC_Output_IsReady_TrayE) && !GetSysStatus<bool>(p => p.PLC_Output_Discharging_TrayE))
                                    {
                                        TrayVisionHandle("TrayE", "ThreadTray", mVision, _trayECamera, m_CameraConfig.CameraWidth_TrayE, m_CameraConfig.CameraHeight_TrayE);
                                        SetSysStatus(p => p.VisionChecked_TrayE = true);
                                        break;
                                    }
                                }
                            });
                        }
                        threadStep++;
                        break;
                    case 2:

                        //在料盘线程中运行，需要一个开始信号，
                        //SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:料盘开始视觉检测");
                        //Task.Run(() => {
                        //    //视觉检测,TODO:此处视觉处理是机械到回到待机位才开始,需要优化!!! 移至料盘线程？！
                        //    BaslerCamera.BaslerCamera baslerCamera = null;
                        //    long picWidth, picHeight = 0;
                        //    if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayA)
                        //    {
                        //        baslerCamera = _trayACamera;
                        //        picWidth = m_CameraConfig.CameraWidth_TrayA; //.lTrayA_CameraWidth;
                        //        picHeight = m_CameraConfig.CameraHeight_TrayA;
                        //        //拍照+保存相片+视觉处理    
                        //        TrayVisionHandle("TrayA", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                        //        SetSysStatus(p => p.VisionChecked_TrayA = true);
                        //        SetSysStatus(p => p.Sys_MoveInContinus = true);
                        //        //TODO:写入产量等操作 放到下一步
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayB)
                        //    {
                        //        baslerCamera = _trayBCamera;
                        //        picWidth = m_CameraConfig.CameraWidth_TrayB;
                        //        picHeight = m_CameraConfig.CameraHeight_TrayB;
                        //        TrayVisionHandle("TrayB", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                        //        SetSysStatus(p => p.VisionChecked_TrayB = true);
                        //        //TODO:写入产量等操作 放到下一步
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayC)
                        //    {
                        //        baslerCamera = _trayCCamera;
                        //        picWidth = m_CameraConfig.CameraWidth_TrayC;
                        //        picHeight = m_CameraConfig.CameraHeight_TrayC;
                        //        TrayVisionHandle("TrayC", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                        //        SetSysStatus(p => p.VisionChecked_TrayC = true);
                        //        //TODO:写入产量等操作 放到下一步
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayD)
                        //    {
                        //        baslerCamera = _trayDCamera;
                        //        picWidth = m_CameraConfig.CameraWidth_TrayD;
                        //        picHeight = m_CameraConfig.CameraHeight_TrayD;
                        //        TrayVisionHandle("TrayD", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                        //        SetSysStatus(p => p.VisionChecked_TrayD = true);
                        //        //TODO:写入产量等操作 放到下一步
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayE)
                        //    {
                        //        baslerCamera = _trayECamera;
                        //        picWidth = m_CameraConfig.CameraWidth_TrayE;
                        //        picHeight = m_CameraConfig.CameraHeight_TrayE;
                        //        TrayVisionHandle("TrayE", "ThreadRobot", mVision, baslerCamera, picWidth, picHeight);
                        //        SetSysStatus(p => p.VisionChecked_TrayE = true);
                        //        //TODO:写入产量等操作 放到下一步
                        //    }
                        //    else
                        //    {
                        //        throw new Exception("不存在此种情况");
                        //    }


                        //    #region //换料,出料,计算产量,计算PPM等操作 
                        //    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadRobot:开始写入产量");
                        //    //换料,出料,计算产量,计算PPM等操作  
                        //    var qtyForCrib = GetProductParam(p => p.QTY_FOR_CRIB);//每lot数量
                        //    var qtyForTray = GetProductParam(p => p.QTY_FOR_TARY);//每盘数量
                        //    if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayA)
                        //    {
                        //        var trayAReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayA);
                        //        if (qtyForTray >= 4)
                        //        {
                        //            if (qtyForTray - trayAReleaseIndex == 2)//倒数第二格
                        //            {
                        //                //通知PLC TrayA准备换盘了
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayA), 1);
                        //            }
                        //            else if (qtyForTray - trayAReleaseIndex == 1)//最后一格
                        //            {
                        //                //TODO:换lot和换料盘等相关操作待定
                        //                //通知PLC TrayA换盘
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayA), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayA = false);
                        //            }
                        //            else
                        //            {

                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (qtyForTray - trayAReleaseIndex == 1)//最后一格
                        //            {
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayA), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayA = false);
                        //            }
                        //            else
                        //            {

                        //            }
                        //        }
                        //        SetSysStatus(p => p.CurrentReleaseIndex_TrayA++);
                        //        //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayB)
                        //    {
                        //        var TrayBReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayB);
                        //        if (qtyForTray >= 4)
                        //        {
                        //            if (qtyForTray - TrayBReleaseIndex == 2)//倒数第二格
                        //            {
                        //                //通知PLC TrayB准备换盘了
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayB), 1);
                        //            }
                        //            else if (qtyForTray - TrayBReleaseIndex == 1)//最后一格
                        //            {
                        //                //TODO:换lot和换料盘等相关操作待定
                        //                //通知PLC TrayB换盘
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayB), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayB = false);
                        //            }
                        //            else
                        //            {

                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (qtyForTray - TrayBReleaseIndex == 1)//最后一格
                        //            {
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayB), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayB = false);
                        //            }
                        //            else
                        //            {

                        //            }
                        //        }
                        //        SetSysStatus(p => p.CurrentReleaseIndex_TrayB++);
                        //        //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayC)
                        //    {
                        //        var TrayCReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayC);
                        //        if (qtyForTray >= 4)
                        //        {
                        //            if (qtyForTray - TrayCReleaseIndex == 2)//倒数第二格
                        //            {
                        //                //通知PLC TrayC准备换盘了
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayC), 1);
                        //            }
                        //            else if (qtyForTray - TrayCReleaseIndex == 1)//最后一格
                        //            {
                        //                //TODO:换lot和换料盘等相关操作待定
                        //                //通知PLC TrayC换盘
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayC), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayC = false);
                        //            }
                        //            else
                        //            {
                        //                //TODO:正常情况,暂不处理,留位
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (qtyForTray - TrayCReleaseIndex == 1)//最后一格
                        //            {
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayC), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayC = false);
                        //            }
                        //            else
                        //            {
                        //                //TODO:正常情况,暂不处理,留位
                        //            }
                        //        }
                        //        SetSysStatus(p => p.CurrentReleaseIndex_TrayC++);
                        //        //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理                            
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayD)
                        //    {
                        //        var TrayDReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayD);
                        //        if (qtyForTray >= 4)
                        //        {
                        //            if (qtyForTray - TrayDReleaseIndex == 2)//倒数第二格
                        //            {
                        //                //通知PLC TrayD准备换盘了
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayD), 1);
                        //            }
                        //            else if (qtyForTray - TrayDReleaseIndex == 1)//最后一格
                        //            {
                        //                //TODO:换lot和换料盘等相关操作待定
                        //                //通知PLC TrayD换盘
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayD), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayD = false);
                        //            }
                        //            else
                        //            {
                        //                //TODO:正常情况,暂不处理,留位
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (qtyForTray - TrayDReleaseIndex == 1)//最后一格
                        //            {
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayD), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayD = false);
                        //            }
                        //            else
                        //            {
                        //                //TODO:正常情况,暂不处理,留位
                        //            }
                        //        }
                        //        SetSysStatus(p => p.CurrentReleaseIndex_TrayD++);
                        //        //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                        //    }
                        //    else if (GetSysStatus(p => p.EnumBisReturn_ResultType_Running) == EnumBisReturn_ResultType.TrayE)
                        //    {
                        //        var TrayEReleaseIndex = GetSysStatus(p => p.CurrentReleaseIndex_TrayE);
                        //        if (qtyForTray >= 4)
                        //        {
                        //            if (qtyForTray - TrayEReleaseIndex == 2)//倒数第二格
                        //            {
                        //                //通知PLC TrayE准备换盘了
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_ReadyForChange_TrayE), 1);
                        //            }
                        //            else if (qtyForTray - TrayEReleaseIndex == 1)//最后一格
                        //            {
                        //                //TODO:换lot和换料盘等相关操作待定
                        //                //通知PLC TrayE换盘
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayE), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayE = false);
                        //            }
                        //            else
                        //            {
                        //                //TODO:正常情况,暂不处理,留位
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (qtyForTray - TrayEReleaseIndex == 1)//最后一格
                        //            {
                        //                _netPLC.WriteUshort(GetPLCAddr(p => p.PLC_Input_Addr_Reload_TrayE), 1);
                        //                SetSysStatus(p => p.VisionChecked_TrayE = false);
                        //            }
                        //            else
                        //            {
                        //                //TODO:正常情况,暂不处理,留位
                        //            }
                        //        }
                        //        SetSysStatus(p => p.CurrentReleaseIndex_TrayE++);
                        //        //TODO:写入产量,计算PPM,计算Lot数量,当前数量,暂不处理
                        //    }
                        //    else
                        //    {
                        //        throw new Exception("不存在此种情况");
                        //    }
                        //    #endregion

                        //});

                        threadStep = 1;
                        break;
                }
                Thread.Sleep(5);
                if (threadStep != GetSysStatus(p => p.Thread_TrayStep))
                {
                    SetSysStatus(p => p.Thread_TrayStep = threadStep);
                }                
            }
        }
    }
}
