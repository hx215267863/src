using IFactory.UI;
using IFactory.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace IFactory.UI.Diagnostic
{
    /// <summary>
    /// IO.xaml 的交互逻辑
    /// </summary>
    public partial class IO : BasePage, IComponentConnector
    {      
        public IO()
        {
            InitializeComponent();
        }
        bool readIO;
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            readIO = true;
            Task.Run(() =>
            {
                while (readIO)
                {
                    this.Dispatcher.BeginInvoke(new Action(() => { MyIO(); }));
                    Thread.Sleep(100);
                }
            });            
        }


        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            readIO = false;           
        }            

        public void MyIO()
        {          
            //安全门         
            radioButtonRobot_0.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Input_SafeDoorIsOpen);
            //NG盒在位
            radioButtonRobot_1.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Input_NGBoxInplace);
            //NG盒已满
            radioButtonRobot_2.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Input_NGBoxIsFull);
            //真空报错
            radioButtonRobot_3.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Input_VacuumError);
            //机械手在拉带区域
            radioButtonRobot_4.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Ouput_RobotInMoveInArea);
            //机械手在料盘1区
            radioButtonRobot_5.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Ouput_RobotInFullTrayArea_1);
            //机械手在料盘2区
            radioButtonRobot_6.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Ouput_RobotInFullTrayArea_2);
            //机械手在待机位A
            radioButtonRobot_7.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Inner_RobotInStandbyA);
            //机械手在待机位B
            radioButtonRobot_8.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Inner_RobotInStandbyB);
            //机械手报警
            radioButtonRobot_9.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Output_RobotInAlarm);
            //机械手严重错误
            radioButtonRobot_10.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.Robot_Output_RobotInFatalAlarm);

            //PLC

            //TrayA出料中
            radioButtonPLC_0.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Discharging_TrayA);
            //TrayB出料中
            radioButtonPLC_1.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Discharging_TrayB);
            //TrayC出料中
            radioButtonPLC_2.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Discharging_TrayC);
            //TrayD出料中
            radioButtonPLC_3.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Discharging_TrayD);
            //TrayE出料中
            radioButtonPLC_4.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Discharging_TrayE);
            //TrayA换料中
            radioButtonPLC_5.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Reloading_TrayA);
            //TrayB换料中
            radioButtonPLC_6.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Reloading_TrayB);
            //TrayC换料中
            radioButtonPLC_7.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Reloading_TrayC);
            //TrayD换料中
            radioButtonPLC_8.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Reloading_TrayD);
            //TrayE换料中
            radioButtonPLC_9.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Reloading_TrayE);
            //TrayA清料中
            radioButtonPLC_10.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Clearing_TrayA);
            //TrayB清料中
            radioButtonPLC_11.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Clearing_TrayB);
            //TrayC清料中
            radioButtonPLC_12.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Clearing_TrayC);
            //TrayD清料中
            radioButtonPLC_13.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Clearing_TrayD);
            //TrayE清料中
            radioButtonPLC_14.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Clearing_TrayE);
            //TrayA就绪
            radioButtonPLC_15.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_IsReady_TrayA);
            //TrayB就绪
            radioButtonPLC_16.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_IsReady_TrayB);
            //TrayC就绪
            radioButtonPLC_17.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_IsReady_TrayC);
            //TrayD就绪
            radioButtonPLC_18.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_IsReady_TrayD);
            //TrayE就绪
            radioButtonPLC_19.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_IsReady_TrayE);
            //重启中
            radioButtonPLC_20.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Reset);
            //开始
            radioButtonPLC_21.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Start);
            //暂停
            radioButtonPLC_22.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Pause);
            //急停
            radioButtonPLC_23.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_E_Stop);
            //拉带电池到位
            radioButtonPLC_24.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_MoveInCanScan);
            //PLC报警
            radioButtonPLC_25.IsChecked = MainWindow.m_MainCtrl.GetSysStatus(p => p.PLC_Output_Alarm);
            
        }       
        private void button0_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //模拟安全门信号
            MainWindow.m_MainCtrl.SetSysStatus(p => p.Robot_Input_SafeDoorIsOpen = true);
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            //模拟机械手在料盘1区
            MainWindow.m_MainCtrl.SetSysStatus(p => p.Robot_Ouput_RobotInFullTrayArea_1 = true);
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            //模拟机械手报警
            MainWindow.m_MainCtrl.SetSysStatus(p => p.Robot_Output_RobotInAlarm = true);
        }
        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            //模拟开始
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_Start = true);
        }
        private void button4_Click_1(object sender, RoutedEventArgs e)
        {
            //模拟TrayA出料中
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_Discharging_TrayA = true);
        }
        private void button5_Click_1(object sender, RoutedEventArgs e)
        {
            //模拟拉带电池到位
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_MoveInCanScan = true);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl.SetSysStatus(p => p.Robot_Input_SafeDoorIsOpen = false);
            MainWindow.m_MainCtrl.SetSysStatus(p => p.Robot_Ouput_RobotInFullTrayArea_1 = false);
            MainWindow.m_MainCtrl.SetSysStatus(p => p.Robot_Output_RobotInAlarm = false);
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_Start = false);
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_Discharging_TrayA = false);
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_MoveInCanScan = false);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            //模拟拉带电池到位
            MainWindow.m_MainCtrl.SetSysStatus(p => p.PLC_Output_IsReady_TrayA = true);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl.SetSysStatus(p => p.VisionChecked_TrayA=true);

        }
    }
}
