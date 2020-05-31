using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyExcel
{
    public class ExcelHelper : IDisposable
    {

        private FileStream m_fs = null;
        private bool m_disposed;
        private string m_curFilePath;
        public ExcelHelper()//构造函数，读入文件路径 
        {
            m_disposed = false;
        }
        public void CreateDateDirectory()
        {
            //获取当前文件夹路径
            //string path = new DirectoryInfo("../../../../").FullName;//当前应用程序路径的上级目录
            //检查是否存在文件夹
            string subPath = "E://机械手2//2019_7_6//DateRecord//" + DateTime.Now.ToString("yyyyMMdd") + "//";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }
            m_curFilePath = subPath;
        }
        public void CreateExcelRecordFile()
        {
            if (false == System.IO.File.Exists(m_curFilePath + "前工序电芯数据记录表.xls"))
            {
                InitExcelStyle("前工序电芯数据记录表.xls", "Data", "前工序电芯数据记录表");
            }
            if (false == System.IO.File.Exists(m_curFilePath + "电芯外观检测缺陷数据记录表.xls"))
            {
                InitExcelStyle("电芯外观检测缺陷数据记录表.xls", "appearanceInspectionDate", "电芯外观检测缺陷数据记录表");
            }
        }
        //创建excel文件
        public void CreateExclFile(string filePath, string sheetName)
        {
            XSSFWorkbook workbook2007 = new XSSFWorkbook();  //新建xlsx工作簿  
            workbook2007.CreateSheet(sheetName);
            FileStream file2007 = new FileStream(filePath, FileMode.Create);
            workbook2007.Write(file2007);
            file2007.Close();
            workbook2007.Close();
        }
        //读excel文件数据
        public void ReadExcelData(string fileName)
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            //string fileName = "E:\\Excel2003.xls";
            FileStream fileStream = new FileStream(@"E:\Excel2003.xls", FileMode.Open, FileAccess.Read);

            workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  

            ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
            for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
            {
                row = sheet.GetRow(i);   //row读入第i行数据  
                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                    {
                        string cellValue = row.GetCell(j).ToString(); //获取i行j列数据
                        //数据显示待完成   
                        //Console.WriteLine(cellValue);
                    }
                }
            }
            Console.ReadLine();
            fileStream.Close();
            workbook.Close();
        }

        public void AppendDataToExcel(string excelPath, string strTime, string strSN, string strInfo, string strResult)
        {
            FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);//读取流
            IWorkbook workbook = new HSSFWorkbook(fs);
            ISheet sheet = workbook.GetSheetAt(0); //获取工作表
            IRow row = sheet.GetRow(0); //得到表头
            FileStream fout = new FileStream(excelPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);//写入流
            row = sheet.CreateRow((sheet.LastRowNum + 1));//在工作表中添加一行
            row.Height = 400;
            ICellStyle style_contentText_Center = workbook.CreateCellStyle(); 
            style_contentText_Center.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//文字水平对齐方式
            style_contentText_Center.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式

            ICell datacell_0 = row.CreateCell(0);
            datacell_0.SetCellValue(/*DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")*/strTime);
            datacell_0.CellStyle = style_contentText_Center;
            ICell datacell_1 = row.CreateCell(1);
            datacell_1.SetCellValue(strSN);
            datacell_1.CellStyle = style_contentText_Center;
            //ICell datacell_2 = row.CreateCell(2);
            //datacell_2.SetCellValue(strInfo);
            //datacell_2.CellStyle = style_contentText_Center;
            //ICell datacell_3 = row.CreateCell(3);
            //datacell_3.SetCellValue(strResult);
            //datacell_3.CellStyle = style_contentText_Center;
            fout.Flush();
            workbook.Write(fout);//写入文件
            workbook = null;
            fout.Close();
        }
        public void InitExcelStyle(string fileName, string sheetName, string name)
        {
            short lineheight_header = 400;  //标题列行高
                                            // short lineheight_content = 300; //记录行行高
            short fontsize_header = 12; //标题字体大小
            short fontsize_content = 11;//记录字体大小
            ///* xls */
            //IWorkbook book = new HSSFWorkbook();
            /* xlsx */
            IWorkbook book = new HSSFWorkbook();
            #region 定义样式
            // 列名 样式
            ICellStyle style_ColumnName = book.CreateCellStyle();
            style_ColumnName.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//文字水平对齐方式
            style_ColumnName.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //style_ColumnName.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//设置边框
            //style_ColumnName.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_ColumnName.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_ColumnName.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font_ColumnName = book.CreateFont();//字体
            font_ColumnName.FontName = "宋体";
            font_ColumnName.FontHeightInPoints = fontsize_header;
            font_ColumnName.Boldweight = (short)FontBoldWeight.Bold;//字体加粗样式
            font_ColumnName.Color = HSSFColor.Black.Index;//字体颜色
            style_ColumnName.SetFont(font_ColumnName);
            IFont font_contentText = book.CreateFont();//字体
            font_contentText.FontName = "宋体";
            font_contentText.FontHeightInPoints = fontsize_content;
            /* Content - text - Left*/
            ICellStyle style_contentText_Left = book.CreateCellStyle();
            style_contentText_Left.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;//文字水平对齐方式
            style_contentText_Left.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //style_contentText_Left.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//设置边框
            //style_contentText_Left.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_contentText_Left.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_contentText_Left.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_contentText_Left.SetFont(font_contentText);
            /* Content - text - Center */
            ICellStyle style_contentText_Center = book.CreateCellStyle();
            style_contentText_Center.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//文字水平对齐方式
            style_contentText_Center.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //style_contentText_Center.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//设置边框
            //style_contentText_Center.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_contentText_Center.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_contentText_Center.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_contentText_Center.SetFont(font_contentText);
            /* Content - DateTime */
            ICellStyle style_date = book.CreateCellStyle();
            style_date.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//文字水平对齐方式
            style_date.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //style_date.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//设置边框
            //style_date.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_date.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_date.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font_date = book.CreateFont();//字体
            font_date.FontName = "宋体";
            font_date.FontHeightInPoints = fontsize_content;
            style_date.SetFont(font_date);
            IDataFormat dataFormatCustom = book.CreateDataFormat(); //定义数据格式
            style_date.DataFormat = dataFormatCustom.GetFormat("yyyy-MM-dd");
            /* Content - numeric  */
            ICellStyle style_numeric = book.CreateCellStyle();
            style_numeric.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;//文字水平对齐方式
            style_numeric.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //style_numeric.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//设置边框
            //style_numeric.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_numeric.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            //style_numeric.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font_numeric = book.CreateFont();//字体
            font_numeric.FontName = "宋体";
            font_numeric.FontHeightInPoints = fontsize_content;
            style_numeric.SetFont(font_date);
            IDataFormat numericFormatCustom = book.CreateDataFormat(); //定义数据格式
            style_numeric.DataFormat = numericFormatCustom.GetFormat("0.00");
            #endregion
            #region 创建表单
            /* 创建一个表单 */
            ISheet sheet = book.CreateSheet(sheetName);
            #endregion
            #region 第一行 标题列
            //IRow row_head1 = sheet.CreateRow(0); //建立行，参数为行号，从0计
            //row_head1.Height = lineheight_header;
            //sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));
            //ICell row0cell0 = row_head1.CreateCell(0);
            //row0cell0.SetCellValue(name);
            //row0cell0.CellStyle = style_ColumnName;
            //sheet.AddMergedRegion(new CellRangeAddress(0, 0, 5, 12));
            //ICell row0cell1 = row_head1.CreateCell(5); 
            //row0cell1.SetCellValue("借款人");
            //row0cell1.CellStyle = style_ColumnName;
            #endregion
            #region 第二行 标题列
            IRow row_head2 = sheet.CreateRow(0); //建立行，参数为行号，从0计
            row_head2.Height = lineheight_header;
            ICell row1cell_0 = row_head2.CreateCell(0);
            row1cell_0.SetCellValue("inputTime");
            row1cell_0.CellStyle = style_ColumnName;
            sheet.SetColumnWidth(0, 25 * 256);
            ICell row1cell_1 = row_head2.CreateCell(1);
            row1cell_1.SetCellValue("barcode");
            row1cell_1.CellStyle = style_ColumnName;
            sheet.SetColumnWidth(1, 30 * 256);
            //ICell row1cell_2 = row_head2.CreateCell(2);
            //row1cell_2.SetCellValue("test1");
            //row1cell_2.CellStyle = style_ColumnName;
            //sheet.SetColumnWidth(2, 50 * 256);
            //ICell row1cell_3 = row_head2.CreateCell(3);
            //row1cell_3.SetCellValue("test2");
            //row1cell_3.CellStyle = style_ColumnName;
            //sheet.SetColumnWidth(3, 50 * 256);
            #endregion
            #region 冻结 标题1行
            sheet.CreateFreezePane(0, 2, 0, 2);
            #endregion
            #region 数据行
            //IRow datarow = sheet.CreateRow(2);
            //datarow.Height = lineheight_content;
            //ICell datacell_0 = datarow.CreateCell(0);
            //datacell_0.SetCellValue(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            //datacell_0.CellStyle = style_contentText_Center;
            //ICell datacell_1 = datarow.CreateCell(1);
            //datacell_1.SetCellValue("Y3242342342432");
            //datacell_1.CellStyle = style_contentText_Center;
            //ICell datacell_2 = datarow.CreateCell(2); 
            //datacell_2.SetCellValue("主体变形、主体破损、角位破损");
            //datacell_2.CellStyle = style_contentText_Center;
            #endregion
            #region 导入 Excel
            // 转为字节数组
            MemoryStream stream = new MemoryStream();
            book.Write(stream);
            var buf = stream.ToArray();
            fileName = m_curFilePath + fileName;
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                //COperatorExportFunction.IBG_LOG(0, 0, 0, ("创建EXECL文件成功").ToCharArray());
            }
            #endregion
        }

        public void Dispose()//IDisposable为垃圾回收相关的东西
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                if (disposing)
                {
                    if (m_fs != null)
                        m_fs.Close();
                }
                m_fs = null;
                m_disposed = true;
            }
        }
    }
}
