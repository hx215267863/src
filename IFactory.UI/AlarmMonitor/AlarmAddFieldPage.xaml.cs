using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Response.Alarm;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// AlarmAddFieldPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmAddFieldPage : BasePage, IComponentConnector
    {
        public AlarmAddFieldPage()
        {
            InitializeComponent();

            this.viewModel = new AlarmFieldViewModel();
            this.DataContext = this.viewModel;
        }

        public class AlarmFieldViewModel
        {
            public string FieldName { get; set; }

            public string FieldDescription { get; set; }
        }

        private AlarmAddFieldPage.AlarmFieldViewModel viewModel;

        private void Reset()
        {
            this.viewModel = new AlarmFieldViewModel();
            this.DataContext = this.viewModel;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.viewModel.FieldName))
            {
                MessageBox.Show("字段名称不能为空");
            }
            else if (!new Regex("^[a-zA-Z][a-zA-Z0-9_]*$").IsMatch(this.viewModel.FieldName))
            {
                MessageBox.Show("字段名称必须由字母、数字、_组成");
            }
            else if (string.IsNullOrEmpty(this.viewModel.FieldDescription))
            {
                MessageBox.Show("字段说明不能为空");
            }
            else
            {
                AlarmFieldSaveResponse fieldSaveResponse = LocalApi.Execute(new AlarmFieldSaveRequest() {
                    FieldDescription = this.viewModel.FieldDescription,
                    FieldName = this.viewModel.FieldName });

                if (fieldSaveResponse.IsError)
                {
                    int num4 = (int)MessageBox.Show(fieldSaveResponse.ErrMsg, "提示");
                }
                else
                {
                    int num5 = (int)MessageBox.Show("提交成功", "提示");
                    this.Reset();
                }
            }
        }
    }
}
