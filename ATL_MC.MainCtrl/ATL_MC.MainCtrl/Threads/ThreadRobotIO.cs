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
        /// 将字符串0,1转成bool类型
        /// </summary>
        /// <returns></returns>
        private bool ConvertToBool(string str) {
            if (str=="0")
            {
                return true;
            }
            else if(str=="1")
            {
                return false;
            }
            else
            {
                throw new Exception("只能转换0和1");
            }        
        }

        /// <summary>
        /// IO线程处理方法
        /// </summary>
        public void ThreadRobotIO()
        {
            Stopwatch sw = new Stopwatch();
            while (!bStopThread)
            {                
                int threadStep = GetSysStatus<int>(p => p.Thread_RobotIOStep);
                switch (threadStep)
                {
                    case 0:
                        //TODO:缺死循环跳出机制
                        Thread.Sleep(500);
                        break;
                    case 1:
                        //ROBOT_GetRobotStatus
                        var robotStatus = _robotController.ROBOT_GetRobotStatus();
                        string[] arr = robotStatus.Split(',');
                        //安全门
                        SetSysStatus(p=>p.Robot_Input_SafeDoorIsOpen= ConvertToBool(arr[1]));
                        //NG盒在位
                        SetSysStatus(p=>p.Robot_Input_NGBoxInplace = ConvertToBool(arr[2]));
                        //NG盒已满
                        SetSysStatus(p=>p.Robot_Input_NGBoxIsFull = ConvertToBool(arr[3]));
                        //真空报错
                        SetSysStatus(p=>p.Robot_Input_VacuumError = ConvertToBool(arr[4]));
                        //机械手在拉带区域
                        SetSysStatus(p=>p.Robot_Ouput_RobotInMoveInArea = ConvertToBool(arr[5]));
                        //机械手在料盘1区
                        SetSysStatus(p=>p.Robot_Ouput_RobotInFullTrayArea_1 = ConvertToBool(arr[6]));
                        //机械手在料盘2区
                        SetSysStatus(p=>p.Robot_Ouput_RobotInFullTrayArea_2 = ConvertToBool(arr[7]));
                        //机械手在待机位A
                        SetSysStatus(p=>p.Robot_Inner_RobotInStandbyA = ConvertToBool(arr[8]));
                        //机械手在待机位B
                        SetSysStatus(p=>p.Robot_Inner_RobotInStandbyB = ConvertToBool(arr[9]));
                        //机械手报警
                        SetSysStatus(p=>p.Robot_Output_RobotInAlarm = ConvertToBool(arr[10]));
                        //机械手严重错误
                        SetSysStatus(p=>p.Robot_Output_RobotInFatalAlarm = ConvertToBool(arr[11]));
                        break;
                }
                Thread.Sleep(20);
                if (threadStep != GetSysStatus(p => p.Thread_RobotIOStep))
                {
                    SetSysStatus(p => p.Thread_RobotIOStep = threadStep);
                }
            }
        }
    }
}
