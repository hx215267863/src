using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl
{   
  public partial  class MainCtrl
    {

        public void ThreadClear()
        {
        //    int istep = 1;
        //    int iret = 0;
        //    double x, y, z, u;
        //    bool status = false;
        //    while (!bStopThread)
        //    {
        //        Thread.Sleep(5);

        //        switch (istep)
        //        {
        //            case 1:
        //                iret = mEpsonScaraRobot.GetCurPos(out x, out y, out z, out u);
        //                if (0 != iret)
        //                {
        //                    return;
        //                }

        //                if ((Math.Abs(x - m_SysConfig.dScaraNGPosX) < 5.0) &&
        //                    (Math.Abs(y - m_SysConfig.dScaraNGPosY) < 5.0))
        //                {
        //                    iret = mEpsonScaraRobot.MoveToPos(m_SysConfig.dScaraStandbyPosX,
        //                                                      m_SysConfig.dScaraStandbyPosY,
        //                                                      m_SysConfig.dScaraStandbyPosZ,
        //                                                      m_SysConfig.dScaraStandbyPosU,
        //                                                      m_SysConfig.dScaraSaveZ, 4);

        //                }
        //                else
        //                {
        //                    iret = mEpsonScaraRobot.MoveToPos(m_SysConfig.dScaraStandbyPosX,
        //                                                      m_SysConfig.dScaraStandbyPosY,
        //                                                      m_SysConfig.dScaraStandbyPosZ,
        //                                                      m_SysConfig.dScaraStandbyPosU,
        //                                                      m_SysConfig.dScaraSaveZ, 0);
        //                }

        //                if (0 != iret)
        //                {
        //                    return;
        //                }
        //                else
        //                {
        //                    istep++;
        //                }

        //                break;

        //            case 2:
        //                if (!IsRobotArrivel())
        //                {
        //                    break;
        //                }

        //                mKEYENCE_PLC.Start();

        //                mEpsonScaraRobot.WriteIO(m_SysConfig.Output_ADDR_PLC_ScaraInSaveArea, 1);

        //                if (mKEYENCE_PLC.StartClear())
        //                {
        //                    istep++;
        //                }
        //                else
        //                {
        //                    return;
        //                }
        //                break;

        //            case 3:
        //                if (!mKEYENCE_PLC.GetClearEnd(out status))
        //                {
        //                    return;
        //                }

        //                if (status)
        //                {
        //                    istep++;
        //                }

        //                break;

        //            case 4:
        //                mEpsonScaraRobot.WriteIO(m_SysConfig.Output_ADDR_PLC_ReloadTray, 1);
        //                mKEYENCE_PLC.EndClear();
        //                istep++;
        //                break;

        //            case 5:
        //                if (IsTrayReady())
        //                {
        //                    mEpsonScaraRobot.WriteIO(m_SysConfig.Output_ADDR_PLC_ReloadTray, 0);
        //                    istep++; ;
        //                }
        //                break;

        //            case 6:

        //                m_SysState.m_iScaraReleaseIndex = 0;
        //                //m_SysState.SetClear(false);
        //                return;

        //            default:
        //                break;
        //        }
        //    }
        }





    }
}
