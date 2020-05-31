using ATL_MC.EpsonScaraRobotController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{
    /// <summary>
    /// 系统负载的配置,对应d:\\Config.cs
    /// </summary>
    public class SystemConfig: BaseStatus
    {
        /// <summary>
        /// 拉带相机SN
        /// </summary>
        public string CameraSN_MoveIn { get; set; }
        /// <summary>
        /// trayA盘相机SN
        /// </summary>
        public string CameraSN_TrayA { get; set; }
        /// <summary>
        /// trayB盘相机SN
        /// </summary>
        public string CameraSN_TrayB { get; set; }
        /// <summary>
        /// trayC盘相机SN
        /// </summary>
        public string CameraSN_TrayC { get; set; }
        /// <summary>
        /// trayD盘相机SN
        /// </summary>
        public string CameraSN_TrayD { get; set; }
        /// <summary>
        /// trayE盘相机SN
        /// </summary>
        public string CameraSN_TrayE { get; set; }

        /// <summary>
        /// 机械手IP,默认192.168.0.1
        /// </summary>
        public string RobotIP { get; set; } = "192.168.0.1";
        /// <summary>
        /// 机械手端口
        /// </summary>
        public int RobotPort { get; set; } = 49152;

        /// <summary>
        /// 扫码枪IP,默认192.168.100.100
        /// </summary>
        public string BarcodeScannerIP { get; set; }


        /// <summary>
        /// 光源控制器1 的虚拟COM口
        /// </summary>
        public string LightControllerCOM_TrayA { get; set; }
        /// <summary>
        /// 光源控制器2 的虚拟COM口
        /// </summary>
        public string LightControllerCOM_TrayB { get; set; }
        /// <summary>
        /// 光源控制器3 的虚拟COM口
        /// </summary>
        public string LightControllerCOM_TrayC { get; set; }
        /// <summary>
        /// 光源控制器4 的虚拟COM口
        /// </summary>
        public string LightControllerCOM_TrayD { get; set; }
        /// <summary>
        /// 光源控制器5 的虚拟COM口
        /// </summary>
        public string LightControllerCOM_TrayE { get; set; }
        /// <summary>
        /// 光源控制器6_拉带  的虚拟COM口
        /// </summary>
        public string LightControllerCOM_MoveIn { get; set; }
        /// <summary>
        /// PLC改用网口,该属性弃用
        /// </summary>
        public string KeyencePlcCOM { get; set; }
        /// <summary>
        /// PLC IP地址
        /// </summary>
        public string KeyencePlcIP { get; set; }
        /// <summary>
        /// Bis Com口,默认COM12
        /// </summary>
        public string BisCOM { get; set; } = "COM12";

        /// <summary>
        /// 机械手默认安全高度
        /// </summary>
        public double ZAxiaSafePosition { get; set; }

        /// <summary>
        /// 默认的待机位A
        /// </summary>
        public SixAxisPose RobotPose_StandbyA { get; set; } = new SixAxisPose();

        /// <summary>
        /// 默认的待机位B
        /// </summary>
        public SixAxisPose RobotPose_StandbyB { get; set; } = new SixAxisPose();
        /// <summary>
        /// 默认的NG位
        /// </summary>
        public SixAxisPose RobotPose_NG { get; set; } = new SixAxisPose();
        /// <summary>
        /// 默认的抓取位
        /// </summary>
        public SixAxisPose RobotPose_Catch { get; set; } = new SixAxisPose();



        /// <summary>
        /// 机械手动作最大等待时间,默认30000ms
        /// </summary>
        public long RobotMaxWaitingTime { get; set; }
        /// <summary>
        /// 吸盘放盘,机械手破真空延时时间,默认100ms
        /// </summary>
        public long RobotVaccumBreakDelay { get; set; }
        /// <summary>
        /// 机械手与入料口相机坐标系角度差
        /// </summary>
        public double DeltaAngleBetweenRobotAndCamera { get; set; }


    }
}
