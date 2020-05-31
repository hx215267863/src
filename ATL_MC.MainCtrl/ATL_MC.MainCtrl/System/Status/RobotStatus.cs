using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{
    /// <summary>
    /// 读取Robot中的所有IO、变量、信息
    /// 写入由Robot控制器方法直接使用,不需要通过此类
    /// </summary>
    public class RobotStatus : BaseStatus
    {
        //public int Robot_Input_ { get; set; } //Robot的输入IO
        //public int Robot_Output_ { get; set; }//Robot的输出IO
        //public int Robot_Inner_ { get; set; }//Robot的内部IO
        /// <summary>
        /// 输出真空吸的IO位
        /// </summary>
        public int Robot_Output_Vacuum { get; set; }
        /// <summary>
        /// 输出真空破的IO位
        /// </summary>
        public int Robot_Output_VacuumBreaker { get; set; }

        //Output_ADDR_Uacuum
        //Output_ADDR_UacuumBreaker
        //Output_ADDR_UacuumError

     

    }
}
