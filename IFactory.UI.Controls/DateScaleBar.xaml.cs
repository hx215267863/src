using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// DateScaleBar.xaml 的交互逻辑
    /// </summary>
    public partial class DateScaleBar : Canvas, IComponentConnector
    {
        public DateScaleBar()
        {
            InitializeComponent();

            this.UpdateStartTime(DateTime.Today.AddDays(-1.0));
            for (int index = 0; index < 52; ++index)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                Rectangle rectangle = new Rectangle();
                Grid.SetColumn((UIElement)rectangle, index);
                Grid.SetRow((UIElement)rectangle, 0);
                rectangle.Style = this.FindResource("ScaleStyle") as Style;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (index % 24 + 1).ToString();
                Grid.SetColumn((UIElement)textBlock, index);
                Grid.SetRow((UIElement)textBlock, 1);
                textBlock.Style = this.FindResource("ScaleValueStyle") as Style;
                this.ruler.ColumnDefinitions.Add(columnDefinition);
                this.ruler.Children.Add((UIElement)rectangle);
                this.ruler.Children.Add((UIElement)textBlock);
            }
        }

        private Point? mouseDown;
        private double sourcePosition;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                this.selectedDate = value;
                if (this.selectedDate < this.startDate)
                    this.UpdateStartTime(this.selectedDate.Date.AddDays(-1.0));
                if (this.selectedDate > this.endDate)
                    this.UpdateStartTime(this.selectedDate.Date.AddDays(-1.0));
                this.btnSelectDate.Visibility = Visibility.Visible;
                Canvas.SetLeft((UIElement)this.btnSelectDate, (this.selectedDate - this.startDate).Ticks / (double)(this.endDate - this.startDate).Ticks * this.bar.Width + Canvas.GetLeft((UIElement)this.bar) - this.btnSelectDate.Width / 2.0);
                this.RaiseSelectedDateChangedEvent(new DateScaleBar.SelectedDateChangedEventArgs()
                {
                    SelectedDate = this.selectedDate
                });
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }
        }

        public event EventHandler<DateScaleBar.SelectedDateChangedEventArgs> SelectedDateChanged;

        public event EventHandler<DateScaleBar.DateRangeChangedEventArgs> DateRangeChanged;
        

        protected void RaiseSelectedDateChangedEvent(DateScaleBar.SelectedDateChangedEventArgs args)
        {
            this.selectedDate = args.SelectedDate;
            // ISSUE: reference to a compiler-generated field
            if (this.SelectedDateChanged == null)
                return;
            // ISSUE: reference to a compiler-generated field
            this.SelectedDateChanged(this, args);
        }

        protected void RaiseDateRangeChangedEvent(DateScaleBar.DateRangeChangedEventArgs args)
        {
            // ISSUE: reference to a compiler-generated field
            if (this.DateRangeChanged == null)
                return;
            // ISSUE: reference to a compiler-generated field
            this.DateRangeChanged(this, args);
        }

        public void SetDates(IList<DateTime> dates)
        {
            this.bar.Children.Clear();
            foreach (DateTime date in (IEnumerable<DateTime>)dates)
            {
                Button button = new Button();
                button.Style = this.FindResource("DateButtonStyle") as Style;
                double length = (double)(date - this.startDate).Ticks / (double)(this.endDate - this.startDate).Ticks * this.bar.Width - button.Width / 2.0;
                Canvas.SetLeft((UIElement)button, length);
                button.Tag = date;
                button.ToolTip = date.ToString("yyyy-MM-dd HH:mm:ss");
                button.Click += new RoutedEventHandler(this.BtnDate_Click);
                this.bar.Children.Add((UIElement)button);
            }
        }

        private void BtnDate_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedDate = (DateTime)((FrameworkElement)sender).Tag;
        }

        private void btnSelectDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.mouseDown = new Point?(e.GetPosition((IInputElement)this));
            this.sourcePosition = this.GetCurrentPosition();
        }

        private void bar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.mouseDown.HasValue)
                return;
            double x = e.GetPosition((IInputElement)this.bar).X;
            Canvas.SetLeft((UIElement)this.btnSelectDate, x + Canvas.GetLeft((UIElement)this.bar) - this.btnSelectDate.Width / 2.0);
            DateTime date = this.ToDate(x);
            this.RaiseSelectedDateChangedEvent(new DateScaleBar.SelectedDateChangedEventArgs()
            {
                SelectedDate = date
            });
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseDown.HasValue)
                return;
            double num = this.sourcePosition + (e.GetPosition((IInputElement)this).X - this.mouseDown.Value.X);
            if (num < 0.0)
                num = 0.0;
            else if (num > this.bar.Width)
                num = this.bar.Width;
            Canvas.SetLeft((UIElement)this.btnSelectDate, num + Canvas.GetLeft((UIElement)this.bar) - this.btnSelectDate.Width / 2.0);
        }

        private double GetCurrentPosition()
        {
            return Canvas.GetLeft((UIElement)this.btnSelectDate) + this.btnSelectDate.Width / 2.0 - Canvas.GetLeft((UIElement)this.bar);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.mouseDown.HasValue)
            {
                double position = this.sourcePosition + (e.GetPosition((IInputElement)this).X - this.mouseDown.Value.X);
                if (position < 0.0)
                    position = 0.0;
                else if (position > this.bar.Width)
                    position = this.bar.Width;
                Canvas.SetLeft((UIElement)this.btnSelectDate, position + Canvas.GetLeft((UIElement)this.bar) - this.btnSelectDate.Width / 2.0);
                DateTime date = this.ToDate(position);
                this.RaiseSelectedDateChangedEvent(new DateScaleBar.SelectedDateChangedEventArgs()
                {
                    SelectedDate = date
                });
            }
            else
            {
                Point position1 = e.GetPosition((IInputElement)this);
                Rect rect = new Rect(new Point(Canvas.GetLeft((UIElement)this.bar), Canvas.GetTop((UIElement)this.bar)), new Size(this.bar.Width, this.bar.Height));
                if (rect.Contains(position1))
                {
                    double position2 = position1.X - rect.X;
                    Canvas.SetLeft((UIElement)this.btnSelectDate, position2 + Canvas.GetLeft((UIElement)this.bar) - this.btnSelectDate.Width / 2.0);
                    this.btnSelectDate.Visibility = Visibility.Visible;
                    DateTime date = this.ToDate(position2);
                    this.RaiseSelectedDateChangedEvent(new DateScaleBar.SelectedDateChangedEventArgs()
                    {
                        SelectedDate = date
                    });
                }
            }
            this.mouseDown = new Point?();
        }

        private void UpdateStartTime(DateTime startTime)
        {
            this.startDate = startTime;
            this.endDate = startTime.AddDays(2.0).AddHours(4.0);
            this.txtDate1.Content = startTime.ToString("MM-dd");
            this.txtDate2.Content = startTime.AddDays(1.0).ToString("MM-dd");
            this.txtDate3.Content = startTime.AddDays(2.0).ToString("MM-dd");
            if (this.selectedDate < this.startDate || this.selectedDate > this.endDate)
            {
                this.btnSelectDate.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.btnSelectDate.Visibility = Visibility.Visible;
                Canvas.SetLeft((UIElement)this.btnSelectDate, (double)(this.selectedDate - this.startDate).Ticks / (double)(this.endDate - this.startDate).Ticks * this.bar.Width + Canvas.GetLeft((UIElement)this.bar) - this.btnSelectDate.Width / 2.0);
            }
            this.bar.Children.Clear();
            this.RaiseDateRangeChangedEvent(new DateScaleBar.DateRangeChangedEventArgs()
            {
                StartDate = this.startDate,
                EndDate = this.endDate
            });
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateStartTime(this.startDate.AddDays(-1.0));
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateStartTime(this.startDate.AddDays(1.0));
        }

        private DateTime ToDate(double position)
        {
            return this.startDate.AddTicks((long)((double)(this.endDate - this.startDate).Ticks * position / this.bar.Width));
        }

        public class SelectedDateChangedEventArgs : EventArgs
        {
            public DateTime SelectedDate { get; set; }
        }

        public class DateRangeChangedEventArgs : EventArgs
        {
            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }
        }
    }
}
