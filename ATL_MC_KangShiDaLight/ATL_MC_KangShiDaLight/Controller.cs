using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;                    //串口
using System.Threading;

using System.Diagnostics;

namespace ATL_MC.KangShiDaLightController
{
    public class KangShiDaLightController
    {
        private bool bSimulate = true;
        private static Mutex m_Controller = new Mutex();
        private int iOpenResult = 1;
        private bool bRead = false;

        private Byte[] ReadBytes = new Byte[1024];

        private SerialPort m_SerialPort = new SerialPort();

        private int[] lightness = new int[4];

        public int InitLightController(string strCom,bool simulate)
        {
            bSimulate = simulate;
            if(bSimulate)
            {
                return 0;
            }

            m_SerialPort.PortName = strCom;
            m_SerialPort.BaudRate = 19200;
            m_SerialPort.StopBits = StopBits.One;
            m_SerialPort.DataBits = 8;
            m_SerialPort.Parity = Parity.None;
            m_SerialPort.ReadTimeout = 500;
            m_SerialPort.RtsEnable = true;

            //m_SerialPort.RtsEnable = false;
            //m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SerialPort_DataReceived);

            try
            {
                m_SerialPort.Open();
                iOpenResult = 0;
            }
            catch
            {
                iOpenResult = 1;
            }
            return 0;
        }

        private void m_SerialPort_DataReceived(object sender,EventArgs e)
        {
            Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
            m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);
            if (bRead)
            {
                bRead = false;
            }
        }

        public int SetLight(int iPortNum, int iValue)
        {
            if (bSimulate)
            {
                return 0;
            }

            if (0 != iOpenResult)
            {
                return 1;
            }

            if ((iPortNum < 1) || (iPortNum > 4))
            {
                return -1;
            }
            if ((iValue < 0) || (iValue > 255))
            {
                return -1;
            }

            Byte[] WriteBytes = new Byte[8];

            WriteBytes[0] = 0x53;
            WriteBytes[1] = (Byte)iPortNum;
            WriteBytes[1] += 0x40;
            WriteBytes[2] = 0x30;
            WriteBytes[3] = (Byte)(iValue / 100);
            WriteBytes[3] += 0x30;
            WriteBytes[4] = (Byte)(iValue % 100 / 10);
            WriteBytes[4] += 0x30;
            WriteBytes[5] = (Byte)(iValue % 10);
            WriteBytes[5] += 0x30;
            WriteBytes[6] = 0x23;

            m_Controller.WaitOne();
            m_SerialPort.Write(WriteBytes, 0, WriteBytes.Length);
            Thread.Sleep(20);

            Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
            m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);

            /*
            bRead = true;
            Stopwatch stmer = new Stopwatch();
            stmer.Start();
            while ( bRead == true )
            {
                stmer.Stop();
                if (stmer.ElapsedMilliseconds > 1000)
                {
                    m_Controller.ReleaseMutex();
                    return -2;
                }
            }
            */
            m_Controller.ReleaseMutex();

