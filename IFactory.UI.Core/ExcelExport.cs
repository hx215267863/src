using IFactory.Common.Logs;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Data;

namespace IFactory.UI.Core
{
    public class ExcelExport
    {
        private string fileName;
        
        public string subject = "report";
        public string sheetName = "sheet1";

        private HSSFWorkbook hssfworkbook;
        private DataTable dt = new DataTable();

        #region Export

        public void ExcuteExport(string fileName, DataTable _dt)
        {
            if (_dt.Rows.Count == 0) return;
            if(string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("请先输入保存路径");
                return;
            }

            dt = _dt.Copy();
            this.fileName = fileName;

            try
            {
                InitializeWorkbook();
                GenerateData();
                WriteToFile(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "IFactory";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = subject;
            hssfworkbook.SummaryInformation = si;
        }

        private void GenerateData()
        {
            ISheet sheet1 = hssfworkbook.CreateSheet(sheetName);

            #region style
            //header font
            IFont headerFont = hssfworkbook.CreateFont();
            headerFont.FontHeightInPoints = 12;
            headerFont.Color = HSSFColor.BLUE.index;
            headerFont.Boldweight = 4;

            //header style
            ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
            headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            headerStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
            headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            headerStyle.WrapText = true;
            headerStyle.SetFont(headerFont);
            //headerStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PINK.index;

            //facLeftStyle
            ICellStyle facLeftStyle = hssfworkbook.CreateCellStyle();
            facLeftStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
            facLeftStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
            facLeftStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            facLeftStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            facLeftStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            facLeftStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            facLeftStyle.WrapText = true;
            facLeftStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            facLeftStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index;


            //segLeftStyle
            ICellStyle segLeftStyle = hssfworkbook.CreateCellStyle();
            segLeftStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
            segLeftStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
            segLeftStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            segLeftStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            segLeftStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            segLeftStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            segLeftStyle.WrapText = true;
            segLeftStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            segLeftStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index;
            #endregion

            //create header
            IRow rowHeader = sheet1.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                rowHeader.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                rowHeader.Cells[i].CellStyle = headerStyle;
            }

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                }
            }

            for (int r = 1; r <= dt.Rows.Count; r++)
            {
                for (int c = 0; c < sheet1.GetRow(r).Cells.Count; c++)
                {
                    switch (c % 14)
                    {
                        case 10:
                            sheet1.GetRow(r).Cells[c].CellStyle = facLeftStyle;

                            break;
                        default:
                            sheet1.GetRow(r).Cells[c].CellStyle = segLeftStyle;
                            break;
                    }
                }
            }
        }

        private void WriteToFile(string filepath)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(filepath, FileMode.OpenOrCreate);
                //Write the stream data of workbook to the root directory
                hssfworkbook.Write(file);
                MessageBox.Show("导出成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败!");
            }
            finally
            {
                file.Close();
            }
        }
        #endregion
        
    }
}
