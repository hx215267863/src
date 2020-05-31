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

namespace IFactory.UI.Debug
{
    /// <summary>
    /// IO.xaml 的交互逻辑
    /// </summary>
    public partial class IO : BasePage, IComponentConnector
    {
        private System.Timers.Timer ss = new System.Timers.Timer();

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            
            ss.AutoReset = true;
            ss.Enabled = true;
            ss.Interval = 100;
            ss.Elapsed += this.Timer_E;
            ss.Start();
            
        }

        public void MyIO()
        {
            ulong io = MainWindow.m_MainWindow.m_MainCrtl.GetIO();

            radioButton0.IsChecked = (0 != ((int)io & (1 << 0))) ? true : false;
            radioButton1.IsChecked = (0 != ((int)io & (1 << 1))) ? true : false;
            radioButton2.IsChecked = (0 != ((int)io & (1 << 2))) ? true : false;
            radioButton3.IsChecked = (0 != ((int)io & (1 << 3))) ? true : false;
            radioButton4.IsChecked = (0 != ((int)io & (1 << 4))) ? true : false;
            radioButton5.IsChecked = (0 != ((int)io & (1 << 5))) ? true : false;
            radioButton6.IsChecked = (0 != ((int)io & (1 << 6))) ? true : false;
            radioButton7.IsChecked = (0 != ((int)io & (1 << 7))) ? true : false;
            radioButton8.IsChecked = (0 != ((int)io & (1 << 8))) ? true : false;
            radioButton9.IsChecked = (0 != ((int)io & (1 << 9))) ? true : false;
            radioButton10.IsChecked = (0 != ((int)io & (1 << 10))) ? true : false;
            radioButton11.IsChecked = (0 != ((int)io & (1 << 11))) ? true : false;
            radioButton12.IsChecked = (0 != ((int)io & (1 << 12))) ? true : false;
            radioButton13.IsChecked = (0 != ((int)io & (1 << 13))) ? true : false;
            radioButton14.IsChecked = (0 != ((int)io & (1 << 14))) ? true : false;
            radioButton15.IsChecked = (0 != ((int)io & (1 << 15))) ? true : false;
            radioButton16.IsChecked = (0 != ((int)io & (1 << 16))) ? true : false;
            radioButton17.IsChecked = (0 != ((int)io & (1 << 17))) ? true : false;
            radioButton18.IsChecked = (0 != ((int)io & (1 << 18))) ? true : false;
            radioButton19.IsChecked = (0 != ((int)io & (1 << 19))) ? true : false;
            radioButton20.IsChecked = (0 != ((int)io & (1 << 20))) ? true : false;
            radioButton21.IsChecked = (0 != ((int)io & (1 << 21))) ? true : false;
            radioButton22.IsChecked = (0 != ((int)io & (1 << 22))) ? true : false;
            radioButton23.IsChecked = (0 != ((int)io & (1 << 23))) ? true : false;
            radioButton24.IsChecked = (0 != ((int)io & (1 << 24))) ? true : false;
        }

        public void Timer_E(object sender,System.Timers.ElapsedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    MyIO();
                }));
            });
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            string st = button0.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(0, 1);
                button0.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(0, 0);
                button0.Content = "ON";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string st = button1.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(1, 1);
                button1.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(1, 0);
                button1.Content = "ON";
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string st = button2.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(2, 1);
                button2.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(2, 0);
                button2.Content = "ON";
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string st = button3.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(3, 1);
                button3.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(3, 0);
                button3.Content = "ON";
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            string st = button4.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(4, 1);
                button4.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(4, 0);
                button4.Content = "ON";
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string st = button5.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(5, 1);
                button5.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(5, 0);
                button5.Content = "ON";
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            string st = button6.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(6, 1);
                button6.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(6, 0);
                button6.Content = "ON";
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            string st = button7.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(7, 1);
                button7.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(7, 0);
                button7.Content = "ON";
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            string st = button8.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(8, 1);
                button8.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(8, 0);
                button8.Content = "ON";
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            string st = button9.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(9, 1);
                button9.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(9, 0);
                button9.Content = "ON";
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            string st = button10.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(10, 1);
                button10.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(10, 0);
                button10.Content = "ON";
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            string st = button11.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(11, 1);
                button11.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(11, 0);
                button11.Content = "ON";
            }
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            string st = button12.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(12, 1);
                button12.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(12, 0);
                button12.Content = "ON";
            }
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            string st = button13.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(13, 1);
                button13.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(13, 0);
                button13.Content = "ON";
            }
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            string st = button14.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(14, 1);
                button14.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(14, 0);
                button14.Content = "ON";
            }
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            string st = button15.Content.ToString();
            if ("ON" == st)
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(15, 1);
                button15.Content = "OFF";
            }
            else
            {
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(15, 0);
                button15.Content = "ON";
            }
        }


    }
}
