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
    //需要处理的问题:
    //1.暂停时是否能与机械手通信
    //2.连接以后将机械需要的初始变量由上位机传给机械手,并保存到全局变量中


    /// <summary>
    /// 电装六轴机械手
    /// </summary>
    public class DansoSixAxisRobotController
    {
        bool _simulate;
        public Socket _clientSocket;
        private Stopwatch sw;
        public bool Paused { get; set; }
        private string _ipString;
        private int _port;

        public DansoSixAxisRobotController(string ipString = "192.168.0.1", int port = 49152, bool simulate = false)
        {
            _simulate = simulate;
            _ipString = ipString;
            _port = port;
            Init();
            sw = new Stopwatch();
            Paused = false;
        }
        private void Init()
        {
            if (_simulate) return;            
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.SendTimeout = 200;
            _clientSocket.ReceiveTimeout = 200;
        }
        /// <summary>
        /// 连接机械手
        /// </summary>
        /// <param name="simulate"></param>
        /// <returns>0成功,1失败</returns>
        public int ROBOT_Connect()
        {
            if (_simulate)
            {
                return 0;
            }
            IPAddress ScaraServer = IPAddress.Parse(this._ipString);
            try
            {
                Init();
                _clientSocket.Connect(new IPEndPoint(ScaraServer, _port));
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("Connect()异常" + ex);
                return 1;
            }
        }
        /// <summary>
        /// 断开与机械手的连接
        /// </summary>
        /// <returns></returns>
        public int ROBOT_DisConnect()
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("DisConnect()异常" + ex);
                return 1;
            }
        }
        /// <summary>
        /// 释放机械手各轴
        /// </summary>
        /// <returns></returns>
        public int ROBOT_FreeRobot()
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "FreeRobot," + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "FreeRobot");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("FreeRobot()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 锁定机械手各轴 ,SrvUnLock True, 1
        /// </summary>
        /// <returns></returns>
        public int ROBOT_LockRobot()
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "LockRobot," + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "LockRobot");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("LockRobot()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 获取机械手所有状态数据和IO
        /// </summary>
        /// <returns> 返回格式:0位: </returns>
        public string ROBOT_GetRobotStatus()
        {
            if (_simulate)
            {
                return string.Empty;
            }
            try
            {
                string sendMessage = "GetRobotStatus," + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "GetRobotStatus");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return null;
                string rec = Encoding.UTF8.GetString(ReceiveData).Trim('\0', ' ');
                //TODO:解析状态参数
                return rec;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("GetRobotStatus()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 获取机械手是否暂停
        /// </summary>
        /// <returns></returns>
        public bool GetPaused()
        {
            return Paused;
        }
        
        /// <summary>
        /// 获取执行时间
        /// </summary>
        /// <returns></returns>
        public long GetExecuteTime()
        {
            if (_simulate)
            {
                return 1000;
            }

            if (Paused)
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
    
        /// <summary>
        /// 获取机械手位置
        /// </summary>
        /// <returns></returns>
        public SixAxisPose ROBOT_GetRobotPosition()
        {
            if (_simulate)
            {
                return new SixAxisPose ();
            }
            try
            {
                string sendMessage = "GetRobotPosition," + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "GetRobotPosition");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return null;
                string recData = Encoding.UTF8.GetString(ReceiveData).Trim('\0', ' ');
                //TODO:解析数据返回
                SixAxisPose sixAxisPose = new SixAxisPose();
                //TODO
                var data = recData.Split(',');
                sixAxisPose.xAxis = Convert.ToDouble(data[0]);
                sixAxisPose.yAxis = Convert.ToDouble(data[1]);
                sixAxisPose.zAxis = Convert.ToDouble(data[2]);
                sixAxisPose.rxAxis = Convert.ToDouble(data[3]);
                sixAxisPose.ryAxis = Convert.ToDouble(data[4]);
                sixAxisPose.rzAxis = Convert.ToDouble(data[0]);
                sixAxisPose.fig = data[6];
                return sixAxisPose;
            }
            catch (Exception ex)
            {
                //TODO
                Console.WriteLine("GetRobotPosition()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 将特殊位置变量存到机械手
        /// </summary>
        /// <param name="poseType">
        /// 1=>待机位1;
        /// 2=>待机位2;
        /// 3=>NG位;
        /// 4=>默认Z轴安全位
        /// 5=>避让位1
        /// 6=>避让位2
        /// 等等
        /// </param>
        /// <param name="sixAxisPose">位姿</param>
        /// <returns></returns>
        public int ROBOT_SetPositionParam(int poseType, SixAxisPose sixAxisPose, double zAxisSafePosition)
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = string.Format("SetPositionParam,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                              poseType,             //0
                              sixAxisPose.xAxis,    //1
                              sixAxisPose.yAxis,    //2
                              sixAxisPose.zAxis,    //3
                              sixAxisPose.rxAxis,   //4
                              sixAxisPose.ryAxis,   //5
                              sixAxisPose.rzAxis,   //6
                              sixAxisPose.fig,      //7
                              sixAxisPose.remark,   //8      
                              zAxisSafePosition,    //9
                              ",\r\n");             //10
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "SetPositionParam");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("SetPositionParam()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 控制机械手输入输出IO
        /// </summary>
        /// <returns></returns>
        public int ROBOT_SetIO(int ioBit, int ioValue)
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "SetIO," + ioBit + "," + ioValue + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "SetIO");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("SetIO()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 暂停机械手所有动作
        /// </summary>
        /// <returns></returns>
        public int ROBOT_Pause()
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "Pause" + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "Pause");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("Pause()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 使机械手继续动作
        /// </summary>
        /// <returns></returns>
        public int ROBOT_Continue()
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "Continue" + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "Continue");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("Continue()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        ///  移动到待机位
        /// </summary>
        /// <param name="position">待机位1,待机位2,默认值0为由机械手自动控制</param>
        /// <returns></returns>
        public int ROBOT_MoveInToStandbyPosition(int position = 0)
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "MoveInToStandbyPosition," + position + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "MoveInToStandbyPosition");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("MoveInToStandbyPosition()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 将电池放到NG盒
        /// </summary>      
        /// <returns></returns>
        public int ROBOT_MoveInToNGPosition()
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = "MoveInToNGPosition" + ",\r\n";
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "MoveInToNGPosition");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("MoveInToNGPosition()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 取放电池线路,分组收料机专用
        /// </summary>      
        /// <param name="model">
        /// 取电池运动模式:
        /// 1.正常流程:待机位->抓取位->抓电池->摆放位->放下电池->待机位
        /// 2.取电池遇到换料盘的情况=>取电池到待机位等待
        /// 3.避让位的情况暂不考虑
        /// </param>
        /// <param name="zAxisSafePosition">为0时使用默认安全位</param>
        /// <returns></returns>
        public int ROBOT_CatchAndPutBattery(int model, SixAxisPose sourcePosition, SixAxisPose targetPosition, double zAxisSafePosition = 0)
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = string.Format("CatchAndPutBattery,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
                              model,                   //0
                              sourcePosition.xAxis,    //1
                              sourcePosition.yAxis,    //2
                              sourcePosition.zAxis,    //3
                              sourcePosition.rxAxis,   //4
                              sourcePosition.ryAxis,   //5
                              sourcePosition.rzAxis,   //6
                              sourcePosition.fig,      //7
                              sourcePosition.remark,   //8      
                              targetPosition.xAxis,    //9
                              targetPosition.yAxis,    //10
                              targetPosition.zAxis,    //11
                              targetPosition.rxAxis,   //12
                              targetPosition.ryAxis,   //13
                              targetPosition.rzAxis,   //14
                              targetPosition.fig,      //15
                              targetPosition.remark,   //16
                              zAxisSafePosition,       //17
                              ",\r\n");                //18    
                byte[] ReceiveData = new byte[1024];
                var mut = new Mutex(false, "CatchAndPutBattery");
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("CatchAndPutBattery()异常" + ex);
                throw ex;
            }
        }
        /// <summary>
        /// 移动到某位置
        /// </summary>
        /// <param name="sixAxisPose"></param>
        /// <param name="moveType">插补模式:1.P;2.L;3.C;4.S; </param>
        /// <param name="zAxisSafePosition">z轴安全位,不为0时,此变量代替sixAxisPose中的zAxis的值</param>
        /// <returns></returns>
        public int ROBOT_MoveToPos(int moveType, SixAxisPose sixAxisPose, double zAxisSafePosition = 0)
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = string.Format("MoveToPos,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                              moveType,            //0
                              sixAxisPose.xAxis,   //1
                              sixAxisPose.yAxis,   //2
                              sixAxisPose.zAxis,   //3
                              sixAxisPose.rxAxis,  //4
                              sixAxisPose.ryAxis,  //5
                              sixAxisPose.rzAxis,  //6
                              sixAxisPose.fig,     //7
                              sixAxisPose.remark,  //8
                              zAxisSafePosition,   //9
                              ",\r\n");            //10              
                byte[] ReceiveData = new byte[1024];
                // var mut = new Mutex(false, "MoveToPos");
                
                 var mut = new Mutex();
               
                mut.WaitOne();
                _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));                
                int receiveLength = _clientSocket.Receive(ReceiveData);
                mut.ReleaseMutex();
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("MoveToPos()异常" + ex);
                throw ex;
            }
        }

        object obj = new object();
        public int ROBOT_MoveToPos1(int moveType, SixAxisPose sixAxisPose, double zAxisSafePosition = 0)
        {
            if (_simulate)
            {
                return 0;
            }
            try
            {
                string sendMessage = string.Format("MoveToPos,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                              moveType,            //0
                              sixAxisPose.xAxis,   //1
                              sixAxisPose.yAxis,   //2
                              sixAxisPose.zAxis,   //3
                              sixAxisPose.rxAxis,  //4
                              sixAxisPose.ryAxis,  //5
                              sixAxisPose.rzAxis,  //6
                              sixAxisPose.fig,     //7
                              sixAxisPose.remark,  //8
                              zAxisSafePosition,   //9
                              ",\r\n");            //10              
                byte[] ReceiveData = new byte[1024];
                int receiveLength;
                lock (obj)
                {
                    _clientSocket.Send(Encoding.UTF8.GetBytes(sendMessage));
                     receiveLength = _clientSocket.Receive(ReceiveData);
                }
                
              
                //TODO:处理异常
                if (0 == receiveLength) return 1;
                return 0;
            }
            catch (Exception ex)
            {
                //TODO:保存日志
                Console.WriteLine("MoveToPos()异常" + ex);
                throw ex;
            }
        }



    }
}
