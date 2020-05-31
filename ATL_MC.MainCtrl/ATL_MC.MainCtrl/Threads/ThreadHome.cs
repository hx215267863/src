using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl
{
    // 回零线程
    public partial class MainCtrl
    {
        /// <summary>
        /// 回零线程
        /// </summary>
        public void ThreadHome()
        {
            //int iret = 0;
            //bool status = false;
            //double dCurX, dCurY, dCurZ, dCurU;
            //Stopwatch sw = new Stopwatch();
            //while (!bStopThread)
            //{
            //    Thread.Sleep(5);
            //    switch (m_SysState.m_iHomeStep)
            //    {
            //        case 0:
            //            break;

            //        case 1:
            //            //等待复位请求
            //            if (m_SysState.GetRequestStartHome())
            //            {
            //                m_SysState.m_iHomeStep++;
            //                m_SysState.SetRequestStartHome(false);
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:开始回零");
            //            }
            //            break;

            //        case 2:
            //            iret = mEpsonScaraRobot.GetCurPos(out dCurX, out dCurY, out dCurZ, out dCurU);
            //            if (0 == iret)
            //            {
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:机械手开始抬起到安全高度");
            //                iret = mEpsonScaraRobot.MoveToPos(dCurX, dCurY, m_SysConfig.dScaraStandbyPosZ, m_SysConfig.dScaraStandbyPosU, m_SysConfig.dScaraSaveZ, 99);
            //                m_SysState.m_iHomeStep++;
            //            }
            //            break;
            //        case 3:
            //            if (IsRobotArrivel())
            //            {
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:机械手抬起到安全高度完成");
            //                m_SysState.m_iHomeStep++;
            //            }
            //            else if (mEpsonScaraRobot.GetExecuteTime() > m_SysConfig.lScaraMaxWaitingTime)
            //            {
            //                SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:机械手抬起到安全高度超时");
            //                m_SysState.m_iHomeStep = 0;
            //            }
            //            break;

            //        case 4:
            //            iret = mEpsonScaraRobot.GetCurPos(out dCurX, out dCurY, out dCurZ, out dCurU);
            //            if (0 == iret)
            //            {
            //                if ((Math.Abs(dCurX - m_SysConfig.dScaraStandbyPosX) < 150.0) &&
            //                    (Math.Abs(dCurY - m_SysConfig.dScaraStandbyPosY) < 150.0))
            //                {
            //                    //如果当前位置在待机位附近，可以直接过去
            //                    iret = mEpsonScaraRobot.MoveToPos(m_SysConfig.dScaraStandbyPosX, m_SysConfig.dScaraStandbyPosY,
            //                                                      m_SysConfig.dScaraStandbyPosZ, m_SysConfig.dScaraStandbyPosU, m_SysConfig.dScaraSaveZ, 0);
            //                }
            //                else if ((Math.Abs(dCurX - m_SysConfig.dScaraCatchPosX) < 50.0) &&
            //                         (Math.Abs(dCurY - m_SysConfig.dScaraCatchPosY) < 50.0))
            //                {
            //                    //如果当前位置在取料位附近，可以直接过去
            //                    iret = mEpsonScaraRobot.MoveToPos(m_SysConfig.dScaraStandbyPosX, m_SysConfig.dScaraStandbyPosY,
            //                                                      m_SysConfig.dScaraStandbyPosZ, m_SysConfig.dScaraStandbyPosU, m_SysConfig.dScaraSaveZ, 0);
            //                }
            //                else if ((Math.Abs(dCurX - m_SysConfig.dScaraNGPosX) < 10.0) &&
            //                         (Math.Abs(dCurY - m_SysConfig.dScaraNGPosY) < 10.0))
            //                {
            //                    //机械手在NG料盒位置，用轨迹5走到待机位置
            //                    iret = mEpsonScaraRobot.MoveToPos(m_SysConfig.dScaraStandbyPosX, m_SysConfig.dScaraStandbyPosY,
            //                                                      m_SysConfig.dScaraStandbyPosZ, m_SysConfig.dScaraStandbyPosU, m_SysConfig.dScaraSaveZ, 5);
            //                }
            //                else if (dCurX < m_SysConfig.dScaraStandbyPosX)
            //                {
            //                    //如果当前位置在待机位置左侧，可以直接走到待机位
            //                    iret = mEpsonScaraRobot.MoveToPos(m_SysConfig.dScaraStandbyPosX, m_SysConfig.dScaraStandbyPosY,
            //                                                      m_SysConfig.dScaraStandbyPosZ, m_SysConfig.dScaraStandbyPosU, m_SysConfig.dScaraSaveZ, 0);
            //                }
            //                else
            //                {
            //                    //机械手在不确定的位置
            //                    SYS_IBG_LOG(WARNINGERR, 0, 0, "ThreadHome:机械手在不确定的位置，请推到待机位附近");
            //                    m_SysState.m_iHomeStep = 1;
            //                    break;
            //                }

            //                if (0 == iret)
            //                {
            //                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:机械手开始移动到待机位");
            //                    m_SysState.m_iHomeStep++;
            //                }
            //                else
            //                {
            //                    SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:机械手通讯失败");
            //                    m_SysState.m_iHomeStep = 0;
            //                }
            //            }
            //            else
            //            {
            //                SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:读取机械手位置失败");
            //                m_SysState.m_iHomeStep = 0;
            //            }
            //            break;

            //        case 5:
            //            //等待机械手到位
            //            if (IsRobotArrivel())
            //            {
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:机械手移动到待机位完成");
            //                m_SysState.SetScaraInTrayArea(false);
            //                mEpsonScaraRobot.WriteIO(m_SysConfig.Output_ADDR_PLC_MoveInMove, 0);//允许拉带走，后续这个信号的开启由机械手控制
            //                m_SysState.SetScaraInCameraArea(false);
            //                m_SysState.m_iHomeStep++;
            //            }
            //            else if (mEpsonScaraRobot.GetExecuteTime() > m_SysConfig.lScaraMaxWaitingTime)
            //            {
            //                SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:机械手移动到待机位超时");
            //                m_SysState.m_iHomeStep = 0;
            //            }
            //            break;

            //        case 6:
            //            //通过Scara Robot IO信号告诉PLC机械手在安全位置
            //            iret = mEpsonScaraRobot.WriteIO(m_SysConfig.Output_ADDR_PLC_ScaraInSaveArea, 1);
            //            if (0 == iret)
            //            {
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:机械手告知PLC在安全位置");
            //                m_SysState.m_iHomeStep++;
            //            }
            //            else
            //            {
            //                SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:机械手通讯失败");
            //                m_SysState.m_iHomeStep = 0;
            //            }
            //            break;

            //        case 7:
            //            //写PLC告诉PLC机械手复位完成
            //            if (mKEYENCE_PLC.ScaraHomeFinished())
            //            {
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:串口告知PLC在机械手复位完成");
            //                m_SysState.m_iHomeStep++;
            //            }
            //            else
            //            {
            //                SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:PLC通讯失败");
            //                m_SysState.m_iHomeStep = 0;
            //            }
            //            break;

            //        case 8:
            //            //通知PLC开始回零
            //            if (mKEYENCE_PLC.StartHome())
            //            {
            //                SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "ThreadHome:串口告知PLC开始回零");
            //                m_SysState.m_iHomeStep++;
            //                sw.Stop();
            //                sw.Reset();
            //                sw.Start();
            //            }
            //            else
            //            {
            //                SYS_IBG_LOG(CRITICALERR, 0, 0, "ThreadHome:PLC通讯失败");
            //                m_SysState.m_iHomeStep = 0;
            //            }
            //            break;

            //        case 9:
            //            //等待PLC复位完成信号
            //            mKEYENCE_PLC.GetHomeStatus(out status);
            //            if (status)
            //            {
            //                mEpsonScaraRobot.WriteIO(m_SysConfig.Output_ADDR_PLC_MoveInMove, 1);//允许拉带走，后续这个信号的开启由机械手控制
            //                m_SysState.SetRequestChangeTrayFlag(true);//请求换料盒线程换料盒
            //                m_SysState.SetSysHome(true);
            //                m_SysState.m_iHomeStep = 1;
            //            }
            //            else
            //            {
            //                if (sw.ElapsedMilliseconds > 20000)
            //                {
            //                    m_SysState.SetSysHome(false);
            //                    m_SysState.m_iHomeStep = 1;
            //                }
            //            }
            //            break;

            //        default:
            //            break;
            //    }
            //}
        }
    }
}