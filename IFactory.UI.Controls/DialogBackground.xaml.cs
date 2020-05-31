using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace IFactory.UI.Controls
{
    /// <summary>
    /// DialogBackground.xaml 的交互逻辑
    /// </summary>
    public partial class DialogBackground : Grid, IComponentConnector
    {
        public DialogBackground()
        {
            InitializeComponent();
        }
    }
}
