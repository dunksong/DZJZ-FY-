using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using ExcelLibrary.SpreadSheet;


using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;  


namespace Maticsoft.Common
{
    public class DataToExcel_Ex
    {
        #region 将DataTable导出为Excel
        /// <summary>
        /// 将DataTable导出为Excel
        /// </summary>
        /// <param name="table">DataTable数据源</param>
        /// <param name="name">文件名</param>
        public void ExportToSpreadsheet(System.Data.DataTable table, string name)
        {
            Random r = new Random();
            string rf = "";
            for (int j = 0; j < 10; j++)
            {
                rf = r.Next(int.MaxValue).ToString();
            }

            HttpContext context = HttpContext.Current;
            context.Response.Clear();

            context.Response.ContentType = "text/csv";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + rf + ".xls");
            context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write(column.ColumnName + ",");
                //context.Response.Write(column.ColumnName + "(" + column.DataType + "),");
            }

            context.Response.Write(Environment.NewLine);
            double test;

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    switch (table.Columns[i].DataType.ToString())
                    {
                        case "System.String":
                            if (double.TryParse(row[i].ToString(), out test)) context.Response.Write("=");
                            context.Response.Write("\"" + row[i].ToString().Replace("\"", "\"\"") + "\",");
                            break;
                        case "System.DateTime":
                            if (row[i].ToString() != "")
                                context.Response.Write("\"" + ((DateTime)row[i]).ToString("yyyy-MM-dd hh:mm:ss") + "\",");
                            else
                                context.Response.Write("\"" + row[i].ToString().Replace("\"", "\"\"") + "\",");
                            break;
                        default:
                            context.Response.Write("\"" + row[i].ToString().Replace("\"", "\"\"") + "\",");
                            break;
                    }
                }
                context.Response.Write(Environment.NewLine);
            }

            context.Response.End();

        }
        #endregion

        //attachName:保存文件名

        //tab:表DataTable

        //sheetName:Excel sheet名

        //RowOfSheet:Excel每页sheet 大小
        public static void DataTable2Excel(string attachName, System.Data.DataTable tab, string sheetName, string path)
        {
            int RowOfSheet = 65536;
            string file = path + attachName + ".xls";
            Workbook workbook = new Workbook();

            int page = tab.Rows.Count / RowOfSheet + 1;
            for (int currPage = 0; currPage < page; currPage++)
            {

                Worksheet worksheet = new Worksheet(Convert.ToString(sheetName) + ":" + (currPage + 1));
                //构建表头
                for (int col = 0; col < tab.Columns.Count; col++)
                {
                    worksheet.Cells[0, col] = new Cell(tab.Columns[col].ColumnName);
                }

                for (int rowIndex = 0; rowIndex < RowOfSheet - 1; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < tab.Columns.Count; colIndex++)
                    {
                        if (currPage * RowOfSheet + rowIndex < tab.Rows.Count)
                            worksheet.Cells[rowIndex + 1, colIndex] = new Cell(tab.Rows[currPage * RowOfSheet + rowIndex][colIndex].ToString());
                    }
                }
                //设置默认宽度
                if (worksheet.Cells.LastRowIndex > 1)
                {
                    for (int n = 0; n < tab.Columns.Count; n++)
                    {
                        if (worksheet.Cells[1, n].Value.ToString().Length >= 50)
                            worksheet.Cells.ColumnWidth[(ushort)n] = 10000;
                        else if (worksheet.Cells[1, n].Value.ToString().Length >= 10)
                            worksheet.Cells.ColumnWidth[(ushort)n] = (ushort)(worksheet.Cells[1, n].Value.ToString().Length * 300);
                        else
                            worksheet.Cells.ColumnWidth[(ushort)n] = 3000;
                    }
                }
                else
                    worksheet.Cells.ColumnWidth[0, (ushort)(tab.Columns.Count - 1)] = 3000;

                workbook.Worksheets.Add(worksheet);
            }
            workbook.Save(file);

        }


        /// <summary>
        /// 由DataTable导出Excel[web]
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="fileName">指定Excel工作表名称</param>
        /// <returns>Excel工作表</returns>
        public static void ExportDataTableToExcel(DataTable sourceTable, string fileName, string sheetName)
        {
            try
            {
                MemoryStream ms = ExportDataTableToExcel(sourceTable, sheetName) as MemoryStream;
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.Response.End();
                ms.Close();
                ms = null;
            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.Write(ex);
            }
        }

        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static string Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            try
            {
              
                String[] Files = System.IO.Directory.GetFiles(System.IO.Directory.GetParent(strFileName).ToString());
                for (int i = 0; i < Files.Length; i++)
                {
                    FileInfo fi = new FileInfo(Files[i]);
                    if (fi != null)
                    {
                        DateTime dt = fi.CreationTime;
                        if (dt != null && dt.Date < DateTime.Now.Date)
                        {
                            fi.Delete();
                        }
                    }
                   
                } 

                using (MemoryStream ms = ExportDataTableToExcel(dtSource, strHeaderText) as MemoryStream)
                {
                    using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region 构造Excel
        /// <summary>
        /// 构造Excel
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private static Stream ExportDataTableToExcel(DataTable sourceTable, string sheetName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet(sheetName);

            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, sourceTable.Columns.Count-1));

            #region 样式

            // 标题字体   
            IFont titlefont = workbook.CreateFont();
            titlefont.FontName = "黑体";
            titlefont.FontHeightInPoints = 18;// 字体大小   
            titlefont.IsBold = false; //加粗  

            // 列表标题字体
            IFont headfont = workbook.CreateFont();
            headfont.FontName = "黑体";
            headfont.FontHeightInPoints = 12;// 字体大小   
            headfont.IsBold = false; //加粗  



            // 标题样式  
            ICellStyle titlestyle = workbook.CreateCellStyle();
            titlestyle.SetFont(titlefont);
            titlestyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;// 左右居中   
            titlestyle.VerticalAlignment = VerticalAlignment.Center;// 上下居中   
            titlestyle.IsLocked = true;
            titlestyle.WrapText = true;// 自动换行   

            // 列头样式           
            ICellStyle headstyle = workbook.CreateCellStyle();
            headstyle.SetFont(headfont);
            headstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;// 左右居中   
            headstyle.VerticalAlignment = VerticalAlignment.Center;// 上下居中   
            headstyle.IsLocked = true;
         
            // headstyle.WrapText = true;// 自动换行   

            #endregion

            //创建标题
            IRow headerRow = sheet.CreateRow(0);
            headerRow.Height = 600;
            ICell titleCell = headerRow.CreateCell(0);
            titleCell.SetCellValue(new HSSFRichTextString(sheetName));
            titleCell.CellStyle = titlestyle;



            //创建列头
            headerRow = sheet.CreateRow(1);
            headerRow.Height = 400;
            ICell headerCell;
            foreach (DataColumn column in sourceTable.Columns)
            {
                headerCell = headerRow.CreateCell(column.Ordinal);
                headerCell.SetCellValue(column.ColumnName);
            
                headerCell.CellStyle = headstyle;
            }

            int rowIndex = 2;

            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    //switch (column.DataType.ToString())
                    //{
                    //    case "System.DateTime":
                    //        dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDateTime(row[column].ToString()));
                    //        break;
                    //    default:
                    //        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    //        break;
                    //}
                }
                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        } 
        #endregion
    }

}