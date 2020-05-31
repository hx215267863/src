using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace IFactory.Common.Utils
{
    public class ImageHelper
    {
        public static void MoveFile(string from, string to)
        {
            if (string.Compare(from, to, true) == 0)
                return;
            if (File.Exists(to))
                File.Delete(to);
            File.Move(from, to);
        }

        public static Size SaveImage(Stream sourceStream, string targetFileName)
        {
            using (Image source = Image.FromStream(sourceStream))
            {
                Size size = SaveThumbnailImage(source, targetFileName, source.Width, source.Height, ShearType.None);
                source.Dispose();
                return size;
            }
        }

        public static Size SaveImage(Image source, string targetFileName)
        {
            return SaveThumbnailImage(source, targetFileName, source.Width, source.Height, ShearType.None);
        }

        public static Size SaveThumbnailImage(Image source, string targetFileName, int targetWidth, int targetHeight, ShearType shearType = ShearType.None)
        {
            using (Image srcImg = SaveThumbnailImage(source, targetWidth, targetHeight, shearType))
            {
                Size size = new Size(srcImg.Width, srcImg.Height);
                Compress(srcImg, targetFileName, 80L);
                return size;
            }
        }

        public static Image SaveThumbnailImage(Image source)
        {
            return SaveThumbnailImage(source, source.Width, source.Height, ShearType.None);
        }

        public static Image SaveThumbnailImage(Image source, int targetWidth, int targetHeight, ShearType shearType = ShearType.None)
        {
            if (shearType == ShearType.None)
            {
                float num1 = targetWidth / (float)targetHeight;
                float num2 = source.Width / (float)source.Height;
                int width = source.Width;
                int height = source.Height;
                if (width > targetWidth || height > targetHeight)
                {
                    if (num2 > (double)num1)
                    {
                        width = targetWidth;
                        height = Convert.ToInt32(targetWidth / num2);
                    }
                    else
                    {
                        width = Convert.ToInt32(targetHeight * num2);
                        height = targetHeight;
                    }
                }
                Image image1 = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(image1);
                Color transparent = Color.Transparent;
                graphics.Clear(transparent);
                int num3 = 2;
                graphics.CompositingQuality = (CompositingQuality)num3;
                int num4 = 7;
                graphics.InterpolationMode = (InterpolationMode)num4;
                int num5 = 2;
                graphics.SmoothingMode = (SmoothingMode)num5;
                Image image2 = source;
                Rectangle destRect = new Rectangle(0, 0, image1.Width, image1.Height);
                Rectangle srcRect = new Rectangle(0, 0, source.Width, source.Height);
                int num6 = 2;
                graphics.DrawImage(image2, destRect, srcRect, (GraphicsUnit)num6);
                graphics.Dispose();
                return image1;
            }
            float num7 = source.Height / (float)targetHeight;
            float num8 = source.Width / (float)targetWidth;
            int x;
            int width1;
            int height1;
            int y;
            if (num8 < (double)num7)
            {
                x = 0;
                width1 = source.Width;
                height1 = (int)(num8 * (double)targetHeight);
                y = (source.Height - height1) / 2;
            }
            else
            {
                y = 0;
                height1 = source.Height;
                width1 = (int)(targetWidth * (double)num7);
                x = (source.Width - width1) / 2;
            }
            Image image3 = new Bitmap(targetWidth, targetHeight);
            Graphics graphics1 = Graphics.FromImage(image3);
            Color transparent1 = Color.Transparent;
            graphics1.Clear(transparent1);
            int num9 = 2;
            graphics1.CompositingQuality = (CompositingQuality)num9;
            int num10 = 7;
            graphics1.InterpolationMode = (InterpolationMode)num10;
            int num11 = 2;
            graphics1.SmoothingMode = (SmoothingMode)num11;
            Image image4 = source;
            Rectangle destRect1 = new Rectangle(0, 0, image3.Width, image3.Height);
            Rectangle srcRect1 = new Rectangle(x, y, width1, height1);
            int num12 = 2;
            graphics1.DrawImage(image4, destRect1, srcRect1, (GraphicsUnit)num12);
            graphics1.Dispose();
            return image3;
        }

        public static Size SaveThumbnailImage(Stream sourceStream, string targetFileName, int targetWidth, int targetHeight, ShearType shearType = ShearType.None)
        {
            using (Image source = Image.FromStream(sourceStream))
            {
                Size size = SaveThumbnailImage(source, targetFileName, targetWidth, targetHeight, shearType);
                source.Dispose();
                return size;
            }
        }

        public static Size SaveThumbnailImage(string sourceFileName, string targetFileName, int targetWidth, int targetHeight, ShearType shearType = ShearType.None)
        {
            using (Stream stream = File.OpenRead(sourceFileName))
            {
                Image source = Image.FromStream(stream);
                string targetFileName1 = targetFileName;
                int targetWidth1 = targetWidth;
                int targetHeight1 = targetHeight;
                int num = (int)shearType;
                Size size = SaveThumbnailImage(source, targetFileName1, targetWidth1, targetHeight1, (ShearType)num);
                source.Dispose();
                stream.Close();
                return size;
            }
        }

        public static byte[] ImageToBytes(Image image, ImageFormat format = null)
        {
            if (format == null)
                format = image.RawFormat;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                    image.Save(memoryStream, ImageFormat.Jpeg);
                else if (format.Equals(ImageFormat.Png))
                    image.Save(memoryStream, ImageFormat.Png);
                else if (format.Equals(ImageFormat.Bmp))
                    image.Save(memoryStream, ImageFormat.Bmp);
                else if (format.Equals(ImageFormat.Gif))
                    image.Save(memoryStream, ImageFormat.Gif);
                else if (format.Equals(ImageFormat.Icon))
                    image.Save(memoryStream, ImageFormat.Icon);
                byte[] buffer = new byte[memoryStream.Length];
                memoryStream.Seek(0L, SeekOrigin.Begin);
                memoryStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static Image BytesToImage(byte[] buffer)
        {
            return Image.FromStream(new MemoryStream(buffer));
        }

        public static Image RotateImage(Image image, int orientation)
        {
            if (orientation == 6)
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            else if (orientation == 8)
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            else if (orientation == 3)
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return image;
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int index = 0; index < imageEncoders.Length; ++index)
            {
                if (imageEncoders[index].MimeType == mimeType)
                    return imageEncoders[index];
            }
            return null;
        }

        public static void Compress(Bitmap srcBitmap, Stream destStream, long level)
        {
            ImageCodecInfo encoderInfo = GetEncoderInfo("image/jpeg");
            Encoder encoder = Encoder.Quality;
            EncoderParameters encoderParams = new EncoderParameters(1);
            long num = level;
            EncoderParameter encoderParameter = new EncoderParameter(encoder, num);
            encoderParams.Param[0] = encoderParameter;
            srcBitmap.Save(destStream, encoderInfo, encoderParams);
        }

        public static void Compress(Bitmap srcBitMap, string destFile, long level)
        {
            Stream destStream = new FileStream(destFile, FileMode.Create);
            Compress(srcBitMap, destStream, level);
            destStream.Close();
        }

        public static void Compress(Stream srcStream, string destFile, long level)
        {
            Bitmap srcBitMap = new Bitmap(srcStream);
            string destFile1 = destFile;
            long level1 = level;
            Compress(srcBitMap, destFile1, level1);
            srcBitMap.Dispose();
        }

        public static void Compress(Image srcImg, string destFile, long level)
        {
            Bitmap srcBitMap = new Bitmap(srcImg);
            string destFile1 = destFile;
            long level1 = level;
            Compress(srcBitMap, destFile1, level1);
            srcBitMap.Dispose();
        }

        public static void Compress(string srcFile, string destFile, long level)
        {
            Bitmap srcBitMap = new Bitmap(srcFile);
            string destFile1 = destFile;
            long level1 = level;
            Compress(srcBitMap, destFile1, level1);
            srcBitMap.Dispose();
        }
    }
}
