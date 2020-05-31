using ATL_MC.EpsonScaraRobotController;
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

namespace IFactory.UI.Diagnostic
{
    /// <summary>
    /// ScaraTest.xaml 的交互逻辑
    /// </summary>
    public partial class ScaraTest : BasePage, IComponentConnector
    {
        public ScaraTest()
        {
            InitializeComponent();
        }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void buttonGetPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x, y, z, c;
                SixAxisPose currentPos = MainWindow.m_MainCtrl._robotController.ROBOT_GetRobotPosition();
                string msg = $"CurPos=>X:{currentPos.xAxis},Y:{currentPos.yAxis},Z:{currentPos.zAxis},rx:{currentPos.rxAxis},ry:{currentPos.ryAxis},rz:{currentPos.rzAxis},fig:{currentPos.fig}";
                txtPos.Text = msg;
            }
            catch (Exception)
            {
                //提示报错信息             
            }           
        }

        private void buttonMoveToPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.m_MainCtrl.GetSysRunning())
                {
                    MessageBox.Show("程序已启动，无法执行该动作，请重开程序");
                    return;
                }

                //ATL_MC.MainCtrl.SystemConfig aa = MainWindow.m_MainCtrl.m_SysConfig;

                double cx, cy, cz, cu;
                //MainWindow.m_MainCtrl.mEpsonScaraRobot.GetCurPos(out cx,out cy,out cz,out cu);

                //if( (Math.Abs(cx- aa.dScaraStandbyPosX)>50.0) ||
                //    (Math.Abs(cy - aa.dScaraStandbyPosY) > 50.0) )
                //{
                //    MessageBox.Show("机械手不在零点附近，无法执行该动作");
                //    return;
                //}

                //MainWindow.m_MainCtrl.mLightController1.SetLightBox(250, 250);

                Thread.Sleep(50);

                double x, y, a;

                //MainWindow.m_MainCtrl.mVision.XXXXXXXXX(0, "D:/0.bmp", out x, out y, out a);

                //long w = MainWindow.m_MainCtrl.mMoveInCamera.GetWidth();
                //long h = MainWindow.m_MainCtrl.mMoveInCamera.GetHeight();

                //Byte[] bmp = new Byte[w * h];

                //MainWindow.m_MainCtrl.mMoveInCamera.TakeSinglePicture(bmp, (int)(w * h));

                //MainWindow.m_MainCtrl.mLightController1.SetLightBox(0, 0);

                //MainWindow.m_MainCtrl.mMoveInCamera.SaveBitmapImageIntoFile(bmp, (int)w, (int)h, "D:/aaaaa.bmp");

                //if (0 != MainWindow.m_MainCtrl.mVision.GetBatteryPos(bmp, w, h, out x, out y, out a, "D:/aaa", false))
                //{
                //    MessageBox.Show("计算电池位置失败，请检查光源/电池参数");
                //    return;
                //}

                //if(( Math.Abs(x) < 50.0) &&
                //    (Math.Abs(y) < 50.0) &&
                //    (Math.Abs(a) < 35.0) )
                //{
                //MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraCatchPosX - y,
                //                                                  aa.dScaraCatchPosY + x,
                //                                                  aa.dScaraCatchPosZ,
                //                                                  aa.dScaraCatchPosU + a,
                //                                                  aa.dScaraSaveZ, 0);
                //MainWindow.m_MainCtrl.mEpsonScaraRobot.WriteIO(aa.Output_ADDR_Uacuum, 1);
                //Thread.Sleep(100);

                //MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraStandbyPosX + 40.0,
                //                                                  aa.dScaraStandbyPosY,
                //                                                  aa.dScaraStandbyPosZ,
                //                                                  aa.dScaraStandbyPosU,
                //                                                  aa.dScaraSaveZ, 0);
                //}
                //else
                //{
                //    MessageBox.Show("计算电池位置偏差过大");
                //}
            }
            catch
            {
                return;
            }
        }
              

        private void buttonNGPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.m_MainCtrl.GetSysRunning())
                {
                    MessageBox.Show("程序已启动，无法执行该动作，请重开程序");
                    return;
                }

                //ATL_MC.MainCtrl.SystemConfig aa = MainWindow.m_MainCtrl.m_SysConfig;

                //double cx, cy, cz, cu;
                //MainWindow.m_MainCtrl.mEpsonScaraRobot.GetCurPos(out cx, out cy, out cz, out cu);

                //if ((Math.Abs(cx - aa.dScaraStandbyPosX) > 50.0) ||
                //    (Math.Abs(cy - aa.dScaraStandbyPosY) > 50.0))
                //{
                //    MessageBox.Show("机械手不在零点附近，无法执行该动作");
                //    return;
                //}

                //MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraNGPosX,
                //                                                      aa.dScaraNGPosY,
                //                                                      aa.dScaraNGPosZ,
                //                                                      aa.dScaraNGPosU,
                //                                                      aa.dScaraSaveZ, 2);
            }
            catch (Exception err)
            {

            }
        }

        private void buttonStandbyPos_Click(object sender, RoutedEventArgs e)
        {
            int iret = 0;
            bool status = false;
            double dCurX, dCurY, dCurZ, dCurU;
            try
            {
                //if (MainWindow.m_MainCtrl.GetSysStart())
                //{
                //    MessageBox.Show("程序已启动，无法执行该动作，请重开程序");
                //    return;
                //}
                //ATL_MC.MainCtrl.SystemConfig aa = MainWindow.m_MainCtrl.m_SysConfig;

                //iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.GetCurPos(out dCurX, out dCurY, out dCurZ, out dCurU);
                //if(iret == 0)
                //{
                //    //把机械手抬起到安全高度
                //    iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(dCurX, dCurY, aa.dScaraStandbyPosZ, aa.dScaraStandbyPosU, aa.dScaraSaveZ, 99);
                //}
                //iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.GetCurPos(out dCurX, out dCurY, out dCurZ, out dCurU);
                //if (0 == iret)
                //{
                //    if ((Math.Abs(dCurX - MainWindow.m_MainCtrl.m_SysConfig.dScaraStandbyPosX) < 150.0) &&
                //        (Math.Abs(dCurY - MainWindow.m_MainCtrl.m_SysConfig.dScaraStandbyPosY) < 150.0))
                //    {
                //        //如果当前位置在待机位附近，可以直接过去
                //        iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraStandbyPosX, aa.dScaraStandbyPosY,
                //                                          aa.dScaraStandbyPosZ, aa.dScaraStandbyPosU, aa.dScaraSaveZ, 0);
                //    }
                //    else if ((Math.Abs(dCurX - aa.dScaraCatchPosX) < 50.0) &&
                //             (Math.Abs(dCurY - aa.dScaraCatchPosY) < 50.0))
                //    {
                //        //如果当前位置在取料位附近，可以直接过去
                //        iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraStandbyPosX, aa.dScaraStandbyPosY,
                //                                          aa.dScaraStandbyPosZ, aa.dScaraStandbyPosU, aa.dScaraSaveZ, 0);
                //    }
                //    else if ((Math.Abs(dCurX - aa.dScaraNGPosX) < 10.0) &&
                //             (Math.Abs(dCurY - aa.dScaraNGPosY) < 10.0))
                //    {
                //        //机械手在NG料盒位置，用轨迹5走到待机位置
                //        iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraStandbyPosX, aa.dScaraStandbyPosY,
                //                                          aa.dScaraStandbyPosZ, aa.dScaraStandbyPosU, aa.dScaraSaveZ, 5);
                //    }
                //    else if (dCurX < aa.dScaraStandbyPosX)
                //    {
                //        //如果当前位置在待机位置左侧，可以直接走到待机位
                //        iret = MainWindow.m_MainCtrl.mEpsonScaraRobot.MoveToPos(aa.dScaraStandbyPosX, aa.dScaraStandbyPosY,
                //                                          aa.dScaraStandbyPosZ, aa.dScaraStandbyPosU, aa.dScaraSaveZ, 0);
                //    }
                //    else
                //    {
                //        //机械手在不确定的位置
                //        MessageBox.Show("ThreadHome:机械手在不确定的位置，请推到待机位附近");
                //    }

                //    if (0 == iret)
                //    {
                //        MessageBox.Show("ThreadHome:机械手开始移动到待机位");
                //    }
                //    else
                //    {
                //        MessageBox.Show("ThreadHome:机械手通讯失败");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("ThreadHome:读取机械手位置失败");
                //}
            }
            catch (Exception err)
            {

            }
        }

       

        private void buttonStandbyAPos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonStandbyBPos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
