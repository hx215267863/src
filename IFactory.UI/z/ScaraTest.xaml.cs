using IFactory.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace IFactory.UI.Debug
{
    /// <summary>
    /// ScaraTest.xaml 的交互逻辑
    /// </summary>
    public partial class ScaraTest : BasePage, IComponentConnector
    {
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }


        private void buttonGetPos_Click(object sender, RoutedEventArgs e)
        {
            double x, y, z, c;
            MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.GetCurPos(out x, out y, out z, out c);
            string msg = "CurPos: X " + Convert.ToString(x) + " Y " + Convert.ToString(y) + " Z " + Convert.ToString(z) + " U " + Convert.ToString(c);
            labelCurPos.Content = msg;
        }

        private void buttonMoveToPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string stx = textBoxX.Text;
                string sty = textBoxY.Text;
                string stz = textBoxZ.Text;
                string stu = textBoxU.Text;
                string stsz = textBoxSaveZ.Text;

                double x, y, z, u, sz;

                x = Convert.ToDouble(stx);
                y = Convert.ToDouble(sty);
                z = Convert.ToDouble(stz);
                u = Convert.ToDouble(stu);
                sz = Convert.ToDouble(stsz);
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.MoveToPos(x, y, z, u, sz);
            }
            catch
            {
                return;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string st0 = textBoxCX.Text;
                string st1 = textBoxCY.Text;
                string st2 = textBoxCZ.Text;
                string st3 = textBoxCU.Text;

                string st4 = textBoxPX.Text;
                string st5 = textBoxPY.Text;
                string st6 = textBoxPZ.Text;
                string st7 = textBoxPU.Text;

                string st8 = textBoxSaveZ.Text;
                double x1, y1, z1, u1, x2, y2, z2, u2, sz;

                x1 = Convert.ToDouble(st0);
                y1 = Convert.ToDouble(st1);
                z1 = Convert.ToDouble(st2);
                u1 = Convert.ToDouble(st3);

                x2 = Convert.ToDouble(st4);
                y2 = Convert.ToDouble(st5);
                z2 = Convert.ToDouble(st6);
                u2 = Convert.ToDouble(st7);

                sz = Convert.ToDouble(st8);
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.CatchAndPutBattery(x1, y1, z1, u1, x2, y2, z2, u2, sz);
            }
            catch
            {
                return;
            }
        }

        private void buttonReadIO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ulong IO = 0;
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.ReadIO(out IO);

                string str = "IO 0-23 :";
                for (int i = 0; i < 24; i++)
                {
                    if ((0 == i % 8) && (0 != i))
                    {
                        str += "  . ";
                    }

                    if (0 != ((1 << i) & (uint)IO))
                    {
                        str += " 1";
                    }
                    else
                    {
                        str += " 0";
                    }
                }

                if (0 != ((1 << 24) & (uint)IO))
                {
                    str += " Arrivel";

                }
                else
                {
                    str += " ";
                }
                labelIO.Content = str;
            }
            catch
            {
                return;
            }
        }

        private void buttonSetIO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string stb = textBoxBit.Text;
                string stv = textBoxV.Text;
                int b, v;

                b = Convert.ToInt32(stb);
                v = Convert.ToInt32(stv);
                MainWindow.m_MainWindow.m_MainCrtl.mEpsonScaraRobot.WriteIO(b, v);
            }
            catch
            {
                return;
            }
        }
    }
}
