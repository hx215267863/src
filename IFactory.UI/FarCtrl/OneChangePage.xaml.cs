using System.Windows;
using IFactory.UI.Controls;
using System.Windows.Markup;
using System.Runtime.InteropServices;
using System.Text;
using IFactory.Domain.Models;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Windows.Media;
using System.Windows.Threading;
//using Microsoft.Win32;

namespace IFactory.UI.FarCtrl
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class OneChange : BasePage, IComponentConnector
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private DispatcherTimer statusCheckTimer = new DispatcherTimer();

        public OneChange()
        {
            select = false;
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.statusCheckTimer.Interval = new TimeSpan(0, 0, 1);
            
            this.statusCheckTimer.Tick += new EventHandler(this.statusCheckTimer_Tick);
           
            this.statusCheckTimer.Start();
			//MainWindow.m_MainCtrl.pcg.VisionConfig = true;
   //         MainWindow.m_MainCtrl.pcg.VisionDirt = true;
        }

        private void statusCheckTimer_Tick(object sender, EventArgs e)
        {
            if(MainWindow.LoginFlag)
            {
                label_Login.Content = "登录成功";
                label_Login.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                textBox_ChangePassword.Visibility = Visibility.Visible;
                button_ChangePassword.Visibility = Visibility.Visible;
            }
            else
            {
                label_Login.Content = "未登录";
                label_Login.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                textBox_ChangePassword.Visibility = Visibility.Hidden;
                button_ChangePassword.Visibility = Visibility.Hidden;
            }
        }


        static public bool oneKey_flag = false;

        public int isReady { get; set; }

        static public int isReady_change { get; set; }

        private bool select;

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            string pro = textBoxProduct.Text;

            if (pro.Length > 0)
            {
                List<AllUsefullProductModel> aaa = LocalApi.GetAllPara(pro);

                if(aaa.Count == 0 )
                {
                    System.Windows.MessageBox.Show("未查询到相关配置");
                }
                else
                {
                    try
                    {
                        //short count = System.Convert.ToInt16(aaa[0].QTY_FOR_TARY);

                        //if ( aaa.Count < count)
                        //{
                        //    System.Windows.MessageBox.Show("槽位坐标配置少于每盒电池数");
                        //    return;
                        //}

                        //MainWindow.m_MainCtrl.m_ProductConfig.iTrayBatteryCount = count;

                        //MainWindow.m_MainCtrl.m_ProductConfig.iMaxTrayCount = System.Convert.ToInt16(aaa[0].QTY_FOR_CRIB);

                        //MainWindow.m_MainCtrl.m_BatteryVisionConfig.product = aaa[0].ITEM_CD;

                        //MainWindow.m_MainCtrl.m_BatteryVisionConfig.dHeight = System.Convert.ToDouble(aaa[0].ITEM_LONG);
                        //MainWindow.m_MainCtrl.m_BatteryVisionConfig.dWidth = System.Convert.ToDouble(aaa[0].ITEM_WIDE);

                        //if("银色" == aaa[0].ITEM_COLOR)
                        //{
                        //    MainWindow.m_MainCtrl.m_BatteryVisionConfig.iBatteryColor = 1;
                        //    MainWindow.m_MainCtrl.mMoveInCamera.SetExposureTime(MainWindow.m_MainCtrl.m_BatteryVisionConfig.dSliveryMoveInCameraExposureTime);
                        //}
                        //else if ("黑色" == aaa[0].ITEM_COLOR)
                        //{
                        //    MainWindow.m_MainCtrl.m_BatteryVisionConfig.iBatteryColor = 0;
                        //    MainWindow.m_MainCtrl.mMoveInCamera.SetExposureTime(MainWindow.m_MainCtrl.m_BatteryVisionConfig.dBlackMoveInCameraExposureTime);
                        //}

                        //bool[] vvv = new bool[aaa.Count];
                        //for (int i = 0; i < aaa.Count; i++)
                        //{
                        //    vvv[i] = false;
                        //}


                        //for (int i = 0;i< aaa.Count;i++)
                        //{
                        //    int index = 0;
                        //    if     ( "一" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 0;   
                        //    }
                        //    else if ("二" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 1;
                        //    }
                        //    else if ("三" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 2;
                        //    }
                        //    else if ("四" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 3;
                        //    }
                        //    else if ("五" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 4;
                        //    }
                        //    else if ("六" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 5;
                        //    }
                        //    else if ("七" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 6;
                        //    }
                        //    else if ("八" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 7;
                        //    }
                        //    else if ("九" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 8;
                        //    }
                        //    else if ("十" == aaa[i].SLOT_SITE)
                        //    {
                        //        index = 9;
                        //    }

                        //    vvv[index] = true;

                        //    MainWindow.m_MainCtrl.m_ProductConfig.iLightnessTray[index, 0] = System.Convert.ToInt16(aaa[i].LIGHT_1);
                        //    MainWindow.m_MainCtrl.m_ProductConfig.iLightnessTray[index, 1] = System.Convert.ToInt16(aaa[i].LIGHT_2);
                        //    MainWindow.m_MainCtrl.m_ProductConfig.iLightnessTray[index, 2] = System.Convert.ToInt16(aaa[i].LIGHT_3);
                        //    MainWindow.m_MainCtrl.m_ProductConfig.iLightnessTray[index, 3] = System.Convert.ToInt16(aaa[i].LIGHT_4);

                        //    MainWindow.m_MainCtrl.m_SysConfig.dScaraReleasePosX[index] = System.Convert.ToDouble(aaa[i].SLOT_X_DOT);
                        //    MainWindow.m_MainCtrl.m_SysConfig.dScaraReleasePosY[index] = System.Convert.ToDouble(aaa[i].SLOT_Y_DOT);
                        //    MainWindow.m_MainCtrl.m_SysConfig.dScaraReleasePosZ[index] = System.Convert.ToDouble(aaa[i].SLOT_Z_DOT);
                        //    MainWindow.m_MainCtrl.m_SysConfig.dScaraReleasePosU[index] = System.Convert.ToDouble(aaa[i].SLOT_U_DOT);
                        //}

                        for (int i = 0; i < aaa.Count; i++)
                        {
                            //if( vvv[i] == false )
                            //{
                            //    System.Windows.MessageBox.Show("槽位配置不全");
                            //    return;
                            //}
                        }

                        if( 0 != MainWindow.m_MainCtrl.LoadXML("D:\\Picture\\template_liyang\\"+ pro + "\\BatteryBoxParameters.xml"))
                        {
                            System.Windows.MessageBox.Show("读取视觉配置文件异常");
                            return;
                        }
                        

                        label_A.Content = pro;

                        MainWindow.m_MainCtrl.SetProductConfig();

                        select = true;

                        //checkBox_BIS.IsChecked = true;
                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("获取参数失败，请检查配置格式");
                    }
                }
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if (0 != MainWindow.m_MainCtrl.SetBis(true))
            {
                checkBox_BIS.IsChecked = false;
                System.Windows.MessageBox.Show("程序已经运行，无法更改BIS");
            }
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if ( 0 != MainWindow.m_MainCtrl.SetBis(false) )
            {
                System.Windows.MessageBox.Show("程序已经运行，无法更改BIS");
                checkBox_BIS.IsChecked = true;
            }
        }

        private void checkBox_VIS_Checked(object sender, RoutedEventArgs e)
        {
            //MainWindow.m_MainCtrl.pcg.VisionConfig = true;
        }

        private void checkBox_VIS_Unchecked(object sender, RoutedEventArgs e)
        {
            //MainWindow.m_MainCtrl.pcg.VisionConfig = false;
        }

        private void checkBox_Dirt_Checked(object sender, RoutedEventArgs e)
        {
            //MainWindow.m_MainCtrl.pcg.VisionDirt = true;
        }

        private void checkBox_Dirt_Unchecked(object sender, RoutedEventArgs e)
        {
            //MainWindow.m_MainCtrl.pcg.VisionDirt = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
//----------------------------------test by ly---------------------------------------------------------------------------
            /*
            double x1, y1, a;

            if (0 != MainWindow.m_MainCtrl.mVision.GetBatteryPosByFile(null, 0, 0, out x1, out y1, out a, "D:/aaa", false))
            {
                System.Windows.MessageBox.Show("计算电池位置失败，请检查光源/电池参数");
                return;
            }
            */
//-----------------------------------------------------------------------------------------------------------------------
            if (!select)
            {
                System.Windows.MessageBox.Show("请先选择产品");
                return;
            }

            int value = Convert.ToInt32(textBox.Text);

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有Jpg文件(*.jpg)|*.jpg";

            string file = "";
            if ( dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
                double x, y, z;
                int aaa = MainWindow.m_MainCtrl.mVision.CheckBatteryStatusFile(file, 2592, 1944, value, "", out x, out y, out z, false, true);
                if (0 == aaa)
                {
                    System.Windows.MessageBox.Show("OK");
                }
                else
                {
                    System.Windows.MessageBox.Show("NG");
                }
            }
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            //string password = MainWindow.m_MainCtrl.m_IniCtrl.GetIni(@"D:\Debug.ini", "Global", "password");
            //if (textBox_password.Text == password)
            //{
            //    MainWindow.LoginFlag = true;
            //    textBox_password.Text = "";
            //} 
            //else
            //{
            //    System.Windows.MessageBox.Show("密码错误");
            //}
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LoginFlag = false;
        }

        private void checkBox_VIS_Click(object sender, RoutedEventArgs e)
        {
            //勾选
            if(checkBox_VIS.IsChecked == true)
            {
                //if (MainWindow.LoginFlag)
                //{
                //    MainWindow.m_MainCtrl.pcg.VisionConfig = true;
                //    checkBox_VIS.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                //}
                //else
                //{
                //    checkBox_VIS.IsChecked = false;
                //    //checkBox_VIS.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                //    System.Windows.MessageBox.Show("当前没有修改权限");
                //}
            }
            //取消勾选
            else {
                //if (MainWindow.LoginFlag)
                //{
                //    MainWindow.m_MainCtrl.pcg.VisionConfig = false;
                //    checkBox_VIS.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                //}
                //else
                //{
                //    checkBox_VIS.IsChecked = true;
                //    //checkBox_VIS.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                //    System.Windows.MessageBox.Show("当前没有修改权限");
                //}
            }
        }

        private void checkBox_Dirt_Click(object sender, RoutedEventArgs e)
        {
            //勾选
            if (checkBox_Dirt.IsChecked == true)
            {
                //if (MainWindow.LoginFlag)
                //{
                //    MainWindow.m_MainCtrl.pcg.VisionDirt = true;
                //    checkBox_Dirt.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                //}
                //else
                //{
                //    checkBox_Dirt.IsChecked = false;
                //    //checkBox_Dirt.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                //    System.Windows.MessageBox.Show("当前没有修改权限");
                //}
            }
            //取消勾选
            else
            {
                //if (MainWindow.LoginFlag)
                //{
                //    MainWindow.m_MainCtrl.pcg.VisionDirt = false;
                //    checkBox_Dirt.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                //}
                //else
                //{
                //    checkBox_Dirt.IsChecked = true;
                //    //checkBox_Dirt.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                //    System.Windows.MessageBox.Show("当前没有修改权限");
                //}
            }
        }

        private void checkBox_BIS_Click(object sender, RoutedEventArgs e)
        {
            //勾选
            if (checkBox_BIS.IsChecked == true)
            {
                if (MainWindow.LoginFlag)
                {
                    checkBox_BIS.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                }
                else
                {
                    checkBox_BIS.IsChecked = false;
                    //checkBox_BIS.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    System.Windows.MessageBox.Show("当前没有修改权限");
                }
            }
            //取消勾选
            else
            {
                if (MainWindow.LoginFlag)
                {
                    checkBox_BIS.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else
                {
                    checkBox_BIS.IsChecked = true;
                    //checkBox_BIS.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    System.Windows.MessageBox.Show("当前没有修改权限");
                }
            }
        }

        private void button_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ////读配置文件
            //string password = MainWindow.m_MainCtrl.m_IniCtrl.GetIni(@"D:\Debug.ini", "Global", "password");
            ////密码跟原来的是否一样
            //if(textBox_ChangePassword.Text != password)
            //{
            //    MainWindow.m_MainCtrl.m_IniCtrl.SetIni(@"D:\Debug.ini", "Global", "password", textBox_ChangePassword.Text);
            //    textBox_ChangePassword.Text = "";
            //    MainWindow.LoginFlag = false;
            //    System.Windows.MessageBox.Show("密码修改成功");
            //}else
            //{
            //    System.Windows.MessageBox.Show("新密码与原来密码一致，无法修改！！！");
            //}
        }

        private void checkBox_ANDON_Click(object sender, RoutedEventArgs e)
        {
            ////勾选
            //if (checkBox_ANDON.IsChecked == true)
            //{
            //    if (MainWindow.LoginFlag)
            //    {
            //        MainWindow.m_MainCtrl.pcg.ANDONFlag = true;
            //        checkBox_ANDON.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            //    }
            //    else
            //    {
            //        checkBox_ANDON.IsChecked = false;
            //        //checkBox_ANDON.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            //        System.Windows.MessageBox.Show("当前没有修改权限");
            //    }
            //} //取消勾选
            //else
            //{
            //    if (MainWindow.LoginFlag)
            //    {
            //        MainWindow.m_MainCtrl.pcg.ANDONFlag = false;
            //        checkBox_ANDON.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            //    }
            //    else
            //    {
            //        checkBox_ANDON.IsChecked = true;
            //        //checkBox_ANDON.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            //        System.Windows.MessageBox.Show("当前没有修改权限");
            //    }
            //}
        }
    }
}
