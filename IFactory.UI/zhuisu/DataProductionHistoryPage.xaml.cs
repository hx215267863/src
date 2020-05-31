using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IFactory.UI.Controls;
using System.Windows.Markup;

using IFactory.UI.Core;
using IFactory.Domain.Models;
using IFactory.Platform.Common.Response.Product;
using IFactory.Platform.Common.Request.Product;
using System.Windows.Forms;
using System.Data;
using IFactory.UI.UserManager;

namespace IFactory.UI.zhuisu
{
    /// <summary>
    /// DataProductionHistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class DataProductionHistoryPage : BasePage, IComponentConnector
    {

        public int IdneDID { get; set; }

        public int? ProcessDID { get; set; }

        public string Keyword { get; set; }

        public ICommand ViewDetailCommand { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public string code { get; set; }

        public DataProductionHistoryPage()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewDetailCommand = (ICommand)new RouteCommand<DataProductionItem>(new Action<DataProductionItem>(this.ViewDetail));
            this.RefreshData();
        }

        public void RefreshData()
        {
            code = FactoryCheckDialog.EndProductNo;
            DataProductionResponse DataProductionResponse = LocalApi.ExecuteDataHistory(new DataProductionRequest()
            {
                Keyword = this.Keyword,
                ProcessDID = this.ProcessDID,
                PageNumber = this.pager.PageNumber,
                //PageNumber = pageNumber.Value,
                TimeStart = this.TimeStart,
                //TimeEnd = this.TimeEnd,
                PageSize = 10,
                code = this.code
            });
            this.pager.Setup(DataProductionResponse.DataProductions);
            this.dataGrid.ItemsSource = DataProductionResponse.DataProductions;
        }

        private void ViewDetail(DataProductionItem item)
        {
            this.NavigationService.Navigate(new DataProductionHistoryPage()
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
            column.ColumnName = "时间";
            column.Caption = "时间";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "生产总量";
            column.Caption = "生产总量";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "良品总量";
            column.Caption = "良品总量";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "产能";
            column.Caption = "产能";
            table.Columns.Add(column);

            for (int i = 0; i < lst.Count; i++)
            {
                row = table.NewRow();
                row["编号"] = lst[i].Iden.ToString();
                row["时间"] = lst[i].ProductTime.ToString();
                row["生产总量"] = lst[i].DeviceNo.ToString();
                row["良品总量"] = lst[i].Operator.ToString();
                row["产能"] = lst[i].BatteryBarCode.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
