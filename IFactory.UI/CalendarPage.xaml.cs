using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace IFactory.UI
{
    /// <summary>
    /// CalendarPage.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarPage : Page, IComponentConnector
    {
        public CalendarPage()
        {
            //用以初始化窗口控件
            InitializeComponent();
        }
    }
}
