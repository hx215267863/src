using IFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl
{
    public partial class MainCtrl
    {
        /// <summary>
        /// 错误,代码运行错误
        /// </summary>
        private void LogErr(string msg)
        {
            SYS_IBG_LOG(CRITICALERR, 0, 0, msg);
        }
        /// <summary>
        /// 严重错误,负载连接不上等造成无法继续运行的错误
        /// </summary>
        private void LogFatal(string msg)
        {
            SYS_IBG_LOG(CRITICALERR, 0, 0, msg);
        }
        /// <summary>
        /// 运行日志
        /// </summary>
        private void LogDebug(string msg)
        {
            SYS_IBG_LOG(DEBUGL1MSG, 0, 0, msg);
        }
        /// <summary>
        /// 警告,需要人工处理的一些错误
        /// </summary>
        private void LogAlarm(string msg)
        {
            SYS_IBG_LOG(WARNINGERR, 0, 0, msg);
        }













        //以下代码为中间过程,不必理会

        private ushort DEBUGL1MSG = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.DEBUGL1MSG;
        private ushort UGUIWARNMSG = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.UGUIWARNMSG;
        private ushort CRITICALERR = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.CRITICALERR;
        private ushort WARNINGERR = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.WARNINGERR;
        private ushort UGUINOTIFY = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.UGUINOTIFY;
        private ushort UGUILOGMSG = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.UGUILOGMSG;
        private ushort LOGGINGMSG = (ushort)ATL_MC.IBG_LOG.MSGTYPEAD.LOGGINGMSG;
        private void SYS_IBG_LOG(ushort Level, long Moudle, long ret, string msg)
        {
            //UGUIWARNMSG  WARNINGERR  只暂停
            if (UGUIWARNMSG == Level)
            {
                ErrorSystem(Level);
                //mKEYENCE_PLC.PCError();//TODO:PLC报警未处理,告诉PLC上位机程序报警了
                SendMsg(0x4000, Moudle, msg);
                try
                {
                    LocalApi.InsertAlarmExecute(msg);
                }
                catch (Exception err)
                {

                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "写报警信息出错," + err.ToString());
                }
            }
            else if (WARNINGERR == Level)
            {
                ErrorSystem(Level);
                // mKEYENCE_PLC.PCError();//TODO:PLC报警未处理,告诉PLC上位机程序报警了
                SendMsg(0x4001, Moudle, msg);
                try
                {
                    LocalApi.InsertAlarmExecute(msg);
                }
                catch (Exception err)
                {

                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "写报警信息出错," + err.ToString());
                }
            }
            //CRITICALERR 宕机了
            else if (CRITICALERR == Level)
            {
                ErrorSystem(Level);
                //mKEYENCE_PLC.PCCriticalError();//TODO:PLC报警未处理,告诉PLC上位机程序严重报警了
                SendMsg(0x4002, Moudle, msg);
                try
                {
                    LocalApi.InsertAlarmExecute(msg);
                }
                catch (Exception err)
                {
                    SYS_IBG_LOG(DEBUGL1MSG, 0, 0, "写报警信息出错," + err.ToString());
                }
            }
            //通告
            if (UGUINOTIFY == Level)
            {
                SendMsg(0x4001, Moudle, msg);
                PauseSystem();
            }

            mIBG_LOG.LOG(Level, Moudle, ret, msg);
        }


    }
}
