using IFactory.Domain.Models;
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
    /// CraftProbably.xaml 的交互逻辑
    /// </summary>
    public partial class CraftProbably : Canvas, IComponentConnector
    {
        public CraftProbably()
        {
            InitializeComponent();
        }

        public void BindData(CraftProbablyModel craftProbablyModel)
        {
            this.txtBatteryBarCode.Text = craftProbablyModel.BatteryBarCode;
            this.txtNowYield.Text = craftProbablyModel.NowYield;
            this.txtDeviceName.Text = craftProbablyModel.DeviceName;
            this.txtTargetYield.Text = craftProbablyModel.TargetYield;
            this.txtUseName.Text = craftProbablyModel.UseName;
            this.txtPPM.Text = craftProbablyModel.PPM;
            this.txtOKCount.Text = craftProbablyModel.OKCount;
            this.txtOKRate.Text = craftProbablyModel.OKRate;
            this.txtCode.Text = craftProbablyModel.Code;
        }
    }
}
