using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace ATL_MC.EpsonScaraRobotController
{
    public class EpsonScaraRobotController
    {
        bool bSimulate;
        Socket PcClientSocket;
        private static Mutex SocketMutex;
        private Stopwatch sw;
        private bool Paused;

        public EpsonScaraRobotController()
        {
            bSimulate = true;
            PcClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            PcClientSocket.SendTimeout = 200;
            PcClientSocket.ReceiveTimeout = 200;
            SocketMutex = new Mutex();
            sw = new Stopwatch();
            Paused = false;
        }

        public int InitEpsonScaraRobotController(bool simulate)
        {
            bSimulate = simulate;
            if (bSimulate)
            {
                return 0;
            }
            //设定服务器IP地址  
            IPAddress ScaraServer = IPAddress.Parse("192.168.0.1");
            
            try
            {
                PcClientSocket.Connect(new IPEndPoint(ScaraServer, 3000)); //配置服务器IP与端口  
            }
            catch
            {
                return 1;
            }
           
            return 0;
        }
        public int CloseEpsonScaraRobotController()
        {
            if (bSimulate)
            {
                return 0;
            }
            try
            {
                PcClientSocket.Shutdown(SocketShutdown.Both);
                PcClientSocket.Close();
            }
            catch
            {

            }
            return 0;
        }
        public long GetExecuteTime()
        {
            if (bSimulate)
            {
                return 1000;
            }

            if( Paused )
            {
                sw.Stop();
            }
            else
            {
                sw.Start();
            }

            Thread.Sleep(20);
            long msec = 0;
            //sw.Stop();
            msec = sw.ElapsedMilliseconds;
            //sw.Start();
            return msec;
        }

        public void StopTime()
        {
            //sw.Stop();
        }

        public void StartTime()
        {
            //sw.Start();
        }
        public int CatchAndPutBattery(double dPosX, double dPosY, double dPosZ, double dPosU,
                                      double dPosX1, double dPosY1, double dPosZ1, double dPosU1, double dSaveZ,int iMode)
        {
            if (bSimulate)
            {
                return 0;
            }

            //机械手位置X超过195.0撞前门
            if (dPosX > 205.0)
            { 
                return -1;
            }

            //机械手位置Y超过300.0撞侧门
            if (dPosY > 320.0)
            {
                return -1;
            }

            //机械手Z轴超过-10.0.撞顶灯
            if (dSaveZ>-80.0)
            {
                return -1;
            }

            try
            {
                sw.Stop();
                sw.Reset();
                sw.Start();
                string sendMessage = string.Format("CatchAndPutBattery,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}\r\n", dPosX, dPosY, dPosZ, dPosU, dPosX1, dPosY1, dPosZ1, dPosU1, dSaveZ,iMode);
                int len = 0;

                byte[] ReceiveData = new byte[1024];

                SocketMutex.WaitOne();
                len = PcClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                SocketMutex.ReleaseMutex();

                if (0 == receiveLength)
                    return -1;
            }
            catch
            {
                return 1;
            }
            return 0;
        }

        public int MoveToPos(double dPosX, double dPosY, double dPosZ, double dPosU, double dSaveZ,int iMode)
        {
            if (bSimulate)
            {
                return 0;
            }

            //机械手位置X超过195.0撞前门
            if (dPosX > 205.0)
            {
                return -1;
            }

            //机械手位置Y超过195.0撞侧门
            if (dPosY > 320.0)
            {
                return -1;
            }

            //机械手Z轴超过-10.0.撞顶灯
            if (dSaveZ > -80.0)
            {
                return -1;
            }

            try
            {
                sw.Stop();
                sw.Reset();
                sw.Start();
                string sendMessage = string.Format("MoveToPos,{0},{1},{2},{3},{4},{5}\r\n", dPosX, dPosY, dPosZ, dPosU, dSaveZ, iMode);
                int len = 0;
                byte[] ReceiveData = new byte[1024];
                SocketMutex.WaitOne();
                len = PcClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                SocketMutex.ReleaseMutex();

                if (0 == receiveLength)
                    return -1;
            }
            catch
            {
                return 1;
            }
            return 0;
        }
        public int GetCurPos(out double dPosX,out double dPosY,out double dPosZ,out double dPosU)
        {
            dPosX = 0.0;
            dPosY = 0.0;
            dPosZ = 0.0;
            dPosU = 0.0;

            if (bSimulate)
            {
                return 0;
            }

            try
            {
                string sendMessage = "GetCurPos,0\r\n";
                int len = 0;
                byte[] ReceiveData = new byte[1024];
                SocketMutex.WaitOne();
                len = PcClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                string ret = Encoding.ASCII.GetString(ReceiveData).Replace('\0', ' ').Trim();
                SocketMutex.ReleaseMutex();

                if (0 == receiveLength)
                    return -1;

                int j = 0;
                int row = 0;
                Byte[] temp = new Byte[128];

                temp.Initialize();

                for (int i = 0;i< receiveLength;i++)
                {
                    if ( (ReceiveData[i] == ',') || ( i == receiveLength - 1 ) )
                    {
                        if (0 != j)
                        {
                            char[] cChar = Encoding.ASCII.GetChars(temp);

                            if ( 0 == row)
                            {
                                dPosX = Convert.ToDouble(new string(cChar));
                            }
                            if (1 == row)
                            {
                                dPosY = Convert.ToDouble(new string(cChar));
                            }
                            if (2 == row)
                            {
                                dPosZ = Convert.ToDouble(new string(cChar));
                            }
                            if (3 == row)
                            {
                                dPosU = Convert.ToDouble(new string(cChar));
                            }
                            Array.Clear(temp, 0, temp.GetLength(0));
                            j = 0;
                            row++;
                        }
                    }
                    else if ( ( (ReceiveData[i] >= '0') && ( ReceiveData[i] <= '9' ) ) ||
                              (ReceiveData[i] == '.') ||
                              (ReceiveData[i] == '-')  )
                    {
                        temp[j] = ReceiveData[i];
                        j++;
                    }
                }
            }
            catch
            {
                return 1;
            }

            return 0;
        }

        public int ReadIO(out ulong lIOStatus,out ulong lIOOut)
        {
            lIOStatus = 0;
            lIOOut = 0;
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                string sendMessage = "GetInPut,0\r\n";
                byte[] ReceiveData = new byte[1024];
                SocketMutex.WaitOne();
                PcClientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                SocketMutex.ReleaseMutex();

                if (0 == receiveLength)
                    return -1;

                int j = 0;
                int row = 0;
                Byte[] temp = new Byte[128];
                int io1 = 0, io2 = 0, io3 = 0,bit=0,out1 = 0, out2 = 0;

                temp.Initialize();

                for (int i = 0; i < receiveLength; i++)
                {
                    if ((ReceiveData[i] == ',') || (i == receiveLength - 1))
                    {
                        if (0 != j)
                        {
                            char[] cChar = Encoding.ASCII.GetChars(temp);

                            if (0 == row)
                            {
                                io1 = Convert.ToInt32(new string(cChar));
                            }
                            if (1 == row)
                            {
                                io2 = Convert.ToInt32(new string(cChar));
                            }
                            if (2 == row)
                            {
                                io3 = Convert.ToInt32(new string(cChar));
                            }
                            if (3 == row)
                            {
                                bit = Convert.ToInt32(new string(cChar));
                            }
                            if (4 == row)
                            {
                                out1 = Convert.ToInt32(new string(cChar));
                            }
                            if (5 == row)
                            {
                                out2 = Convert.ToInt32(new string(cChar));
                            }
                            Array.Clear(temp, 0, temp.GetLength(0));
                            j = 0;
                            row++;
                        }
                    }
                    else if ((ReceiveData[i] >= '0') && (ReceiveData[i] <= '9'))
                    {
                        temp[j] = ReceiveData[i];
                        j++;
                    }
                }

                if( 0 != (out1 & (1<<2) ) )
                {
                    sw.Stop();
                    Paused = true;
                }
                else
                {
                    Paused = false;
                }

                lIOStatus = (ulong)io1 + (ulong)io2 * 256 + (ulong)io3 * 256 * 256 + (ulong)bit*256*256*256;
                lIOOut = (ulong)out1 + (ulong)out2 * 256;
            }
            catch
            {
                return 1;
            }
           
            return 0;
        }

        public bool GetPaused()
        {
            return Paused;
        }

        public int WriteIO(int iBitIndex,int iValue)
        {
            if (bSimulate)
            {
                return 0;
            }

            if (iBitIndex < 8 || iBitIndex > 15)
                return -1;

            if ( 0 != iValue )
                iValue = 1;

            try
            {
                string sendMessage = string.Format("SetOutPut,{0},{1}\r\n", iBitIndex, iValue);
                byte[] ReceiveData = new byte[1024];
                SocketMutex.WaitOne();
                PcClientSocket.Send(System.Text.Encoding.Default.GetBytes(sendMessage), sendMessage.Length,0);
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                SocketMutex.ReleaseMutex();

            }
            catch
            {
                return 1;
            }
            return 0;
        }

        public int FreeRobot()
        {
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                string sendMessage = string.Format("FreeRobot,\r\n");
                byte[] ReceiveData = new byte[1024];
                SocketMutex.WaitOne();
                PcClientSocket.Send(System.Text.Encoding.Default.GetBytes(sendMessage), sendMessage.Length, 0);
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                SocketMutex.ReleaseMutex();
            }
            catch
            {
                return 1;
            }
            return 0;
        }

        public int LockRobot()
        {
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                string sendMessage = string.Format("LockRobot,\r\n");
                byte[] ReceiveData = new byte[1024];
                SocketMutex.WaitOne();
                PcClientSocket.Send(System.Text.Encoding.Default.GetBytes(sendMessage), sendMessage.Length, 0);
                int receiveLength = PcClientSocket.Receive(ReceiveData);
                SocketMutex.ReleaseMutex();
            }
            catch
            {
                return 1;
            }
            return 0;
        }
    }
}
