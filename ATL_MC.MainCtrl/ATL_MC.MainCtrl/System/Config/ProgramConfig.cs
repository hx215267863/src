using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{
    public class ProgramConfig: BaseStatus
    {

        /// <summary>
        ///  不计代价拍摄空盘和满盘的方式,机械手去NG盒附近的避让位置
        /// </summary>
        public bool EmptyAndFullTrayVision { get; set; } = false;
        /// <summary>
        /// 料盒放满后，在机械手过了待机位时拍摄满料照片，除了第一个电池位被挡住一部分，其它三个能全看到
        /// </summary>
        public bool FullTray { get; set; } = false;
        /// <summary>
        /// 空料盒放好后，在搬运臂离开后拍摄空盒照片，除了第一个电池位被挡住一部分，其它三个能全看到
        /// </summary>
        public bool EmptyTray { get; set; } = false;
        /// <summary>
        /// 料盘位置补偿
        /// </summary>
        public bool TrayPosFix { get; set; } = false;
        public bool BIS_Config { get; set; } = true;
        public bool VisionConfig { get; set; } = true;
  
        public bool ANDONFlag { get; set; } = true;



        //以下为已使用属性
        /// <summary>
        /// 是否开启了异物检测,未知从何配置
        /// </summary>
        public bool VisionDirt { get; set; } = true;
    }
}
