using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// StateImage.xaml 的交互逻辑
    /// </summary>
    public partial class StateImage : UserControl, IComponentConnector
    {
        public StateImage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ImagesSourceProperty = DependencyProperty.Register("Images", typeof(List<ImageSource>), typeof(StateImage), new FrameworkPropertyMetadata(new List<ImageSource>(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnImagesChanged)));
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(int), typeof(StateImage), new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnStateChanged)));

        public virtual List<ImageSource> Images
        {
            get
            {
                return (List<ImageSource>)this.GetValue(ImagesSourceProperty);
            }
            set
            {
                this.SetValue(ImagesSourceProperty, value);
            }
        }

        public virtual int State
        {
            get
            {
                return (int)this.GetValue(StateProperty);
            }
            set
            {
                this.SetValue(StateProperty, value);
            }
        }

        private static void OnImagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StateImage stateImage = (StateImage)d;
            List<ImageSource> imageSourceList = (List<ImageSource>)e.NewValue;
            if (imageSourceList.Count <= stateImage.State || stateImage.State < 0)
                return;
            stateImage.Background = new ImageBrush(imageSourceList[stateImage.State]);
        }

        private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StateImage stateImage = (StateImage)d;
            int index = (int)e.NewValue;
            if (stateImage.Images == null || stateImage.Images.Count <= index || index < 0)
                return;
            stateImage.Background = new ImageBrush(stateImage.Images[index]);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            if (this.Images == null || this.Images.Count <= this.State || this.State < 0)
                return;
            this.Background = new ImageBrush(this.Images[this.State]);
        }
    }
}
