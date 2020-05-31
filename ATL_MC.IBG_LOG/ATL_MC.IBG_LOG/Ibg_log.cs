using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ATL_MC.IBG_LOG
{
    public enum MSGTYPEAD
    {
        DEBUGL1MSG = 0,
        UGUIWARNMSG,
        CRITICALERR,
        WARNINGERR,
        UGUINOTIFY,
        UGUILOGMSG,
        LOGGINGMSG
    };

    public class IBG_LOG
    {
        static string m_Msg;
        
        //日志创建的天号，当天号发生变化时，重开一个日志
        int m_iFileCreateDay = 0;
        //日志年、月、日
        string m_szAppStartTime = null;
        string m_szAppFilePath = null;
        string m_strDebugLogDirectory = null;
        public FileStream m_LogFile = null;
        public StreamWriter m_LogFileGB2312 = null;

        private string GetAppPath()
        {
            string Temp = System.Environment.CurrentDirectory;
            return Temp;
        }

        //创建日志目录及日志文档
        public int CreatDebugDirectory()
        {
            m_szAppFilePath = GetAppPath();

            m_strDebugLogDirectory = m_szAppFilePath + "\\RunTimeLog";
            System.IO.Directory.CreateDirectory(m_strDebugLogDirectory);

            m_szAppStartTime = string.Format("\\{0}_{1:D2}_{2:D2}.txt", DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day);
            m_strDebugLogDirectory += m_szAppStartTime;

            m_LogFile = new FileStream(m_strDebugLogDirectory, FileMode.Append);

            m_LogFileGB2312 = new StreamWriter(m_LogFile, Encoding.GetEncoding("GB2312")); //指定GB2312编码 

            //记录日志创建日期
            m_iFileCreateDay = DateTime.Now.Day;

            return 0;
        }
        public void LOG(long level, long nmodule, long nret, string pMsg)
        {

            //return;

            string szLevel;

            //如果日期发生变化,关闭文件，后面会重建
            if (m_iFileCreateDay != DateTime.Now.Day)
            {
                if (m_LogFile != null)
                {
                    m_LogFile.Close();
                    m_LogFile = null;
                }
            }

            string curtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");

            m_Msg = curtime;
            m_Msg += " : ";
            switch (level)
            {
                case (long)MSGTYPEAD.CRITICALERR:
                    szLevel = string.Format("{0}","CRITICALERR");
                    break;
                case (int)MSGTYPEAD.WARNINGERR:
                    szLevel = string.Format("{0}", "UGUIWARNMSG");
                    break;
                case (int)MSGTYPEAD.UGUIWARNMSG:
                    szLevel = string.Format("{0}", "WARNINGERR"); 
                    break;
                case (int)MSGTYPEAD.UGUINOTIFY:
                    szLevel = string.Format("{0}", "UGUINOTIFY");
                    break;
                case (int)MSGTYPEAD.UGUILOGMSG:
                    szLevel = string.Format("{0}", "UGUILOGMSG");
                    break;
                case (int)MSGTYPEAD.LOGGINGMSG:
                    szLevel = string.Format("{0}", "LOGGINGMSG");
                    break;
                case (int)MSGTYPEAD.DEBUGL1MSG:
                    szLevel = string.Format("{0}", "DEBUGL1MSG");
                    break;
                default:
                    szLevel = string.Format("{0}", "UNKNOWN");
                    break;
            }
            m_Msg += szLevel;
            m_Msg += " : ";

            m_Msg += pMsg;

            //如果文件指针为空
            if (m_LogFile == null)
            {
                CreatDebugDirectory();
            }

            if (m_LogFile == null)
            {
            }
            else
            {
                //写入日志
                m_LogFileGB2312.Write(m_Msg);
                m_LogFileGB2312.Flush();

                //换行
                m_LogFileGB2312.Write("\r\n");
                m_LogFileGB2312.Flush();
            }
        }

        public void Close()
        {
            if (m_LogFileGB2312 != null)
            {
                m_LogFileGB2312.Close();
                m_LogFileGB2312 = null;
            }
            if (m_LogFile != null)
            {  
                m_LogFile.Close();
                m_LogFile = null;
            }
        }
    }
}
