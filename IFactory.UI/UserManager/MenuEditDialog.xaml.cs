using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.User;
using IFactory.Platform.Common.Response.User;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace IFactory.UI.UserManager
{
    /// <summary>
    /// MenuEditDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MenuEditDialog : Window, IComponentConnector
    {
        public MenuEditDialog()
        {
            InitializeComponent();
        }

        private PermissionModel model;
        public int PermissionId { get; set; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            if (string.IsNullOrEmpty(this.model.PermissionName))
            {
                int num = (int)MessageBox.Show("请输入名称", "提示");
            }
            else
            {
                PermissionSaveResponse permissionSaveResponse = LocalApi.Execute(
                    new PermissionSaveRequest() {
                    PermissionId = this.model.PermissionId,
                    PermissonName = this.model.PermissionName,
                    Remark = this.model.Remark });
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

        private  void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = (LocalApi.Execute(new PermissionGetRequest() { PermissionId = this.PermissionId }
            )).Permission;
            this.DataContext = this.model;
        }
    }
}