            if (ReadBytes[0] != WriteBytes[1])
            {
                return 1;
            }
            else
            {
                lightness[iPortNum - 1] = iValue;
                return 0;
            }
        }

        public int SetLightBox(int iValue1, int iValue2)
        {
            if (bSimulate)
            {
                return 0;
            }

            if (0 != iOpenResult)
                return 1;

            Byte[] WriteBytes = new Byte[14];
            WriteBytes[0] = 0x53;
            WriteBytes[1] = 0x41;
            WriteBytes[2] = 0x30;
            WriteBytes[3] = (Byte)(iValue1 / 100);
            WriteBytes[3] += 0x30;
            WriteBytes[4] = (Byte)(iValue1 % 100 / 10);
            WriteBytes[4] += 0x30;
            WriteBytes[5] = (Byte)(iValue1 % 10);
            WriteBytes[5] += 0x30;
            WriteBytes[6] = 0x23;

            WriteBytes[7] = 0x53;
            WriteBytes[8] = 0x42;
            WriteBytes[9] = 0x30;
            WriteBytes[10] = (Byte)(iValue2 / 100);
            WriteBytes[10] += 0x30;
            WriteBytes[11] = (Byte)(iValue2 % 100 / 10);
            WriteBytes[11] += 0x30;
            WriteBytes[12] = (Byte)(iValue2 % 10);
            WriteBytes[12] += 0x30;
            WriteBytes[13] = 0x23;

            m_Controller.WaitOne();
            m_SerialPort.Write(WriteBytes, 0, WriteBytes.Length);


            Thread.Sleep(20);

            Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
            m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);
            /*
            bRead = true;

            Stopwatch stmer = new Stopwatch();
            stmer.Start();
            while (bRead == true)
            {
                stmer.Stop();
                if (stmer.ElapsedMilliseconds > 1000)
                {
                    m_Controller.ReleaseMutex();
                    return -2;
                }
            }
            */
            m_Controller.ReleaseMutex();

            if ((ReadBytes[0] != 0x41) ||
                (ReadBytes[1] != 0x42))
            {
                return 1;
            }
            else
            {
                lightness[0] = iValue1;
                lightness[1] = iValue2;
                lightness[2] = 0;
                lightness[3] = 0;
                return 0;
            }
        }

        public int SetLightBox(int iValue1, int iValue2, int iValue3, int iValue4)
        {
            if (bSimulate)
            {
                return 0;
            }

            if (0 != iOpenResult)
                return 1;

            Byte[] WriteBytes = new Byte[32];
            WriteBytes[0] = 0x53;
            WriteBytes[1] = 0x41;
            WriteBytes[2] = 0x30;
            WriteBytes[3] = (Byte)(iValue1 / 100);
            WriteBytes[3] += 0x30;
            WriteBytes[4] = (Byte)(iValue1 % 100 / 10);
            WriteBytes[4] += 0x30;
            WriteBytes[5] = (Byte)(iValue1 % 10);
            WriteBytes[5] += 0x30;
            WriteBytes[6] = 0x23;

            WriteBytes[7] = 0x53;
            WriteBytes[8] = 0x42;
            WriteBytes[9] = 0x30;
            WriteBytes[10] = (Byte)(iValue2 / 100);
            WriteBytes[10] += 0x30;
            WriteBytes[11] = (Byte)(iValue2 % 100 / 10);
            WriteBytes[11] += 0x30;
            WriteBytes[12] = (Byte)(iValue2 % 10);
            WriteBytes[12] += 0x30;
            WriteBytes[13] = 0x23;

            WriteBytes[14] = 0x53;
            WriteBytes[15] = 0x43;
            WriteBytes[16] = 0x30;
            WriteBytes[17] = (Byte)(iValue3 / 100);
            WriteBytes[17] += 0x30;
            WriteBytes[18] = (Byte)(iValue3 % 100 / 10);
            WriteBytes[18] += 0x30;
            WriteBytes[19] = (Byte)(iValue3 % 10);
            WriteBytes[19] += 0x30;
            WriteBytes[20] = 0x23;

            WriteBytes[21] = 0x53;
            WriteBytes[22] = 0x44;
            WriteBytes[23] = 0x30;
            WriteBytes[24] = (Byte)(iValue4 / 100);
            WriteBytes[24] += 0x30;
            WriteBytes[25] = (Byte)(iValue4 % 100 / 10);
            WriteBytes[25] += 0x30;
            WriteBytes[26] = (Byte)(iValue4 % 10);
            WriteBytes[26] += 0x30;
            WriteBytes[27] = 0x23;

            m_Controller.WaitOne();
            m_SerialPort.Write(WriteBytes, 0, WriteBytes.Length);


            Thread.Sleep(20);

            Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
            m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);
            /*
            bRead = true;

            Stopwatch stmer = new Stopwatch();
            stmer.Start();
            while (bRead == true)
            {
                stmer.Stop();
                if (stmer.ElapsedMilliseconds > 1000)
                {
                    m_Controller.ReleaseMutex();
                    return -2;
                }
            }
            */
            m_Controller.ReleaseMutex();

            if ((ReadBytes[0] != 0x41) ||
                (ReadBytes[1] != 0x42) ||
                (ReadBytes[2] != 0x43) ||
                (ReadBytes[3] != 0x44) )
            { 
                return 1;
            }
            else
            {
                lightness[0] = iValue1;
                lightness[1] = iValue2;
                lightness[2] = iValue3;
                lightness[3] = iValue4;
                return 0;
            }     
        }

        public int Recover(int count)
        {
            if (bSimulate)
            {
                return 0;
            }

            if (0 != iOpenResult)
                return 1;

            if (2 == count)
            {
                Byte[] WriteBytes = new Byte[14];
                WriteBytes[0] = 0x53;
                WriteBytes[1] = 0x41;
                WriteBytes[2] = 0x30;
                WriteBytes[3] = (Byte)(lightness[0] / 100);
                WriteBytes[3] += 0x30;
                WriteBytes[4] = (Byte)(lightness[0] % 100 / 10);
                WriteBytes[4] += 0x30;
                WriteBytes[5] = (Byte)(lightness[0] % 10);
                WriteBytes[5] += 0x30;
                WriteBytes[6] = 0x23;

                WriteBytes[7] = 0x53;
                WriteBytes[8] = 0x42;
                WriteBytes[9] = 0x30;
                WriteBytes[10] = (Byte)(lightness[1] / 100);
                WriteBytes[10] += 0x30;
                WriteBytes[11] = (Byte)(lightness[1] % 100 / 10);
                WriteBytes[11] += 0x30;
                WriteBytes[12] = (Byte)(lightness[1] % 10);
                WriteBytes[12] += 0x30;
                WriteBytes[13] = 0x23;

                m_Controller.WaitOne();
                m_SerialPort.Write(WriteBytes, 0, WriteBytes.Length);


                Thread.Sleep(20);

                Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
                m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);

                m_Controller.ReleaseMutex();

                if ((ReadBytes[0] != 0x41) ||
                    (ReadBytes[1] != 0x42))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                Byte[] WriteBytes = new Byte[32];
                WriteBytes[0] = 0x53;
                WriteBytes[1] = 0x41;
                WriteBytes[2] = 0x30;
                WriteBytes[3] = (Byte)(lightness[0] / 100);
                WriteBytes[3] += 0x30;
                WriteBytes[4] = (Byte)(lightness[0] % 100 / 10);
                WriteBytes[4] += 0x30;
                WriteBytes[5] = (Byte)(lightness[0] % 10);
                WriteBytes[5] += 0x30;
                WriteBytes[6] = 0x23;

                WriteBytes[7] = 0x53;
                WriteBytes[8] = 0x42;
                WriteBytes[9] = 0x30;
                WriteBytes[10] = (Byte)(lightness[1] / 100);
                WriteBytes[10] += 0x30;
                WriteBytes[11] = (Byte)(lightness[1] % 100 / 10);
                WriteBytes[11] += 0x30;
                WriteBytes[12] = (Byte)(lightness[1] % 10);
                WriteBytes[12] += 0x30;
                WriteBytes[13] = 0x23;

                WriteBytes[14] = 0x53;
                WriteBytes[15] = 0x43;
                WriteBytes[16] = 0x30;
                WriteBytes[17] = (Byte)(lightness[2] / 100);
                WriteBytes[17] += 0x30;
                WriteBytes[18] = (Byte)(lightness[2] % 100 / 10);
                WriteBytes[18] += 0x30;
                WriteBytes[19] = (Byte)(lightness[2] % 10);
                WriteBytes[19] += 0x30;
                WriteBytes[20] = 0x23;

                WriteBytes[21] = 0x53;
                WriteBytes[22] = 0x44;
                WriteBytes[23] = 0x30;
                WriteBytes[24] = (Byte)(lightness[3] / 100);
                WriteBytes[24] += 0x30;
                WriteBytes[25] = (Byte)(lightness[3] % 100 / 10);
                WriteBytes[25] += 0x30;
                WriteBytes[26] = (Byte)(lightness[3] % 10);
                WriteBytes[26] += 0x30;
                WriteBytes[27] = 0x23;

                m_Controller.WaitOne();
                m_SerialPort.Write(WriteBytes, 0, WriteBytes.Length);


                Thread.Sleep(20);

                Array.Clear(ReadBytes, 0, ReadBytes.GetLength(0));
                m_SerialPort.Read(ReadBytes, 0, ReadBytes.Length);
    
                m_Controller.ReleaseMutex();

                if ((ReadBytes[0] != 0x41) ||
                    (ReadBytes[1] != 0x42) ||
                    (ReadBytes[2] != 0x43) ||
                    (ReadBytes[3] != 0x44))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int CloseLightController()
        {
            if (bSimulate)
            {
                return 0;
            }

            m_SerialPort.Close();
            iOpenResult = 1;
            return 0;
        }
    }
}
