using IFactory.UI.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// WorkCalendarPage.xaml 的交互逻辑
    /// </summary>
    public partial class WorkCalendarPage : BaseCraftIndexPage, IComponentConnector
    {
        public WorkCalendarPage()
        {
            InitializeComponent();
        }
    }
}
