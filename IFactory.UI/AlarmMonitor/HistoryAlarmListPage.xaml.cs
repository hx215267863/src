using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Request.Alarm;
using IFactory.Platform.Common.Response.Alarm;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Data;

namespace IFactory.UI.AlarmMonitor
{
    /// <summary>
    /// HistoryAlarmListPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryAlarmListPage : BasePage, IComponentConnector
    {
        public HistoryAlarmListPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public IList<AlarmRecordItem> Rows { get; set; }

        public string Keyword { get; set; }

        public DateTime? AlarmDateStart { get; set; }

        public DateTime? AlarmDateEnd { get; set; }

        public DateTime? ChartDateStart { get; set; }

        public DateTime? ChartDateEnd { get; set; }

        public ICommand ViewDetailCommand { get; set; }

        static public int CraftDID { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<AlarmRecordItem>(new Action<AlarmRecordItem>(this.ViewDetail));
            this.RefreshData(new int?(1));
        }

        public void RefreshData(int? pageNumber = 1)
        {
            AlarmRecordListResponse recordListResponse = LocalApi.GetAlarmRecordList(new AlarmRecordListRequest()
            {
                Keyword = this.Keyword,
                AlarmDateStart = this.AlarmDateStart,
                AlarmDateEnd = this.AlarmDateEnd,
                PageNumber = pageNumber.Value,
                CraftsDid = CraftDID,
                PageSize = 15
            });
            this.pager.Setup(recordListResponse.AlarmRecords);
            this.dataGrid.ItemsSource = recordListResponse.AlarmRecords;
        }

        private void ViewDetail(AlarmRecordItem item)
        {
            this.NavigationService.Navigate(new HistoryAlarmDetailPage()
            {
                AlarmRecordDID = item.AlarmDid
            });
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData(new int?(e.PageNumber));
        }

        private void btnSearchList_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshData(new int?(1));
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string str = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.Filter = str;
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            if (saveFileDialog2.ShowDialog() != DialogResult.OK)
                return;
            AlarmRecordListResponse recordListResponse = LocalApi.GetAlarmRecordList(new AlarmRecordListRequest() { Keyword = this.Keyword, AlarmDateStart = this.AlarmDateStart, AlarmDateEnd = this.AlarmDateEnd, PageNumber = 1, PageSize = int.MaxValue });
            if (recordListResponse.IsError)
                return;
            PagedData<AlarmRecordItem> rows = recordListResponse.AlarmRecords;
            ExcelExport excelExport = new ExcelExport();
            DataTable table = ToDataTable(rows.ToList());

            excelExport.sheetName = "历史报警记录";

            excelExport.ExcuteExport(saveFileDialog2.FileName, table);
        }


        public static DataTable ToDataTable(List<AlarmRecordItem> lst)
        {
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "报警编号";
            column.Caption = "报警编号";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "报警时间";
            column.Caption = "报警时间";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "报警内容";
            column.Caption = "报警内容";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "恢复时间";
            column.Caption = "恢复时间";
            table.Columns.Add(column);



            for (int i = 0; i < lst.Count; i++)
            {
                row = table.NewRow();
                row["报警编号"] = lst[i].DID.ToString();
                row["报警时间"] = lst[i].AlarmTime.ToString();
                row["报警内容"] = lst[i].AlarmContent.ToString();
                row["恢复时间"] = lst[i].DisposeTime.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
