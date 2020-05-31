using System;
using System.IO;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Resources;
using System.Windows.Threading;

namespace IFactory.UI.Controls
{
    public class GifImage : UserControl
    {
        public static readonly DependencyProperty ForceGifAnimProperty = DependencyProperty.Register("ForceGifAnim", typeof(bool), typeof(GifImage), (PropertyMetadata)new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(GifImage), (PropertyMetadata)new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(GifImage.OnSourceChanged)));
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(GifImage), (PropertyMetadata)new FrameworkPropertyMetadata(Stretch.Fill, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(GifImage.OnStretchChanged)));
        public static readonly DependencyProperty StretchDirectionProperty = DependencyProperty.Register("StretchDirection", typeof(StretchDirection), typeof(GifImage), (PropertyMetadata)new FrameworkPropertyMetadata(StretchDirection.Both, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(GifImage.OnStretchDirectionChanged)));
        public static readonly RoutedEvent ImageFailedEvent = EventManager.RegisterRoutedEvent("ImageFailed", RoutingStrategy.Bubble, typeof(GifImage.ExceptionRoutedEventHandler), typeof(GifImage));
        private GifAnimation gifAnimation;
        private Image image;

        public bool ForceGifAnim
        {
            get
            {
                return (bool)this.GetValue(GifImage.ForceGifAnimProperty);
            }
            set
            {
                this.SetValue(GifImage.ForceGifAnimProperty, value);
            }
        }

        public string Source
        {
            get
            {
                return (string)this.GetValue(GifImage.SourceProperty);
            }
            set
            {
                this.SetValue(GifImage.SourceProperty, value);
            }
        }

        public Stretch Stretch
        {
            get
            {
                return (Stretch)this.GetValue(GifImage.StretchProperty);
            }
            set
            {
                this.SetValue(GifImage.StretchProperty, value);
            }
        }

        public StretchDirection StretchDirection
        {
            get
            {
                return (StretchDirection)this.GetValue(GifImage.StretchDirectionProperty);
            }
            set
            {
                this.SetValue(GifImage.StretchDirectionProperty, value);
            }
        }

        public event GifImage.ExceptionRoutedEventHandler ImageFailed
        {
            add
            {
                this.AddHandler(GifImage.ImageFailedEvent, (Delegate)value);
            }
            remove
            {
                this.RemoveHandler(GifImage.ImageFailedEvent, (Delegate)value);
            }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GifImage)d).CreateFromSourceString((string)e.NewValue);
        }

        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GifImage gifImage = (GifImage)d;
            Stretch stretch = (Stretch)e.NewValue;
            if (gifImage.gifAnimation != null)
            {
                gifImage.gifAnimation.Stretch = stretch;
            }
            else
            {
                if (gifImage.image == null)
                    return;
                gifImage.image.Stretch = stretch;
            }
        }

        private static void OnStretchDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GifImage gifImage = (GifImage)d;
            StretchDirection stretchDirection = (StretchDirection)e.NewValue;
            if (gifImage.gifAnimation != null)
            {
                gifImage.gifAnimation.StretchDirection = stretchDirection;
            }
            else
            {
                if (gifImage.image == null)
                    return;
                gifImage.image.StretchDirection = stretchDirection;
            }
        }

        private void image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.RaiseImageFailedEvent(e.ErrorException);
        }

        private void RaiseImageFailedEvent(Exception exp)
        {
            this.RaiseEvent((RoutedEventArgs)new GifImageExceptionRoutedEventArgs(GifImage.ImageFailedEvent, this)
            {
                ErrorException = exp
            });
        }

        private void DeletePreviousImage()
        {
            if (this.image != null)
            {
                this.RemoveLogicalChild(this.image);
                this.image = (Image)null;
            }
            if (this.gifAnimation == null)
                return;
            this.RemoveLogicalChild(this.gifAnimation);
            this.gifAnimation = (GifAnimation)null;
        }

        private void CreateNonGifAnimationImage()
        {
            this.image = new Image();
            this.image.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(this.image_ImageFailed);
            this.image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(this.Source);
            this.image.Stretch = this.Stretch;
            this.image.StretchDirection = this.StretchDirection;
            this.AddChild(this.image);
        }

        private void CreateGifAnimation(MemoryStream memoryStream)
        {
            this.gifAnimation = new GifAnimation();
            this.gifAnimation.CreateGifAnimation(memoryStream);
            this.gifAnimation.Stretch = this.Stretch;
            this.gifAnimation.StretchDirection = this.StretchDirection;
            this.AddChild(this.gifAnimation);
        }

        private void CreateFromSourceString(string source)
        {
            this.DeletePreviousImage();
            Uri uri;
            try
            {
                uri = new Uri(source, UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                this.RaiseImageFailedEvent(ex);
                return;
            }
            if (source.Trim().ToUpper().EndsWith(".GIF") || this.ForceGifAnim)
            {
                if (!uri.IsAbsoluteUri)
                {
                    this.GetGifStreamFromPack(uri);
                }
                else
                {
                    string leftPart = uri.GetLeftPart(UriPartial.Scheme);
                    if (leftPart == "http://" || leftPart == "ftp://" || leftPart == "file://")
                        this.GetGifStreamFromHttp(uri);
                    else if (leftPart == "pack://")
                        this.GetGifStreamFromPack(uri);
                    else
                        this.CreateNonGifAnimationImage();
                }
            }
            else
                this.CreateNonGifAnimationImage();
        }

        private void WebRequestFinished(MemoryStream memoryStream)
        {
            this.CreateGifAnimation(memoryStream);
        }

        private void WebRequestError(Exception exp)
        {
            this.RaiseImageFailedEvent(exp);
        }

        private void WebResponseCallback(IAsyncResult asyncResult)
        {
            WebReadState webReadState = (WebReadState)asyncResult.AsyncState;
            try
            {
                WebResponse response = webReadState.webRequest.EndGetResponse(asyncResult);
                webReadState.readStream = response.GetResponseStream();
                webReadState.buffer = new byte[100000];
                webReadState.readStream.BeginRead(webReadState.buffer, 0, webReadState.buffer.Length, new AsyncCallback(this.WebReadCallback), webReadState);
            }
            catch (WebException ex)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Render, (Delegate)new GifImage.WebRequestErrorDelegate(this.WebRequestError), ex);
            }
        }

        private void WebReadCallback(IAsyncResult asyncResult)
        {
            WebReadState webReadState = (WebReadState)asyncResult.AsyncState;
            int count = webReadState.readStream.EndRead(asyncResult);
            if (count > 0)
            {
                webReadState.memoryStream.Write(webReadState.buffer, 0, count);
                try
                {
                    webReadState.readStream.BeginRead(webReadState.buffer, 0, webReadState.buffer.Length, new AsyncCallback(this.WebReadCallback), webReadState);
                }
                catch (WebException ex)
                {
                    this.Dispatcher.Invoke(DispatcherPriority.Render, (Delegate)new GifImage.WebRequestErrorDelegate(this.WebRequestError), ex);
                }
            }
            else
                this.Dispatcher.Invoke(DispatcherPriority.Render, (Delegate)new GifImage.WebRequestFinishedDelegate(this.WebRequestFinished), webReadState.memoryStream);
        }

        private void GetGifStreamFromHttp(Uri uri)
        {
            try
            {
                WebReadState webReadState = new WebReadState();
                webReadState.memoryStream = new MemoryStream();
                webReadState.webRequest = WebRequest.Create(uri);
                webReadState.webRequest.Timeout = 10000;
                webReadState.webRequest.BeginGetResponse(new AsyncCallback(this.WebResponseCallback), webReadState);
            }
            catch (SecurityException ex)
            {
                this.CreateNonGifAnimationImage();
            }
        }

        private void ReadGifStreamSynch(Stream s)
        {
            MemoryStream memoryStream;
            using (s)
            {
                memoryStream = new MemoryStream((int)s.Length);
                byte[] buffer = new BinaryReader(s).ReadBytes((int)s.Length);
                memoryStream.Write(buffer, 0, (int)s.Length);
                memoryStream.Flush();
            }
            this.CreateGifAnimation(memoryStream);
        }

        private void GetGifStreamFromPack(Uri uri)
        {
            try
            {
                StreamResourceInfo streamResourceInfo = uri.IsAbsoluteUri ? (!uri.GetLeftPart(UriPartial.Authority).Contains("siteoforigin") ? Application.GetContentStream(uri) ?? Application.GetResourceStream(uri) : Application.GetRemoteStream(uri)) : Application.GetContentStream(uri) ?? Application.GetResourceStream(uri);
                if (streamResourceInfo == null)
                    throw new FileNotFoundException("Resource not found.", uri.ToString());
                this.ReadGifStreamSynch(streamResourceInfo.Stream);
            }
            catch (Exception ex)
            {
                this.RaiseImageFailedEvent(ex);
            }
        }

        public delegate void ExceptionRoutedEventHandler(object sender, GifImageExceptionRoutedEventArgs args);

        private delegate void WebRequestFinishedDelegate(MemoryStream memoryStream);

        private delegate void WebRequestErrorDelegate(Exception exp);
    }
}
