using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;
using System.Diagnostics;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Reflection;

namespace BaslerCamera
{
    public class BaslerCamera
    {
        private bool bLineTrigger = false;
        private Camera camera = null;
        private PixelDataConverter converter = null;
        bool bSimulate = true;

        long lWidth = 400, lHeight = 300;

        IntPtr latestFrameAddress = IntPtr.Zero;

        public delegate void delegateProcessHImage(int width, int height, IntPtr frameAddress);

        public event delegateProcessHImage eventProcessImage;

        public int OpenCamera(string SN,bool simulate)
        {
            bSimulate = simulate;
            if (bSimulate)
            { 
                return 0;
            }

            try
            {
                if (null == converter)
                {
                    converter = new PixelDataConverter();
                }

                List<ICameraInfo> allCameras = CameraFinder.Enumerate();

                foreach (ICameraInfo cameraInfo in allCameras)
                {
                    if (SN == cameraInfo[CameraInfoKey.SerialNumber])
                    {
                        camera = new Camera(cameraInfo);

                        camera.CameraOpened += Configuration.SoftwareTrigger;

                        if (camera.Parameters[PLCameraInstance.GrabCameraEvents].IsWritable)
                        {
                            camera.Parameters[PLCameraInstance.GrabCameraEvents].SetValue(false);
                        }
                        else
                        {
                            throw new Exception("Can not disable GrabCameraEvents.");
                        }

                        camera.Open();
                        
                        lWidth = camera.Parameters[PLCamera.Width].GetValue();
                        lHeight = camera.Parameters[PLCamera.Height].GetValue();

                        return 0; ;
                    }
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }
            return 1;
        }

        public int GetExposureTime(out double dExposuretime)
        {
            dExposuretime = 0.0;

            if (bSimulate)
            {
                return 0;
            }

            if (null == camera)
                return -1;

            try
            {
                if (camera.Parameters.Contains(PLCamera.GainAbs))
                {
                    dExposuretime = camera.Parameters[PLCamera.ExposureTimeAbs].GetValue();
                }
                else
                {
                    dExposuretime = camera.Parameters[PLCamera.ExposureTime].GetValue();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }

            return 0;
        }

        public int SetExposureTime( double dExposuretime)
        {
            if (bSimulate)
            {
                return 0;
            }

            double max = 0.0,min = 0.0;
            try
            {
                if (camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                {
                    max = camera.Parameters[PLCamera.ExposureTimeAbs].GetMaximum();
                    min = camera.Parameters[PLCamera.ExposureTimeAbs].GetMinimum();
                }
                else if (camera.Parameters.Contains(PLCamera.ExposureTime))
                {
                    max = camera.Parameters[PLCamera.ExposureTime].GetMaximum();
                    min = camera.Parameters[PLCamera.ExposureTime].GetMinimum();
                }

                if ((dExposuretime > max) || (dExposuretime < min))
                {
                    return -1;
                }

                if (camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                {
                    camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(dExposuretime);
                }
                else if (camera.Parameters.Contains(PLCamera.ExposureTime))
                {
                    camera.Parameters[PLCamera.ExposureTime].SetValue(dExposuretime);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }
            return 0;
        }

        public long GetWidth()
        {
            if (bSimulate)
            {
                return 0;
            }
            else
            {
                return camera.Parameters[PLCamera.Width].GetValue();
            }
        }

        public long GetHeight()
        {
            if (bSimulate)
            {
                return 0;
            }
            else
            {
                return camera.Parameters[PLCamera.Height].GetValue();
            }
        }

        public int SetImageSize(long width,long height,long widthoffset,long heightoffset)
        {
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                camera.Parameters[PLCamera.OffsetX].TrySetValue(0);
                camera.Parameters[PLCamera.OffsetY].TrySetValue(0);

                long lWidthMax = camera.Parameters[PLCamera.Width].GetMaximum();
                long lHeightMax = camera.Parameters[PLCamera.Height].GetMaximum();
                
                if( ( (width + widthoffset) > lWidthMax) || 
                    ( (height + heightoffset ) > lHeightMax))
                {
                    return -1;
                }

                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }

                camera.Parameters[PLCamera.Width].TrySetValue(width);
                camera.Parameters[PLCamera.Height].TrySetValue(height);

                camera.Parameters[PLCamera.OffsetX].TrySetValue(widthoffset);
                camera.Parameters[PLCamera.OffsetY].TrySetValue(heightoffset);

                lWidth = width;
                lHeight = height;

                if ( latestFrameAddress == IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(latestFrameAddress);
                    latestFrameAddress = IntPtr.Zero;
                    latestFrameAddress = Marshal.AllocHGlobal((Int32)(lWidth * lHeight));
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }
            return 0;
        }

        public int TakeSinglePicture(Byte [] data,Int32 len)
        {
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                camera.StreamGrabber.Start(1);
                if (camera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException))
                {
                    camera.ExecuteSoftwareTrigger();
                }

                while (camera.StreamGrabber.IsGrabbing)
                {
                    IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);

                    using (grabResult)
                    {
                        if (grabResult.GrabSucceeded)
                        {
                            if (PixelType.YUV422packed == grabResult.PixelTypeValue)
                            {
                                //converter.OutputPixelFormat = PixelType.BGRA8packed;
                            }
                            else if (PixelType.Mono8 == grabResult.PixelTypeValue)
                            {
                                Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);

                                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                                
                                converter.OutputPixelFormat = PixelType.Mono8;
                                IntPtr ptrBmp = bmpData.Scan0;
                                converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);

                                System.IntPtr srcPtr = bmpData.Scan0;
                                System.Runtime.InteropServices.Marshal.Copy(srcPtr, data, 0, len);

                                bitmap.UnlockBits(bmpData);
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            return -1; ;
                        }
                    }
                    camera.StreamGrabber.Stop();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }
            return 0;
        }

        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                IGrabResult grabResult = e.GrabResult;

                if (grabResult.GrabSucceeded)
                {
                    if (grabResult.PixelTypeValue == PixelType.Mono8)
                    {
                        byte[] buffer = grabResult.PixelData as byte[];

                        if (latestFrameAddress == IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(latestFrameAddress);
                            latestFrameAddress = IntPtr.Zero;
                            latestFrameAddress = Marshal.AllocHGlobal((Int32)(lWidth * lHeight));
                        }
                        
                        Marshal.Copy(buffer, 0, latestFrameAddress,(int)(lWidth * lHeight));
                    }
                    else if (grabResult.PixelTypeValue == PixelType.BayerRG8 || grabResult.PixelTypeValue == PixelType.BayerGB8
                          || grabResult.PixelTypeValue == PixelType.BayerBG8 || grabResult.PixelTypeValue == PixelType.BayerGR8
                          || grabResult.PixelTypeValue == PixelType.YUV422packed)
                    {
                        if (latestFrameAddress == IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(latestFrameAddress);
                            latestFrameAddress = IntPtr.Zero;
                            latestFrameAddress = Marshal.AllocHGlobal((Int32)(lWidth * lHeight));
                        }

                        converter.OutputPixelFormat = PixelType.BGR8packed;
                        converter.Convert(latestFrameAddress, 3 * (int)(lWidth * lHeight), grabResult);
                    }

                    if (bLineTrigger)
                    {
                        eventProcessImage((int)lWidth, (int)lHeight, latestFrameAddress);
                    }
                }
                else
                {
                    MessageBox.Show("Grab faild!\n" + grabResult.ErrorDescription, "Error", 0, 0);
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                e.DisposeGrabResultIfClone();
            }
        }

