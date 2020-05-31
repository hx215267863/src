using Microsoft.Expression.Media;
using Microsoft.Expression.Shapes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// StateCycle.xaml 的交互逻辑
    /// </summary>
    public partial class StateCycle : Canvas, IComponentConnector
    {
        public StateCycle()
        {
            InitializeComponent();
        }

        public static readonly Color SelectedOuterColor = Color.FromRgb(101, 101, 214);
        public static readonly Color SelectedInnerColor = Color.FromRgb(48, 48, 160);
        public static readonly Color SelectedTextColor = Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue);
        public static readonly DependencyProperty StateTextImageSourceProperty = DependencyProperty.Register("StateTextImageSource", typeof(ImageSource), typeof(StateCycle));
        private List<StateCycleButton> stateCycleButtons = new List<StateCycleButton>();
        private readonly int MinR = 270;
        private readonly int MaxR = 344;
        private readonly int TextR = 312;

        public class StateCycleItemClickEventArgs : EventArgs
        {
            public StateCycleItem StateCycleItem { get; set; }
        }

        public class StateCycleItem
        {
            public double StartAngle { get; set; }

            public double EndAngle { get; set; }

            public string Name { get; set; }

            public int State { get; set; }

            public object Tag { get; set; }

            public bool Disabled { get; set; }

            public string Text { get; set; }
        }

        private class StateCycleButton
        {
            public double StartAngle { get; set; }

            public double EndAngle { get; set; }

            public Arc OuterArc { get; set; }

            public Arc InnerArc { get; set; }

            public bool Selected { get; set; }

            public StateCycleItem StateCycleItem { get; set; }

            public TextBlock[] TextBlocks { get; set; }
        }

        public virtual ImageSource StateTextImageSource
        {
            get
            {
                return (ImageSource)GetValue(StateTextImageSourceProperty);
            }
            set
            {
                this.SetValue(StateTextImageSourceProperty, value);
            }
        }

        public event EventHandler<StateCycleItemClickEventArgs> ItemClick;

        public Color GetStateOuterColor(int state)
        {
            switch (state)
            {
                case 0:
                    return Color.FromRgb(0, byte.MaxValue, 0);
                case 1:
                    return Color.FromRgb(218, 251, byte.MaxValue);
                case 2:
                    //return Color.FromRgb(byte.MaxValue, 253, 0);
                    return Color.FromRgb(byte.MaxValue, 30, 48);
                default:
                    return Color.FromRgb(253, 30, 48);
            }
        }

        public Color GetStateInnerColor(int state)
        {
            switch (state)
            {
                case 0:
                    return Color.FromRgb(0, 217, 0);
                case 1:
                    return Color.FromRgb(166, 209, 217);
                case 2:
                    //return Color.FromRgb(217, 213, 0);
                    return Color.FromRgb(213, 22, 34);
                default:
                    return Color.FromRgb(213, 22, 34);
            }
        }

        public Color GetStateTextColor(int state)
        {
            return Color.FromRgb(0, 0, 0);
        }

        protected void RaiseItemClickEvent(StateCycleItemClickEventArgs args)
        {
            if (ItemClick == null)
                return;
            this.ItemClick(this, args);
        }

        private StateCycleButton GetEnterButton(MouseEventArgs e)
        {
            StateCycleButton stateCycleButton1 = null;
            Point point = new Point(this.canvas.Width / 2.0, this.canvas.Height / 2.0);
            Point position = e.GetPosition((IInputElement)this.canvas);
            double num1 = Math.Sqrt((position.X - point.X) * (position.X - point.X) + (position.Y - point.Y) * (position.Y - point.Y));
            if (num1 >= (double)this.MinR && num1 < (double)this.MaxR)
            {
                double num2 = position.X != point.X ? (position.Y != point.Y ? (Math.Atan2(position.Y - point.Y, position.X - point.X) * 180.0 / Math.PI + 90.0 + 360.0) % 360.0 : (position.X <= point.X ? 270.0 : 90.0)) : (position.Y <= point.Y ? 0.0 : 180.0);
                foreach (StateCycleButton stateCycleButton2 in stateCycleButtons)
                {
                    if ((num2 >= stateCycleButton2.StartAngle || num2 >= stateCycleButton2.StartAngle + 360.0) && (num2 < stateCycleButton2.EndAngle || stateCycleButton2.EndAngle < 0.0 && num2 < stateCycleButton2.EndAngle + 360.0))
                    {
                        stateCycleButton1 = stateCycleButton2;
                        break;
                    }
                }
            }
            return stateCycleButton1;
        }

        public void Setup(List<StateCycleItem> stateCycleItems)
        {
            this.stateCycleButtons = new List<StateCycleButton>();
            this.canvas.Children.Clear();
            double num1 = Math.PI * this.canvas.Width;
            foreach (StateCycleItem stateCycleItem in stateCycleItems)
            {
                Arc arc1 = new Arc();
                arc1.Fill = new SolidColorBrush(this.GetStateOuterColor(stateCycleItem.State));
                arc1.StartAngle = stateCycleItem.StartAngle;
                arc1.EndAngle = stateCycleItem.EndAngle;
                Canvas.SetLeft(arc1, 0.0);
                Canvas.SetTop(arc1, 0.0);
                arc1.Width = this.canvas.Width;
                arc1.Height = this.canvas.Height;
                arc1.ArcThickness = 54.2;
                arc1.StrokeThickness = 0.0;
                arc1.Stretch = Stretch.None;
                arc1.ArcThicknessUnit = UnitType.Pixel;
                this.canvas.Children.Add((UIElement)arc1);
                Arc arc2 = new Arc();
                arc2.Fill = new SolidColorBrush(this.GetStateInnerColor(stateCycleItem.State));
                arc2.StartAngle = stateCycleItem.StartAngle;
                arc2.EndAngle = stateCycleItem.EndAngle;
                SetLeft((UIElement)arc2, 53.2);
                SetTop((UIElement)arc2, 53.2);
                arc2.Width = this.canvas.Width - 106.4;
                arc2.Height = this.canvas.Height - 106.4;
                arc2.ArcThickness = 18.4;
                arc2.StrokeThickness = 0.0;
                arc2.Stretch = Stretch.None;
                arc2.ArcThicknessUnit = UnitType.Pixel;
                this.canvas.Children.Add((UIElement)arc2);
                StateCycleButton stateCycleButton = new StateCycleButton();
                stateCycleButton.StartAngle = stateCycleItem.StartAngle;
                stateCycleButton.EndAngle = stateCycleItem.EndAngle;
                stateCycleButton.OuterArc = arc1;
                stateCycleButton.InnerArc = arc2;
                stateCycleButton.StateCycleItem = stateCycleItem;
                this.stateCycleButtons.Add(stateCycleButton);
                if (!string.IsNullOrEmpty(stateCycleButton.StateCycleItem.Text))
                {
                    string text = stateCycleButton.StateCycleItem.Text;
                    TextBlock[] textBlockArray = new TextBlock[text.Length];
                    double[] numArray = new double[text.Length];
                    for (int index = 0; index < text.Length; ++index)
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = new string(text[index], 1);
                        textBlock.FontSize = 16;
                        textBlock.Style = this.FindResource("TextStyle") as Style;
                        textBlock.Foreground = (Brush)new SolidColorBrush(this.GetStateTextColor(stateCycleItem.State));
                        textBlock.RenderTransformOrigin = new Point(0.0, 0.0);
                        textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        textBlockArray[index] = textBlock;
                        numArray[index] = (int)text[index] < 19968 || (int)text[index] > 40891 ? textBlock.DesiredSize.Width * 1.1 : textBlock.DesiredSize.Width * 1.25;
                    }
                    double num2 = ((IEnumerable<double>)numArray).Sum() / num1 * 360.0;
                    double num3 = stateCycleItem.StartAngle + (stateCycleItem.EndAngle - stateCycleItem.StartAngle) / 2.0 - num2 / 2.0;
                    for (int count = 0; count < textBlockArray.Length; ++count)
                    {
                        TextBlock textBlock = textBlockArray[count];
                        TransformGroup transformGroup = new TransformGroup();
                        transformGroup.Children.Add(new TranslateTransform(-numArray[count] / 2.0, -textBlock.DesiredSize.Height / 2.0));
                        RotateTransform rotateTransform = new RotateTransform();
                        rotateTransform.CenterX = 0.0;
                        rotateTransform.CenterY = 0.0;
                        rotateTransform.Angle = num3 + ((IEnumerable<double>)numArray).Take<double>(count).Sum() / num1 * 360.0 + numArray[count] / num1 * 180.0;
                        transformGroup.Children.Add((Transform)rotateTransform);
                        Point point = this.GetPoint(new Point(TextR, TextR), TextR, rotateTransform.Angle);
                        transformGroup.Children.Add((Transform)new TranslateTransform(point.X, point.Y));
                        transformGroup.Children.Add((Transform)new TranslateTransform((double)(this.MaxR - this.TextR), (this.MaxR - this.TextR)));
                        textBlockArray[count].RenderTransform = transformGroup;
                        this.canvas.Children.Add((UIElement)textBlockArray[count]);
                    }
                    stateCycleButton.TextBlocks = textBlockArray;
                }
            }
            this.canvas.InvalidateArrange();
        }

        private Point GetPoint(Point center, double radius, double angle)
        {
            double num1 = Math.PI * (angle - 90.0) / 180.0;
            double num2 = radius * Math.Sin(num1);
            double num3 = radius * Math.Cos(num1);
            return new Point(center.X + num3, center.Y + num2);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            StateCycleButton enterButton = this.GetEnterButton(e);
            StateCycleButton stateCycleButton = this.stateCycleButtons.FirstOrDefault(m => m.Selected);
            if (enterButton != null)
            {
                if (!enterButton.StateCycleItem.Disabled)
                    this.Cursor = Cursors.Hand;
                enterButton.Selected = true;
                enterButton.OuterArc.Fill = new SolidColorBrush(SelectedOuterColor);
                enterButton.InnerArc.Fill = new SolidColorBrush(SelectedInnerColor);
                if (enterButton.TextBlocks != null)
                {
                    foreach (TextBlock textBlock in enterButton.TextBlocks)
                        textBlock.Foreground = new SolidColorBrush(SelectedTextColor);
                }
            }
            else
                this.Cursor = null;
            if (stateCycleButton == enterButton || stateCycleButton == null)
                return;
            stateCycleButton.Selected = false;
            stateCycleButton.OuterArc.Fill = new SolidColorBrush(this.GetStateOuterColor(stateCycleButton.StateCycleItem.State));
            stateCycleButton.InnerArc.Fill = new SolidColorBrush(this.GetStateInnerColor(stateCycleButton.StateCycleItem.State));
            if (stateCycleButton.TextBlocks == null)
                return;
            foreach (TextBlock textBlock in stateCycleButton.TextBlocks)
                textBlock.Foreground = new SolidColorBrush(this.GetStateTextColor(stateCycleButton.StateCycleItem.State));
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            StateCycle.StateCycleButton stateCycleButton = this.stateCycleButtons.FirstOrDefault(m => m.Selected);
            if (stateCycleButton == null)
                return;
            this.Cursor = null;
            stateCycleButton.Selected = false;
            stateCycleButton.OuterArc.Fill = new SolidColorBrush(this.GetStateOuterColor(stateCycleButton.StateCycleItem.State));
            stateCycleButton.InnerArc.Fill = new SolidColorBrush(this.GetStateInnerColor(stateCycleButton.StateCycleItem.State));
            if (stateCycleButton.TextBlocks == null)
                return;
            foreach (TextBlock textBlock in stateCycleButton.TextBlocks)
                textBlock.Foreground = new SolidColorBrush(GetStateTextColor(stateCycleButton.StateCycleItem.State));
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StateCycleButton enterButton = this.GetEnterButton(e);
            if (enterButton == null)
                return;
            this.RaiseItemClickEvent(new StateCycleItemClickEventArgs()
            {
                StateCycleItem = enterButton.StateCycleItem
            });
        }
    }
}
