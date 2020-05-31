using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.EpsonScaraRobotController
{
    /// <summary>
    /// 4轴位姿
    /// </summary>
    public class FourAxisPose
    {


    }
    /// <summary>
    /// 6轴位姿
    /// </summary>
    public class SixAxisPose
    {
        public double xAxis { get; set; }
        public double yAxis { get; set; }
        public double zAxis { get; set; }
        public double rxAxis { get; set; }
        public double ryAxis { get; set; }
        public double rzAxis { get; set; }
        public string fig { get; set; }//-1
        /// <summary>
        /// 用途
        /// </summary>
        public string remark { get; set; }
    }
}
