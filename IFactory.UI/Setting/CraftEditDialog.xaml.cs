using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Product;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.Setting
{
    /// <summary>
    /// CraftEditDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CraftEditDialog : Window, IComponentConnector
    {
        public CraftEditDialog()
        {
            InitializeComponent();
        }

        private CraftDetailModel model;

        public int CraftDID { get; set; }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.model.CraftName))
            {
                MessageBox.Show("请输入工艺名称", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.TargetYield))
            {
                MessageBox.Show("请输入目标产量", "提示");
            }
            else
            {
                CraftDetailSaveResponse detailSaveResponse = LocalApi.Execute(new CraftDetailSaveRequest() { CraftDetail = this.model });
                this.DialogResult = new bool?(true);
                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this)) || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = (LocalApi.Execute(new CraftDetailGetRequest()
            {
                CraftDID = this.CraftDID
            })).CraftDetail;
            this.DataContext = this.model;
        }
    }
}
