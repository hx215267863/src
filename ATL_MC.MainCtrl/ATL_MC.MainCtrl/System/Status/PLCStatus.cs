using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{

    /// <summary>
    /// PLC_Input_为上位机写入寄存器的变量,需要寄存器地址
    /// PLC_Ouput_为PLC写入寄存器的变量,读出来是一个bool值
    /// </summary>
    public class PLCStatus: BaseStatus
    {
        #region 上位位->寄存器
        /// <summary>
        /// trayA出料盘
        /// </summary>
        public string PLC_Input_Addr_Discharge_TrayA { get; set; } = "15698";
        public string PLC_Input_Addr_Discharge_TrayB { get; set; }
        public string PLC_Input_Addr_Discharge_TrayC { get; set; }
        public string PLC_Input_Addr_Discharge_TrayD { get; set; }
        public string PLC_Input_Addr_Discharge_TrayE { get; set; }
        /// <summary>
        /// trayA换料
        /// </summary>
        public string PLC_Input_Addr_Reload_TrayA { get; set; }
        public string PLC_Input_Addr_Reload_TrayB { get; set; }
        public string PLC_Input_Addr_Reload_TrayC { get; set; }
        public string PLC_Input_Addr_Reload_TrayD { get; set; }
        public string PLC_Input_Addr_Reload_TrayE { get; set; }
        /// <summary>
        /// trayA清盘
        /// </summary>
        public string PLC_Input_Addr_Clear_TrayA { get; set; }
        public string PLC_Input_Addr_Clear_TrayB { get; set; }
        public string PLC_Input_Addr_Clear_TrayC { get; set; }
        public string PLC_Input_Addr_Clear_TrayD { get; set; }
        public string PLC_Input_Addr_Clear_TrayE { get; set; }

        /// <summary>
        /// 复位
        /// </summary>
        public string PLC_Input_Addr_Reset { get; set; }
        public string PLC_Input_Addr_Start { get; set; }
        public string PLC_Input_Addr_Pause { get; set; }
        /// <summary>
        /// 入料位取料完成
        /// </summary>
        public string PLC_Input_Addr_MoveInCatchFinish { get; set; }
       
        /// <summary>
        /// 机械手准备完成
        /// </summary>
        public string PLC_Input_Addr_RobotIsReady { get; set; }
        /// <summary>
        /// NG盒已满,IO接在机械手,满时告诉PLC
        /// </summary>
        public string PLC_Input_Addr_NGBoxIsFull { get; set; }
        /// <summary>
        /// NG盒不在位,IO接在机械手,不在位时告诉PLC
        /// </summary>
        public string PLC_Input_Addr_NGBoxNotInplace { get; set; }
        /// <summary>
        /// 机械手放电芯到料盘完成
        /// </summary>
        public string PLC_Input_Addr_PutFinish_TrayA { get; set; }
        public string PLC_Input_Addr_PutFinish_TrayB { get; set; }
        public string PLC_Input_Addr_PutFinish_TrayC { get; set; }
        public string PLC_Input_Addr_PutFinish_TrayD { get; set; }
        public string PLC_Input_Addr_PutFinish_TrayE { get; set; }
        /// <summary>
        /// 拉带电池偏差过大报警
        /// </summary>
        public string PLC_Input_Addr_BatteryDeviationTooLarge { get; set; }


        /// <summary>
        /// 准备换盘,当前放料格为倒数第二格
        /// </summary>
        public string PLC_Input_Addr_ReadyForChange_TrayA { get; set; }
        public string PLC_Input_Addr_ReadyForChange_TrayB { get; set; }
        public string PLC_Input_Addr_ReadyForChange_TrayC { get; set; }
        public string PLC_Input_Addr_ReadyForChange_TrayD { get; set; }
        public string PLC_Input_Addr_ReadyForChange_TrayE { get; set; }



        #endregion






    }
}
