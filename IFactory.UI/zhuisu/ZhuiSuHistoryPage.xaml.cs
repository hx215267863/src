using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IFactory.UI.Controls;
using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Request.Product;
using System.Windows.Markup;
using System.Windows.Forms;
using System.Data;

namespace IFactory.UI.zhuisu
{
    /// <summary>
    /// ZhuiSuHistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class ZhuiSuHistoryPage : BasePage, IComponentConnector
    {
        public ZhuiSuHistoryPage()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public int IdneDID { get; set; }

        public int? ProcessDID { get; set; }

        public string Keyword { get; set; }
        public ICommand ViewDetailCommand { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<ZhuiSuItem>(new Action<ZhuiSuItem>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            ZhuiSuResponse zhuisuResponse = LocalApi.ExecuteHistory(new ZhuiSuRequest()
            {
                Keyword = this.Keyword,
                ProcessDID = this.ProcessDID,
                PageNumber = this.pager.PageNumber,
                //PageNumber = pageNumber.Value,
                TimeStart = this.TimeStart,
                TimeEnd = this.TimeEnd,
                PageSize = 10
            });
            this.pager.Setup(zhuisuResponse.ZhuiSus);
            this.dataGrid.ItemsSource = zhuisuResponse.ZhuiSus;
        }

        private void ViewDetail(ZhuiSuItem item)
        {
            this.NavigationService.Navigate(new ZhuiSuHistoryPage()
            {
                IdneDID = item.Iden
            });
        }

        private void pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string str = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.Filter = str;
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            if (saveFileDialog2.ShowDialog() != DialogResult.OK)
                return;
            ZhuiSuResponse zhuisuResponse = LocalApi.ExecuteHistory(new ZhuiSuRequest()
            {
                ProcessDID = this.ProcessDID,
                //PageNumber = this.pager.PageNumber,
                TimeStart = this.TimeStart,
                TimeEnd = this.TimeEnd,
                PageNumber = 1,
                PageSize = int.MaxValue
            });
            if (zhuisuResponse.IsError)
                return;
            PagedData<ZhuiSuItem> rows = zhuisuResponse.ZhuiSus;
            ExcelExport excelExport = new ExcelExport();
            DataTable table = ToDataTable(rows.ToList());

            excelExport.sheetName = "历史追溯记录";

            excelExport.ExcuteExport(saveFileDialog2.FileName, table);
        }

        private void btnSearchList_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        public static DataTable ToDataTable(List<ZhuiSuItem> lst)
        {
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "编号";
            column.Caption = "编号";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "生产时间";
            column.Caption = "生产时间";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "设备编号";
            column.Caption = "设备编号";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "操作员编号";
            column.Caption = "操作员编号";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "电池条码";
            column.Caption = "电池条码";
            table.Columns.Add(column);

            for (int i = 0; i < lst.Count; i++)
            {
                row = table.NewRow();
                row["编号"] = lst[i].Iden.ToString();
                row["生产时间"] = lst[i].ProductTime.ToString();
                row["设备编号"] = lst[i].DeviceNo.ToString();
                row["操作员编号"] = lst[i].Operator.ToString();
                row["电池条码"] = lst[i].BatteryBarCode.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
