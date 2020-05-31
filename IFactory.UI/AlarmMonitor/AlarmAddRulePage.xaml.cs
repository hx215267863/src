using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Request.Product;
using IFactory.Platform.Common.Response.Alarm;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Util;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// AlarmAddRulePage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmAddRulePage : BasePage, IComponentConnector
    {
        public AlarmAddRulePage()
        {
            InitializeComponent();
        }

        private Dictionary<string, TextBox> dicFields = new Dictionary<string, TextBox>();
        private string solutionImagePath;
        private string alarmLocationImagePath;
        private AlarmRuleModel model;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = new AlarmRuleModel();
            this.DataContext = this.model;
            AlarmFieldListResponse fieldListResponse = LocalApi.Execute(new AlarmFieldListRequest());
            for (int index = 0; index < fieldListResponse.AlarmFields.Count; ++index)
            {
                AlarmFieldModel alarmFieldModel = fieldListResponse.AlarmFields[index];
                this.formGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(60.0)
                });
                TextBlock textBlock = new TextBlock();
                textBlock.Text = alarmFieldModel.FieldDescription + "：";
                Grid.SetRow(textBlock, this.formGrid.RowDefinitions.Count - 2);
                Grid.SetColumn(textBlock, 0);
                TextBox textBox = new TextBox();
                textBox.Height = 36;
                textBox.Width = 320;

                Grid.SetRow(textBox, this.formGrid.RowDefinitions.Count - 2);
                Grid.SetColumn(textBox, 1);
                this.formGrid.Children.Add(textBlock);
                this.formGrid.Children.Add(textBox);
                formGrid.VerticalAlignment = VerticalAlignment.Center;
                formGrid.HorizontalAlignment = HorizontalAlignment.Left;

                this.dicFields.Add(alarmFieldModel.FieldName, textBox);
            }
            this.ddlCraft.ItemsSource = (LocalApi.GetCraftsList(new CraftListRequest())).Crafts.Where(m => AppContext.Current.CraftDIDs.Contains(m.CraftDID));
            this.ddlUnit.ItemsSource = (LocalApi.Execute(new UnitListRequest())).Units;
            this.ddlAlarmType.ItemsSource = (LocalApi.Execute(new AlarmTypeListRequest())).AlarmTypes;
        }

        private void btnSelectSolutionImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string str = "图片 (*.png;*.jpg)|*.png;*.jpg";
            openFileDialog1.Filter = str;
            OpenFileDialog openFileDialog2 = openFileDialog1;
            bool? nullable = openFileDialog2.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.solutionImagePath = openFileDialog2.FileName;
            this.imgSolutionImage.Source = new BitmapImage(new Uri(this.solutionImagePath, UriKind.Absolute));
        }

        private void btnSelectAlarmLocationImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string str = "图片 (*.png;*.jpg)|*.png;*.jpg";
            openFileDialog1.Filter = str;
            OpenFileDialog openFileDialog2 = openFileDialog1;
            bool? nullable = openFileDialog2.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.alarmLocationImagePath = openFileDialog2.FileName;
            this.imgAlarmLocationImage.Source = new BitmapImage(new Uri(this.alarmLocationImagePath, UriKind.Absolute));
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
        }

        private void Reset()
        {
            this.model = new AlarmRuleModel();
            this.DataContext = this.model;
            this.alarmLocationImagePath = null;
            this.solutionImagePath = null;
            this.imgAlarmLocationImage.Source = null;
            this.imgSolutionImage.Source = null;
            foreach (KeyValuePair<string, TextBox> dicField in this.dicFields)
                dicField.Value.Text = null;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.model.AlarmContent))
            {
                MessageBox.Show("报警内容不能为空", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.AlarmReason))
            {
                MessageBox.Show("异常原因不能为空", "提示");
            }
            else if (string.IsNullOrEmpty(this.model.SolutionText))
            {
                MessageBox.Show("解决方法不能为空", "提示");
            }
            else if (this.model.CraftDID == 0)
            {
                MessageBox.Show("请选择工艺", "提示");
            }
            else if (this.model.AlarmTypeDID == 0)
            {
                MessageBox.Show("请选择报警类型", "提示");
            }
            else if (string.IsNullOrEmpty(this.alarmLocationImagePath))
            {
                MessageBox.Show("请选择报警图片", "提示");
            }
            else if (string.IsNullOrEmpty(this.solutionImagePath))
            {
                MessageBox.Show("请选择报警处理图片", "提示");
            }
            else
            {
                AlarmRuleSaveRequest alarmRuleSaveRequest = new AlarmRuleSaveRequest()
                {   AlarmContent = this.model.AlarmContent,
                    AlarmReason = this.model.AlarmReason,
                    SolutionText = this.model.SolutionText,
                    AlarmRuleDID = this.model.RuleDID,
                    CraftDID = this.model.CraftDID,
                    UnitDID = this.model.UnitDID,
                    AlarmTypeDID = this.model.AlarmTypeDID
                };

                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (KeyValuePair<string, TextBox> dicField in this.dicFields)
                    dictionary.Add(dicField.Key, dicField.Value.Text);

                alarmRuleSaveRequest.Fields = dictionary;
                alarmRuleSaveRequest.GetFileParameters().Add("SolutionImage", new FileItem(this.solutionImagePath));
                alarmRuleSaveRequest.GetFileParameters().Add("AlarmLocationImage", new FileItem(this.alarmLocationImagePath));

                AlarmRuleSaveResponse ruleSaveResponse = LocalApi.Execute(alarmRuleSaveRequest);
                if (ruleSaveResponse.IsError)
                {
                    MessageBox.Show(ruleSaveResponse.ErrMsg, "提示");
                    this.btnSubmit.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("提交成功，报警编号：" + ruleSaveResponse.AlarmRuleDID, "提示");
                    this.Reset();
                }
            }
        }
    }
}
