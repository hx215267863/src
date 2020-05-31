using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace IFactory.UI.Controls
{
    internal class GifAnimation : Viewbox
    {
        private int numberOfLoops = -1;
        private Canvas canvas;
        private List<GifAnimation.GifFrame> frameList;
        private int frameCounter;
        private int numberOfFrames;
        private int currentLoop;
        private int logicalWidth;
        private int logicalHeight;
        private DispatcherTimer frameTimer;
        private GifAnimation.GifFrame currentParseGifFrame;

        public GifAnimation()
        {
            this.canvas = new Canvas();
            this.Child = (UIElement)this.canvas;
        }

        private void Reset()
        {
            if (this.frameList != null)
                this.frameList.Clear();
            this.frameList = (List<GifAnimation.GifFrame>)null;
            this.frameCounter = 0;
            this.numberOfFrames = 0;
            this.numberOfLoops = -1;
            this.currentLoop = 0;
            this.logicalWidth = 0;
            this.logicalHeight = 0;
            if (this.frameTimer == null)
                return;
            this.frameTimer.Stop();
            this.frameTimer = (DispatcherTimer)null;
        }

        private void ParseGif(byte[] gifData)
        {
            this.frameList = new List<GifAnimation.GifFrame>();
            this.currentParseGifFrame = new GifAnimation.GifFrame();
            this.ParseGifDataStream(gifData, 0);
        }

        private int ParseBlock(byte[] gifData, int offset)
        {
            switch (gifData[offset])
            {
                case 33:
                    if ((int)gifData[offset + 1] == 249)
                        return this.ParseGraphicControlExtension(gifData, offset);
                    return this.ParseExtensionBlock(gifData, offset);
                case 44:
                    offset = this.ParseGraphicBlock(gifData, offset);
                    this.frameList.Add(this.currentParseGifFrame);
                    this.currentParseGifFrame = new GifAnimation.GifFrame();
                    return offset;
                case 59:
                    return -1;
                default:
                    throw new Exception("GIF format incorrect: missing graphic block or special-purpose block. ");
            }
        }

        private int ParseGraphicControlExtension(byte[] gifData, int offset)
        {
            int num = (int)gifData[offset + 2];
            int index = offset + num + 2 + 1;
            this.currentParseGifFrame.disposalMethod = ((int)gifData[offset + 3] & 28) >> 2;
            this.currentParseGifFrame.delayTime = (int)BitConverter.ToUInt16(gifData, offset + 4);
            while ((int)gifData[index] != 0)
                index = index + (int)gifData[index] + 1;
            return index + 1;
        }

        private int ParseLogicalScreen(byte[] gifData, int offset)
        {
            this.logicalWidth = (int)BitConverter.ToUInt16(gifData, offset);
            this.logicalHeight = (int)BitConverter.ToUInt16(gifData, offset + 2);
            byte num1 = gifData[offset + 4];
            int num2 = ((int)num1 & 128) > 0 ? 1 : 0;
            int num3 = offset + 7;
            if (num2 != 0)
            {
                int num4 = (int)Math.Pow(2.0, (double)(((int)num1 & 7) + 1)) * 3;
                num3 += num4;
            }
            return num3;
        }

        private int ParseGraphicBlock(byte[] gifData, int offset)
        {
            this.currentParseGifFrame.left = (int)BitConverter.ToUInt16(gifData, offset + 1);
            this.currentParseGifFrame.top = (int)BitConverter.ToUInt16(gifData, offset + 3);
            this.currentParseGifFrame.width = (int)BitConverter.ToUInt16(gifData, offset + 5);
            this.currentParseGifFrame.height = (int)BitConverter.ToUInt16(gifData, offset + 7);
            if (this.currentParseGifFrame.width > this.logicalWidth)
                this.logicalWidth = this.currentParseGifFrame.width;
            if (this.currentParseGifFrame.height > this.logicalHeight)
                this.logicalHeight = this.currentParseGifFrame.height;
            byte num1 = gifData[offset + 9];
            int num2 = ((int)num1 & 128) > 0 ? 1 : 0;
            int num3 = offset + 9;
            if (num2 != 0)
            {
                int num4 = (int)Math.Pow(2.0, (double)(((int)num1 & 7) + 1)) * 3;
                num3 += num4;
            }
            int index;
            for (index = num3 + 1 + 1; (int)gifData[index] != 0; index = index + (int)gifData[index] + 1)
            {
                int num4 = (int)gifData[index];
            }
            return index + 1;
        }

        private int ParseExtensionBlock(byte[] gifData, int offset)
        {
            int num = (int)gifData[offset + 2];
            int index = offset + num + 2 + 1;
            if ((int)gifData[offset + 1] == (int)byte.MaxValue && num > 10 && Encoding.ASCII.GetString(gifData, offset + 3, 8) == "NETSCAPE")
            {
                this.numberOfLoops = (int)BitConverter.ToUInt16(gifData, offset + 16);
                if (this.numberOfLoops > 0)
                    this.numberOfLoops = this.numberOfLoops + 1;
            }
            while ((int)gifData[index] != 0)
                index = index + (int)gifData[index] + 1;
            return index + 1;
        }

        private int ParseHeader(byte[] gifData, int offset)
        {
            if (Encoding.ASCII.GetString(gifData, offset, 3) != "GIF")
                throw new Exception("Not a proper GIF file: missing GIF header");
            return 6;
        }

        private void ParseGifDataStream(byte[] gifData, int offset)
        {
            offset = this.ParseHeader(gifData, offset);
            offset = this.ParseLogicalScreen(gifData, offset);
            while (offset != -1)
                offset = this.ParseBlock(gifData, offset);
        }

        public void CreateGifAnimation(MemoryStream memoryStream)
        {
            this.Reset();
            byte[] buffer = memoryStream.GetBuffer();
            GifBitmapDecoder gifBitmapDecoder = new GifBitmapDecoder((Stream)memoryStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            this.numberOfFrames = gifBitmapDecoder.Frames.Count;
            try
            {
                this.ParseGif(buffer);
            }
            catch
            {
                throw new FileFormatException("Unable to parse Gif file format.");
            }
            for (int index = 0; index < gifBitmapDecoder.Frames.Count; ++index)
            {
                this.frameList[index].Source = (ImageSource)gifBitmapDecoder.Frames[index];
                this.frameList[index].Visibility = Visibility.Hidden;
                this.canvas.Children.Add((UIElement)this.frameList[index]);
                Canvas.SetLeft((UIElement)this.frameList[index], (double)this.frameList[index].left);
                Canvas.SetTop((UIElement)this.frameList[index], (double)this.frameList[index].top);
                Panel.SetZIndex((UIElement)this.frameList[index], index);
            }
            this.canvas.Height = (double)this.logicalHeight;
            this.canvas.Width = (double)this.logicalWidth;
            this.frameList[0].Visibility = Visibility.Visible;
            for (int index = 0; index < this.frameList.Count; ++index)
                Console.WriteLine(this.frameList[index].disposalMethod.ToString() + " " + this.frameList[index].width.ToString() + " " + this.frameList[index].delayTime.ToString());
            if (this.frameList.Count <= 1)
                return;
            if (this.numberOfLoops == -1)
                this.numberOfLoops = 1;
            this.frameTimer = new DispatcherTimer();
            this.frameTimer.Tick += new EventHandler(this.NextFrame);
            this.frameTimer.Interval = new TimeSpan(0, 0, 0, 0, this.frameList[0].delayTime * 10);
            this.frameTimer.Start();
        }

        public void NextFrame()
        {
            this.NextFrame(null, (EventArgs)null);
        }

        public void NextFrame(object sender, EventArgs e)
        {
            this.frameTimer.Stop();
            if (this.numberOfFrames == 0)
                return;
            if (this.frameList[this.frameCounter].disposalMethod == 2)
                this.frameList[this.frameCounter].Visibility = Visibility.Hidden;
            if (this.frameList[this.frameCounter].disposalMethod >= 3)
                this.frameList[this.frameCounter].Visibility = Visibility.Hidden;
            this.frameCounter = this.frameCounter + 1;
            if (this.frameCounter < this.numberOfFrames)
            {
                this.frameList[this.frameCounter].Visibility = Visibility.Visible;
                this.frameTimer.Interval = new TimeSpan(0, 0, 0, 0, this.frameList[this.frameCounter].delayTime * 10);
                this.frameTimer.Start();
            }
            else
            {
                if (this.numberOfLoops != 0)
                    this.currentLoop = this.currentLoop + 1;
                if (this.currentLoop >= this.numberOfLoops && this.numberOfLoops != 0)
                    return;
                for (int index = 0; index < this.frameList.Count; ++index)
                    this.frameList[index].Visibility = Visibility.Hidden;
                this.frameCounter = 0;
                this.frameList[this.frameCounter].Visibility = Visibility.Visible;
                this.frameTimer.Interval = new TimeSpan(0, 0, 0, 0, this.frameList[this.frameCounter].delayTime * 10);
                this.frameTimer.Start();
            }
        }

        private class GifFrame : Image
        {
            public int delayTime;
            public int disposalMethod;
            public int left;
            public int top;
            public int width;
            public int height;
        }
    }
}
