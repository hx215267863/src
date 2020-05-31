using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.BIS
{
    public class BIS
    {
        private bool bSimulate = true;
        private static Mutex m_Controller = new Mutex();

        private Byte[] ReadBytes = new Byte[1024];
        private SerialPort m_SerialPort = new SerialPort();

        private bool request = false;
        private bool reDel = false;

        private int ret = -1;
        private int retDel = -1;
        private string code = "";

        public FileStream m_LogFile = null;
        public StreamWriter m_LogFileGB2312 = null;
        string m_szAppFilePath = null;
        string m_strDebugLogDirectory = null;
        string m_szAppStartTime = null;

        private bool paaked = false;
        private bool packing = false;

        private ulong packCount = 0;

        private ulong lotcount = 100;

        private bool Error = false;

        public void  SetLot(ulong va)
        {
            lotcount = va;
        }

        public BIS()
        {
            m_szAppFilePath = GetAppPath();

            m_strDebugLogDirectory = m_szAppFilePath + "\\RunTimeLog";
            System.IO.Directory.CreateDirectory(m_strDebugLogDirectory);

            m_szAppStartTime = string.Format("\\{0}_{1:D2}_{2:D2}_BIS.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            m_strDebugLogDirectory += m_szAppStartTime;

            m_LogFile = new FileStream(m_strDebugLogDirectory, FileMode.Append);

            m_LogFileGB2312 = new StreamWriter(m_LogFile, Encoding.GetEncoding("GB2312")); //指定GB2312编码 
        }

        private string GetAppPath()
        {
            string Temp = System.Environment.CurrentDirectory;
            return Temp;
        }

        public int OpenCom(string strCom, bool simulate)
        {
            bSimulate = simulate;
            if (bSimulate)
            {
                return 0;
            }

            m_SerialPort.PortName = strCom;
            m_SerialPort.BaudRate = 9600;
            m_SerialPort.StopBits = StopBits.One;
            m_SerialPort.DataBits = 8;
            m_SerialPort.Parity = Parity.Odd;
            m_SerialPort.ReadTimeout = 1000;
            m_SerialPort.WriteTimeout = 1000;
            m_SerialPort.RtsEnable = true;

            m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SerialPort_DataReceived);

            try
            {
                m_SerialPort.Open();
            }
            catch
            {
                return 1;
            }

            return 0;
        }

        private void m_SerialPort_DataReceived(object sender, EventArgs e)
        {
            Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
            int length = m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);

            code = Encoding.UTF8.GetString(ReadBytes, 0, length);

            write(code);

            if (reDel)
            {
                if (code.IndexOf("DEL.OK") > 0)
                {
                    reDel = false;
                    retDel = 0;
                    return;
                }
                else if (code.IndexOf("DEL.NG") > 0)
                {
                    reDel = false;
                    retDel = 1;
                    return;
                }
                else if (code.IndexOf("NO.EXISTS") > 0)
                {
                    reDel = false;
                    retDel = 0;
                    return;
                }
            }

            if (request)
            {
                if (code.IndexOf("OK") > 0)
                {
                    request = false;
                    ret = 0;
                    packCount++;
                    return;
                }
                else if (code.IndexOf("NG") > 0)
                {
                    request = false;
                    ret = 1;
                    return;
                }
            }

            if (code.IndexOf("PACK UP") > 0)
            {
            }

            if (code.IndexOf("PACKED") > 0)
            {
                paaked = true;
                //packing = false;
            }
           
            if (code.IndexOf("PACKING") > 0)
            {
                if (packCount % lotcount != 0)
                {
                    Error = true;
                }
                write("开始打包");
                paaked = false;
                packing = true;
            }   
        }

        public bool GetRrror()
        {
            return Error;
        }

        public void write(string msg)
        {

            string writemsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");

            writemsg += ":" + msg;

            m_LogFileGB2312.Write(writemsg);
            m_LogFileGB2312.Flush();

            m_LogFileGB2312.Write("\r\n");
            m_LogFileGB2312.Flush();
        }

        public int WaitPacking()
        {
            Stopwatch bb = new Stopwatch();

            bb.Start();
            while (packing != true)
            {
                Thread.Sleep(1);

                if (bb.ElapsedMilliseconds > 15000)
                { 
                    return -1;
                }
            }

            packing = false;
            return 0;
        }

        public int Del(ulong index, string bar)
        {
            if (bSimulate)
            {
                return 0;
            }

			//Edit by panwu 2019-11-12
            //string aaa = index.ToString() + ",DEL" + bar + "\n";
			string aaa = index.ToString() + ","+ bar+",DEL\n";

            reDel = true;
            retDel = -1;
            code = "";
            m_SerialPort.Write(aaa);

            write(aaa);

            Stopwatch sa = new Stopwatch();

            sa.Start();
            while (reDel == true)
            {
                Thread.Sleep(1);

                if (sa.ElapsedMilliseconds > 15000)
                {
                    reDel = false;
                    return -1;
                }
            }

            return retDel;
        }

        public int Check(ulong index ,string bar)
        {
            if (bSimulate)
            {
                return 0;
            }
            
            string aaa = index.ToString() + "," + bar + "\n";

            request = true;
            ret = -1;
            code = "";
            m_SerialPort.Write(aaa);

            write(aaa);

            Stopwatch sa = new Stopwatch();

            sa.Start();
            while(request == true)
            {
                Thread.Sleep(1);

                if(sa.ElapsedMilliseconds > 15000)
                {
                    request = false;
                    return -1;
                }
            }

            return ret;
        }

        public void Close()
        {
            if (bSimulate)
            {
                return;
            }

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

            m_SerialPort.Close();
        }
    }
}
