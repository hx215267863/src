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


namespace IFactory.UI.AlarmInfo
{
    public partial class AlarmInfoPage : BasePage, IComponentConnector
    {
        public AlarmInfoPage()
        {
            InitializeComponent();

            this.DataContext = this;
        }
        public IList<AlarmInfoModel> Rows { get; set; }

        public string Keyword { get; set; }

        public string ALARM_ID { get; set; }
        public DateTime? AlarmDateStart { get; set; }

        public DateTime? AlarmDateEnd { get; set; }

        public DateTime? ChartDateStart { get; set; }

        public DateTime? ChartDateEnd { get; set; }

        public ICommand ViewDetailCommand { get; set; }

        static public int CraftDID { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<AlarmInfoModel>(new Action<AlarmInfoModel>(this.ViewDetail));
            this.RefreshData(new int?(1));
        }

        public void RefreshData(int? pageNumber = 1)
        {
            AlarmInfoListResponse alarmInfoListResponse = LocalApi.GetAlarmInfoList(new AlarmInfoListRequest()
            {
                Keyword = this.Keyword,
                AlarmDateStart = this.AlarmDateStart,
                AlarmDateEnd = this.AlarmDateEnd,
                PageNumber = pageNumber.Value,
                CraftsDid = CraftDID,
                PageSize = 15
            });
            this.pager.Setup(alarmInfoListResponse.AlarmInfoModel);
            this.dataGrid.ItemsSource = alarmInfoListResponse.AlarmInfoModel;
        }

        private void ViewDetail(AlarmInfoModel item)
        {
            this.NavigationService.Navigate(new AlarmInfoPage()
            {
                ALARM_ID = item.ALARM_ID
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
            AlarmInfoListResponse infoListResponse = LocalApi.GetAlarmInfoList(new AlarmInfoListRequest() { Keyword = this.Keyword, AlarmDateStart = this.AlarmDateStart, AlarmDateEnd = this.AlarmDateEnd, PageNumber = 1, PageSize = int.MaxValue });
            if (infoListResponse.IsError)
                return;
            PagedData<AlarmInfoModel> rows = infoListResponse.AlarmInfoModel;
            ExcelExport excelExport = new ExcelExport();
            DataTable table = ToDataTable(rows.ToList());

            excelExport.sheetName = "新报警记录";

            excelExport.ExcuteExport(saveFileDialog2.FileName, table);
        }


        public static DataTable ToDataTable(List<AlarmInfoModel> lst)
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
            column.ColumnName = "产品型号";
            column.Caption = "产品型号";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "所属工艺";
            column.Caption = "所属工艺";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "操作员";
            column.Caption = "操作员";
            table.Columns.Add(column);



            for (int i = 0; i < lst.Count; i++)
            {
                row = table.NewRow();
                row["报警编号"] = lst[i].ALARM_ID.ToString();
                row["报警时间"] = lst[i].CRT_DT.ToString();
                row["报警内容"] = lst[i].ALARM_INFO.ToString();
                row["产品型号"] = lst[i].MODEL_CD.ToString();
                row["所属工艺"] = lst[i].ALARM_CRAFT.ToString();
                row["操作员"] = lst[i].OPER_CD.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
