using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Client.Config;
using IFactory.Platform.Common.Request;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Response.Alarm;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// RealTimeAlarmDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeAlarmDetailPage : BasePage, IComponentConnector
    {
        public RealTimeAlarmDetailPage()
        {
            InitializeComponent();
        }

        public int AlarmTemporaryDID { get; set; }
        private AlarmTemporaryModel model;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AlarmTemporaryGetResponse temporaryGetResponse = LocalApi.Execute(new AlarmTemporaryGetRequest() { AlarmTemporaryDID = this.AlarmTemporaryDID });
            if (temporaryGetResponse.AlarmTemporary == null)
                return;
            this.model = temporaryGetResponse.AlarmTemporary;
            List<FieldRow> fieldRowList1 = new List<FieldRow>();
            List<FieldRow> fieldRowList2 = fieldRowList1;
            FieldRow fieldRow1 = new FieldRow();
            fieldRow1.FieldName = "报警编号";
            string string1 = this.model.RuleDID.ToString();
            fieldRow1.FieldValue = string1;
            fieldRowList2.Add(fieldRow1);
            List<FieldRow> fieldRowList3 = fieldRowList1;
            FieldRow fieldRow2 = new FieldRow();
            fieldRow2.FieldName = "报警时间";
            string string2 = this.model.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss");
            fieldRow2.FieldValue = string2;
            fieldRowList3.Add(fieldRow2);
            List<FieldRow> fieldRowList4 = fieldRowList1;
            FieldRow fieldRow3 = new FieldRow();
            fieldRow3.FieldName = "报警内容";
            string alarmContent = this.model.AlarmContent;
            fieldRow3.FieldValue = alarmContent;
            fieldRowList4.Add(fieldRow3);
            List<FieldRow> fieldRowList5 = fieldRowList1;
            FieldRow fieldRow4 = new FieldRow();
            fieldRow4.FieldName = "报警部件";
            string unitName = this.model.UnitName;
            fieldRow4.FieldValue = unitName;
            fieldRowList5.Add(fieldRow4);
            List<FieldRow> fieldRowList6 = fieldRowList1;
            FieldRow fieldRow5 = new FieldRow();
            fieldRow5.FieldName = "所在工位";
            string facilityName = this.model.FacilityName;
            fieldRow5.FieldValue = facilityName;
            fieldRowList6.Add(fieldRow5);
            fieldRowList1.Add(new FieldRow()
            {
                FieldName = "报警类型",
                FieldValue = this.model.AlarmTypeName
            });
            List<FieldRow> fieldRowList7 = fieldRowList1;
            FieldRow fieldRow6 = new FieldRow();
            fieldRow6.FieldName = "异常原因";
            string alarmReason = this.model.AlarmReason;
            fieldRow6.FieldValue = alarmReason;
            fieldRowList7.Add(fieldRow6);
            List<FieldRow> fieldRowList8 = fieldRowList1;
            FieldRow fieldRow7 = new FieldRow();
            fieldRow7.FieldName = "解决方案";
            string solutionText = this.model.SolutionText;
            fieldRow7.FieldValue = solutionText;
            fieldRowList8.Add(fieldRow7);
            List<FieldRow> fieldRowList9 = fieldRowList1;
            FieldRow fieldRow8 = new FieldRow();
            fieldRow8.FieldName = "报警地址";
            string address = this.model.Address;
            fieldRow8.FieldValue = address;
            fieldRowList9.Add(fieldRow8);
            foreach (AlarmFieldValue fieldValue in this.model.FieldValues)
                fieldRowList1.Add(new FieldRow()
                {
                    FieldName = fieldValue.FieldDescription,
                    FieldValue = fieldValue.FieldValue
                });
            //string urlstring = "C:/" + this.model.AlarmLocationImagePath;
            string urlstring = System.Environment.CurrentDirectory + this.model.AlarmLocationImagePath; 
            if (System.IO.File.Exists(urlstring))
                this.imgAlarmLocation.Source = new BitmapImage(new Uri(urlstring, UriKind.Absolute));
            else
                urlstring = urlstring = System.Environment.CurrentDirectory + "/ErrorPic/Error.jpg"; 
            this.imgAlarmLocation.Source = new BitmapImage(new Uri(urlstring, UriKind.Absolute));
            this.dataGrid.ItemsSource = fieldRowList1;
        }

        private void btnHandle_Click(object sender, RoutedEventArgs e)
        {
            AlarmTemporaryUpdateHandledResponse updateHandledResponse = LocalApi.Execute(new AlarmTemporaryUpdateHandledRequest() { AlarmTemporaryDID = this.AlarmTemporaryDID, HandlerId = AppContext.Current.UserId });
            if (updateHandledResponse.IsError)
            {
                int num1 = (int)MessageBox.Show(updateHandledResponse.ErrMsg, "提示");
            }
            else
            {
                int num2 = (int)MessageBox.Show("提交成功", "提示");
                this.NavigationService.GoBack();
            }
        }

        private void btnShowSolutionImage_Click(object sender, RoutedEventArgs e)
        {
            if (this.model == null)
                return;
            //string urlstring = "C:/" + this.model.SolutionImagePath;
            string urlstring = System.Environment.CurrentDirectory + this.model.AlarmLocationImagePath;
            if (!System.IO.File.Exists(urlstring))
                //urlstring = "C:/Pictures/11.png";
                urlstring = System.Environment.CurrentDirectory + "/ErrorPic/Error.jpg";
            new ShowPictureWindow()
            {
                ImageUri = new Uri(urlstring, UriKind.Absolute)
            }.ShowDialog();
        }
    }
}
