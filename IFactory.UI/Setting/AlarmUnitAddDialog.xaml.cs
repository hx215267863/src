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
    /// AlarmUnitAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmUnitAddDialog : Window, IComponentConnector
    {
        public AlarmUnitAddDialog()
        {
            InitializeComponent();
        }

        private UnitModel model;

        public int UnitDID { get; set; }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.model.UnitNO))
            {
                int num1 = (int)MessageBox.Show("请输入部件编号", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.UnitName))
            {
                int num2 = (int)MessageBox.Show("请输入部件名称", "提示");
            }
            else
            {
                UnitSaveResponse unitSaveResponse = LocalApi.Execute(new UnitSaveRequest() { DID = this.model.UnitDID, Name = this.model.UnitName, NO = this.model.UnitNO });
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
            if (this.UnitDID > 0)
            {
                this.model = LocalApi.Execute((new UnitGetRequest()
                {
                    UnitDID = this.UnitDID
                })).Unit;
                this.dialogTitle.Content = "修改部件";
            }
            else
            {
                this.dialogTitle.Content = "添加部件";
                this.model = new UnitModel();
            }
            this.DataContext = this.model;
        }
    }
}
