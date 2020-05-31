using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

using System.Net;
using System.IO;

namespace ATL_MC.SR_1000
{
    public class SR
    {
        private bool bSimulate;
        private Socket tcpClient;

        public SR()
        {
            bSimulate = true;
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpClient.SendTimeout = 200;
            tcpClient.ReceiveTimeout = 500;
        }
        /*
        功      能：    连接扫码枪
        参      数：    扫码枪ip
        返  回  值：    true:成功 false：失败
        */
        public bool ConnectPort(string strServerIP,bool simulate )
        {
            bSimulate = simulate;
            if(bSimulate)
            {
                return true;
            }
            try
            {
                IPAddress ipAddress = IPAddress.Parse(strServerIP);
                EndPoint endPoint = new IPEndPoint(ipAddress, 9004);
                tcpClient.Connect(endPoint);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
       功      能：    断开扫码枪
       参      数：    无
       返  回  值：    无
       */
        public void DisconnectPort()
        {
            if (bSimulate)
            {
                return;
            }
            tcpClient.Close();
            return ;
        }

        /*
       功      能：    开始扫码
       参      数：    无
       返  回  值：    成功：true，失败：false
       */
        public bool StartRead()
        {
            if (bSimulate)
            {
                return true;
            }

            byte[] Data = new byte[4];

            Data[0] = 0X4C;
            Data[1] = 0X4F;
            Data[2] = 0X4E;
            Data[3] = 0X0D;

            int slen = tcpClient.Send(Data);
            if (slen == Data.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
       功      能：    停止扫码
       参      数：    无
       返  回  值：    成功：true，失败：false
       */
        public bool StopRead()
        {
            if (bSimulate)
            {
                return true;
            }

            byte[] Data = new byte[5];

            Data[0] = 0X4C;
            Data[1] = 0X4F;
            Data[2] = 0X46;
            Data[3] = 0X46;
            Data[4] = 0X0D;

            int slen = tcpClient.Send(Data);
            if (slen == Data.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
       功      能：    获取扫到二维码
       参      数：    无
       返  回  值：    二维码
       */
        public int GetBarCode(out string code)
        {
            code = "Simulate";
            if (bSimulate)
            {
                return 0;
            }

            code = "";
            //设置接收超时时长(ms)
            tcpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);
            byte[] buf = new byte[1024];
            try
            {
                int length = tcpClient.Receive(buf);
                if (length > 0)
                {
                    code = Encoding.UTF8.GetString(buf, 0, length);
                }
            }
            catch (IOException)
            {
                return 1;
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 10060)
                    // 超时的时候错误号码是10060
                    return 2;
                else
                    return 3;
            }
            return 0;
        }

        public int Get()
        {
            if (bSimulate)
            {
                return 0;
            }

            //设置接收超时时长(ms)
            tcpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);
            byte[] buf = new byte[1024];
            try
            {
                int a = 2;
                int length;
                string aaa;
                while (a >0)
                {
                    length = tcpClient.Receive(buf);
                    if (length > 0)
                    {
                        aaa = Encoding.UTF8.GetString(buf, 0, length);
                    }
                    a--;
                }
            }
            catch (IOException)
            {
                return 1;
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 10060)
                    // 超时的时候错误号码是10060
                    return 2;
                else
                    return 3;
            }
            return 0;

        }
    }
}
