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

namespace IFactory.UI.Diagnostic
{
    /// <summary>
    /// LightController.xaml 的交互逻辑
    /// </summary>
    public partial class LightController : BasePage, IComponentConnector
    {
        public LightController()
        {
            InitializeComponent();
        }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }      
        private void btnMoveIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value1 = Convert.ToInt32(txtMoveIn_1.Text.Trim());
                var value2 = Convert.ToInt32(txtMoveIn_2.Text.Trim());
                MainWindow.m_MainCtrl._lightController_MoveIn.SetLightBox(value1, value2);
            }
            catch (Exception ex)
            {
                //TODO:提示错误信息                
            }        
        }

        private void btnTrayA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value1 = Convert.ToInt32(txtTrayA_1.Text.Trim());
                var value2 = Convert.ToInt32(txtTrayA_2.Text.Trim());
                var value3 = Convert.ToInt32(txtTrayA_3.Text.Trim());
                var value4 = Convert.ToInt32(txtTrayA_4.Text.Trim());
                MainWindow.m_MainCtrl._lightController_TrayA.SetLightBox(value1, value2,value3,value4);
            }
            catch (Exception ex)
            {
                //TODO:提示错误信息                
            }
        }

        private void btnTrayB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value1 = Convert.ToInt32(txtTrayB_1.Text.Trim());
                var value2 = Convert.ToInt32(txtTrayB_2.Text.Trim());
                var value3 = Convert.ToInt32(txtTrayB_3.Text.Trim());
                var value4 = Convert.ToInt32(txtTrayB_4.Text.Trim());
                MainWindow.m_MainCtrl._lightController_TrayB.SetLightBox(value1, value2, value3, value4);
            }
            catch (Exception ex)
            {
                //TODO:提示错误信息                
            }
        }

        private void btnTrayC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value1 = Convert.ToInt32(txtTrayC_1.Text.Trim());
                var value2 = Convert.ToInt32(txtTrayC_2.Text.Trim());
                var value3 = Convert.ToInt32(txtTrayC_3.Text.Trim());
                var value4 = Convert.ToInt32(txtTrayC_4.Text.Trim());
                MainWindow.m_MainCtrl._lightController_TrayC.SetLightBox(value1, value2, value3, value4);
            }
            catch (Exception ex)
            {
                //TODO:提示错误信息                
            }
        }

        private void btnTrayD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value1 = Convert.ToInt32(txtTrayD_1.Text.Trim());
                var value2 = Convert.ToInt32(txtTrayD_2.Text.Trim());
                var value3 = Convert.ToInt32(txtTrayD_3.Text.Trim());
                var value4 = Convert.ToInt32(txtTrayD_4.Text.Trim());
                MainWindow.m_MainCtrl._lightController_TrayD.SetLightBox(value1, value2, value3, value4);
            }
            catch (Exception ex)
            {
                //TODO:提示错误信息                
            }
        }

        private void btnTrayE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value1 = Convert.ToInt32(txtTrayE_1.Text.Trim());
                var value2 = Convert.ToInt32(txtTrayE_2.Text.Trim());
                var value3 = Convert.ToInt32(txtTrayE_3.Text.Trim());
                var value4 = Convert.ToInt32(txtTrayE_4.Text.Trim());
                MainWindow.m_MainCtrl._lightController_TrayE.SetLightBox(value1, value2, value3, value4);
            }
            catch (Exception ex)
            {
                //TODO:提示错误信息                
            }
        }
        string picFilePath = @"D:\Picture\temp\";
        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {         
              var barcodeScanner=  MainWindow.m_MainCtrl._barcodeScanner;
                if (barcodeScanner.StartRead())
                {
                    int ret = 0;
                    string barcode = string.Empty;
                    ret = barcodeScanner.GetBarCode(out barcode);
                    barcodeScanner.StopRead();
                    if (0 == ret)
                    {
                        string error = "ERROR";
                        barcode = barcode.Replace("\r", "");
                        if (barcode.IndexOf(error) > -1)
                        {
                            //显示Err
                            txtSN.Text = "Error";
                        }
                        else
                        {
                            //成功显示Barcode
                            txtSN.Text = barcode;
                        }
                    }
                    else
                    {
                        //显示Fail
                        txtSN.Text = "Fail";
                    }
                }
                else
                {  
                    throw new Exception("访问扫码枪失败");
                }                
            }
            catch (Exception ex)
            {
                txtSN.Text = ex.Message;
            }
        }

        private void btnMoveInTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            picFilePath +="MoveIn_"+ DateTime.Now.ToString("yyyyMMddhhmmss")+"jpg" ;
            byte[] buff = new byte[MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_MoveIn*MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_MoveIn];
            MainWindow.m_MainCtrl._moveInCamera.TakeSinglePicture(buff,buff.Length);
            MainWindow.m_MainCtrl._moveInCamera.SaveBitmapImageIntoFile(buff, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_MoveIn, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_MoveIn, picFilePath);
            txtPhotoPath.Text = picFilePath;
        }

        private void btnTakePic_TrayA_Click(object sender, RoutedEventArgs e)
        {
            picFilePath += "TrayA_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "jpg";
            byte[] buff = new byte[MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayA * MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayA];
            MainWindow.m_MainCtrl._trayACamera.TakeSinglePicture(buff, buff.Length);
            MainWindow.m_MainCtrl._trayACamera.SaveBitmapImageIntoFile(buff, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayA, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayA, picFilePath);
            txtPhotoPath.Text = picFilePath;
        }

        private void btnTakePic_TrayB_Click(object sender, RoutedEventArgs e)
        {
            picFilePath += "TrayB_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "jpg";
            byte[] buff = new byte[MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayB * MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayB];
            MainWindow.m_MainCtrl._trayBCamera.TakeSinglePicture(buff, buff.Length);
            MainWindow.m_MainCtrl._trayBCamera.SaveBitmapImageIntoFile(buff, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayB, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayB, picFilePath);
            txtPhotoPath.Text = picFilePath;
        }

        private void btnTakePic_TrayC_Click(object sender, RoutedEventArgs e)
        {
            picFilePath += "TrayC_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "jpg";
            byte[] buff = new byte[MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayC * MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayC];
            MainWindow.m_MainCtrl._trayCCamera.TakeSinglePicture(buff, buff.Length);
            MainWindow.m_MainCtrl._trayCCamera.SaveBitmapImageIntoFile(buff, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayC, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayC, picFilePath);
            txtPhotoPath.Text = picFilePath;
        }

        private void btnTakePic_TrayD_Click(object sender, RoutedEventArgs e)
        {
            picFilePath += "TrayD_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "jpg";
            byte[] buff = new byte[MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayD * MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayD];
            MainWindow.m_MainCtrl._trayDCamera.TakeSinglePicture(buff, buff.Length);
            MainWindow.m_MainCtrl._trayDCamera.SaveBitmapImageIntoFile(buff, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayD, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayD, picFilePath);
            txtPhotoPath.Text = picFilePath;
        }

        private void btnTakePic_TrayE_Click(object sender, RoutedEventArgs e)
        {
            picFilePath += "TrayE_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "jpg";
            byte[] buff = new byte[MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayE * MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayE];
            MainWindow.m_MainCtrl._trayECamera.TakeSinglePicture(buff, buff.Length);
            MainWindow.m_MainCtrl._trayECamera.SaveBitmapImageIntoFile(buff, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraWidth_TrayE, (int)MainWindow.m_MainCtrl.m_CameraConfig.CameraHeight_TrayE, picFilePath);
            txtPhotoPath.Text = picFilePath;
        }

        private void btnMoveIn_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl._lightController_MoveIn.SetLightBox(0,0);
        }

        private void btnTrayA_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl._lightController_TrayA.SetLightBox(0, 0,0,0);
        }

        private void btnTrayB_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl._lightController_TrayB.SetLightBox(0, 0, 0, 0);
        }

        private void btnTrayC_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl._lightController_TrayC.SetLightBox(0, 0, 0, 0);
        }

        private void btnTrayD_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl._lightController_TrayD.SetLightBox(0, 0, 0, 0);
        }

        private void btnTrayE_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.m_MainCtrl._lightController_TrayE.SetLightBox(0, 0, 0, 0);
        }
    }
}