        public int SetExternTrigger()
        {
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }

                if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                    {
                        camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                        camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                        camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                        camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                        camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                    }
                    else
                    {
                        camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                        camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                        camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                    }
                }

                camera.Parameters[PLCamera.TriggerDelayAbs].SetValue(1);        // 触发延时1ms
                camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                camera.Parameters[PLCamera.LineDebouncerTimeAbs].SetValue(10000);       // 去抖延时10ms
                if (!bLineTrigger)
                {
                    try
                    { 
                        camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                    }
                    catch(Exception)
                    {
                    }
                    bLineTrigger = true;
                }
                camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);        
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }

            return 0;
        }

        public int SetSoftwareTrigger()
        {
            if (bSimulate)
            {
                return 0;
            }

            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }

                if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                    {
                        camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                        camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                        camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                        camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                        camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                    }
                    else
                    {
                        camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                        camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                        camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                    }
                }

                if( bLineTrigger )
                {
                    try
                    {
                        camera.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
                    }
                    catch (Exception)
                    {
                    }
                    bLineTrigger = false;
                }        
            }
            catch (Exception e)
            {
                ShowException(e);
                return 1;
            }
            return 0;
        }

        public void CloseCamera()
        {
            if (bSimulate)
            {
                return;
            }

            try
            {
                if (camera != null)
                {
                    camera.Close();
                    camera.Dispose();
                    camera = null;

                    if (latestFrameAddress != null)
                    {
                        Marshal.FreeHGlobal(latestFrameAddress);
                        latestFrameAddress = IntPtr.Zero;
                    }
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        public static int ReadData(MemoryStream curStream, int startPosition, int length)
        {
            int result = -1;

            byte[] tempData = new byte[length];
            curStream.Position = startPosition;
            curStream.Read(tempData, 0, length);
            result = BitConverter.ToInt32(tempData, 0);

            return result;
        }

        private void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", 0, 0);
        }

        private Bitmap CreateBitmap(byte[] originalImageData, int originalWidth, int originalHeight)
        {
            //指定8位格式，即256色
            Bitmap resultBitmap = new Bitmap(originalWidth, originalHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //将该位图存入内存中
            MemoryStream curImageStream = new MemoryStream();
            resultBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Bmp);
            curImageStream.Flush();

            //由于位图数据需要DWORD对齐（4byte倍数），计算需要补位的个数
            int curPadNum = ((originalWidth * 8 + 31) / 32 * 4) - originalWidth;

            //最终生成的位图数据大小
            int bitmapDataSize = ((originalWidth * 8 + 31) / 32 * 4) * originalHeight;

            //数据部分相对文件开始偏移，具体可以参考位图文件格式
            int dataOffset = ReadData(curImageStream, 10, 4);


            //改变调色板，因为默认的调色板是32位彩色的，需要修改为256色的调色板
            int paletteStart = 54;
            int paletteEnd = dataOffset;
            int color = 0;

            for (int i = paletteStart; i < paletteEnd; i += 4)
            {
                byte[] tempColor = new byte[4];
                tempColor[0] = (byte)color;
                tempColor[1] = (byte)color;
                tempColor[2] = (byte)color;
                tempColor[3] = (byte)0;
                color++;

                curImageStream.Position = i;
                curImageStream.Write(tempColor, 0, 4);
            }

            //最终生成的位图数据，以及大小，高度没有变，宽度需要调整
            byte[] destImageData = new byte[bitmapDataSize];
            int destWidth = originalWidth + curPadNum;

            //生成最终的位图数据，注意的是，位图数据 从左到右，从下到上，所以需要颠倒
            for (int originalRowIndex = originalHeight - 1; originalRowIndex >= 0; originalRowIndex--)
            {
                int destRowIndex = originalHeight - originalRowIndex - 1;

                for (int dataIndex = 0; dataIndex < originalWidth; dataIndex++)
                {
                    //同时还要注意，新的位图数据的宽度已经变化destWidth，否则会产生错位
                    destImageData[destRowIndex * destWidth + dataIndex] = originalImageData[originalRowIndex * originalWidth + dataIndex];
                }
            }

            //将流的Position移到数据段   
            curImageStream.Position = dataOffset;

            //将新位图数据写入内存中
            curImageStream.Write(destImageData, 0, bitmapDataSize);

            curImageStream.Flush();

            //将内存中的位图写入Bitmap对象
            resultBitmap = new Bitmap(curImageStream);

            return resultBitmap;
        }

        public void SaveBitmapImageIntoFile(Byte[] SaveImageBuf, int iWidth, int iHeight, string filePath)
        {
            /*
            Bitmap bitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr ptrBmp = bmpData.Scan0;
            System.IntPtr srcPtr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(SaveImageBuf,0, srcPtr, iWidth * iHeight);
            //System.Runtime.InteropServices.Marshal.Copy(srcPtr, SaveImageBuf, 0, iWidth* iHeight);
            bitmap.UnlockBits(bmpData);
            bitmap.Save(filePath);
            */

            Bitmap m_currBitmap = new Bitmap(iWidth, iHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            m_currBitmap = CreateBitmap(SaveImageBuf, iWidth, iHeight);
            m_currBitmap.Save(filePath);
        }

    }
}
