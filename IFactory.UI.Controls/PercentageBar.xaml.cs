using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// PercentageBar.xaml 的交互逻辑
    /// </summary>
    public partial class PercentageBar : Canvas, IComponentConnector
    {
        public PercentageBar()
        {
            InitializeComponent();
        }

        public void UpdatePercentage(long number, long total)
        {
            if (total == 0L)
            {
                this.txtInfo.Content = (number.ToString() + " / " + total);
                this.txtInfo.Visibility = Visibility.Visible;
                this.percentageBody.Visibility = Visibility.Hidden;
            }
            else
            {
                this.txtInfo.Content = (number.ToString() + " / " + total);
                this.percentageBody.Width = (double)number / (double)total * this.Width;
                this.txtInfo.Visibility = Visibility.Visible;
                this.percentageBody.Visibility = Visibility.Visible;
            }
            this.InvalidateVisual();
        }
    }
}
