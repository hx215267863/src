using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl
{
    //IO处理线程，随系统启停 仅处理那些随时都要处理的IO
    public partial class MainCtrl
    {
        /// <summary>
        /// IO处理线程，随系统启停 仅处理那些随时都要处理的IO
        /// </summary>
        public void ThreadIOFunction()
        {
            //while (!bStopThread)
            //{
            //    Thread.Sleep(5);

            //    if (m_SysState.GetSysCriticalError())
            //        continue;

            //    if (!m_SysState.GetSysHome())
            //        continue;

            //    if (m_SysState.GetResetButtonClicked())
            //    {
            //        ResetSystem();
            //        m_SysState.SetResetButtonClicked(false);
            //    }

            //    if (m_SysState.GetSysError())
            //        continue;

            //    if (m_SysState.GetStartButtonClicked())
            //    {
            //        StartSystem();
            //        m_SysState.SetStartButtonClicked(false);
            //    }

            //    if (m_SysState.GetStopButtonClicked())
            //    {
            //        PauseSystem();
            //        m_SysState.SetStopButtonClicked(false);
            //    }
            //}
        }




    }
}
