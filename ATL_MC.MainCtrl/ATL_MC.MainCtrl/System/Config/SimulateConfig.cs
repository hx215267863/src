using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{
    public class SimulateConfig : BaseStatus
    {
        public bool Simulate_CameraSN_TrayA { get; set; } = true;
        public bool Simulate_CameraSN_TrayB { get; set; } = true;
        public bool Simulate_CameraSN_TrayC { get; set; } = true;
        public bool Simulate_CameraSN_TrayD { get; set; } = true;
        public bool Simulate_CameraSN_TrayE { get; set; } = true;
        public bool Simulate_MoveInCamera { get; set; } = true;
        public bool Simulate_DansoRobot { get; set; } = true;
        public bool Simulate_Lightcontroller_TrayA { get; set; } = true;
        public bool Simulate_Lightcontroller_TrayB { get; set; } = true;
        public bool Simulate_Lightcontroller_TrayC { get; set; } = true;
        public bool Simulate_Lightcontroller_TrayD { get; set; } = true;
        public bool Simulate_Lightcontroller_TrayE { get; set; } = true;
        /// <summary>
        /// 拉带
        /// </summary>
        public bool Simulate_Lightcontroller_MoveIn { get; set; } = true;
        public bool Simulate_PLC { get; set; } = true;
        public bool Simulate_BarcodeScanner { get; set; } = true;
        public bool Simulate_gBis { get; set; } = true;


        public bool Simulate_Vision { get; set; } = true;


    }
}
