using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl
{
    public partial class MainCtrl
    {
        /// <summary>
        /// IO线程处理方法
        /// </summary>
        public void ThreadPLCStatus()
        {
            Stopwatch sw = new Stopwatch();
            while (!bStopThread)
            {
                int threadStep = GetSysStatus<int>(p => p.Thread_PLCStatusStep);
                switch (threadStep)
                {
                    case 0:
                        //TODO:缺死循环跳出机制
                        Thread.Sleep(500);
                        break;
                    case 1:
                        //TODO:PLC的地址还未知,
                        Dictionary<string, bool> dic = _netPLC.DicBoolRead("", 50);
                        //TrayA出料中
                        SetSysStatus(p => p.PLC_Output_Discharging_TrayA = dic[""]);
                        //TrayB出料中
                        SetSysStatus(p => p.PLC_Output_Discharging_TrayB = dic[""]);
                        //TrayC出料中
                        SetSysStatus(p => p.PLC_Output_Discharging_TrayC = dic[""]);
                        //TrayD出料中
                        SetSysStatus(p => p.PLC_Output_Discharging_TrayD = dic[""]);
                        //TrayE出料中
                        SetSysStatus(p => p.PLC_Output_Discharging_TrayE = dic[""]);
                        //TrayA换料中
                        SetSysStatus(p => p.PLC_Output_Reloading_TrayA = dic[""]);
                        //TrayB换料中
                        SetSysStatus(p => p.PLC_Output_Reloading_TrayB = dic[""]);
                        //TrayC换料中
                        SetSysStatus(p => p.PLC_Output_Reloading_TrayC = dic[""]);
                        //TrayD换料中
                        SetSysStatus(p => p.PLC_Output_Reloading_TrayD = dic[""]);
                        //TrayE换料中
                        SetSysStatus(p => p.PLC_Output_Reloading_TrayE = dic[""]);
                        //TrayA清料中
                        SetSysStatus(p => p.PLC_Output_Clearing_TrayA = dic[""]);
                        //TrayB清料中
                        SetSysStatus(p => p.PLC_Output_Clearing_TrayB = dic[""]);
                        //TrayC清料中
                        SetSysStatus(p => p.PLC_Output_Clearing_TrayC = dic[""]);
                        //TrayD清料中
                        SetSysStatus(p => p.PLC_Output_Clearing_TrayD = dic[""]);
                        //TrayE清料中
                        SetSysStatus(p => p.PLC_Output_Clearing_TrayE = dic[""]);
                        //TrayA就绪
                        SetSysStatus(p => p.PLC_Output_IsReady_TrayA = dic[""]);
                        //TrayB就绪
                        SetSysStatus(p => p.PLC_Output_IsReady_TrayB = dic[""]);
                        //TrayC就绪
                        SetSysStatus(p => p.PLC_Output_IsReady_TrayC = dic[""]);
                        //TrayD就绪
                        SetSysStatus(p => p.PLC_Output_IsReady_TrayD = dic[""]);
                        //TrayE就绪
                        SetSysStatus(p => p.PLC_Output_IsReady_TrayE = dic[""]);
                        //重启中
                        SetSysStatus(p => p.PLC_Output_Reset  = dic[""]);
                        //开始
                        SetSysStatus(p => p.PLC_Output_Start  = dic[""]);
                        //暂停
                        SetSysStatus(p => p.PLC_Output_Pause  = dic[""]);
                        //急停
                        SetSysStatus(p => p.PLC_Output_E_Stop = dic[""]);
                        //拉带电池到位
                        SetSysStatus(p => p.PLC_Output_MoveInCanScan = dic[""]);
                        //PLC报警
                        SetSysStatus(p => p.PLC_Output_Alarm = dic[""]);

                        break;
                }
                Thread.Sleep(20);
                if (threadStep != GetSysStatus(p => p.Thread_PLCStatusStep))
                {
                    SetSysStatus(p => p.Thread_PLCStatusStep = threadStep);
                }
            }
        }
    }
}
