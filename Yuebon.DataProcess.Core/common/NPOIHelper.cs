using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using static Yuebon.DataProcess.Core.OutSideDbService.Entity.DbRowInfo;

namespace Yuebon.DataProcess.Core.common
{
    /// <summary>
    /// office 导入导出
    /// </summary>
    public class NPOIHelper
    {
        #region 导出至EXCEL
        /// <summary>
        /// DataTable 导出到 Excel 的 MemoryStream
        /// </summary>
        /// <param name="dtSource">源 DataTable</param>
        /// <param name="strHeaderText">表头文本 空值未不要表头标题</param>
        /// <returns></returns>
        public static MemoryStream ExportExcel(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            #region 文件属性
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "yuebon.com";
            workbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "yuebon.com";
            si.ApplicationName = "yuebon.com";
            si.LastAuthor = "yuebon.com";
            si.Comments = "";
            si.Title = "";
            si.Subject = "";
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;
            #endregion
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding("gb2312").GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding("gb2312").GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            int intTop = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表、填充表头、填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }
                    intTop = 0;
                    #region 表头及样式
                    {
                        if (strHeaderText.Length > 0)
                        {
                            IRow headerRow = sheet.CreateRow(intTop);
                            intTop += 1;
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);
                            ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));

                        }
                    }
                    #endregion
                    #region  列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(intTop);
                        intTop += 1;
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }


                    }
                    #endregion
                    rowIndex = intTop;
                }
                #endregion
                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);
                    string drValue = row[column].ToString();
                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);
                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }
                #endregion
                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }
        /// <summary>
        /// DaataTable 导出到 Excel 文件
        /// </summary>
        /// <param name="dtSource">源 DataaTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置(文件名及路径)</param>
        public static void ExportExcel(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = ExportExcel(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        #region NPOI-将【DataSet】导出到Excel【xlsx】
        /// <summary>
        /// NPOI-将【DataSet】导出到Excel【xlsx】
        /// </summary>
        /// <param name="ds">数据源DataSet</param>
        /// <param name="fileName">输出的文件名，或保存路径（输出时无需后缀名）</param>
        /// <param name="output">是否输出到页面</param>
        public static void DataSetToXLSX(DataSet ds, string fileName)
        {
            IWorkbook workbook = new XSSFWorkbook();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    CreateSheetByDataTable(workbook, dt); //创建Sheet
                }
            }
            #region 输出操作
            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            #endregion
        }
        #endregion

        #region NPOI-将【DataSet】导出到Excel【xls】(65535行以内)
        /// <summary>
        ///  NPOI-将DataTable导出到Excel【xls】(65535行以内)
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="fileName">输出的文件名，或保存路径</param>
        ///  <param name="output">是否输出到页面</param>
        public static void DataSetToXls(DataSet dsSource, string fileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            foreach (DataTable dtSource in dsSource.Tables)
            {
                HSSFSheet sheet = workbook.CreateSheet(dtSource.TableName) as HSSFSheet;
                HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
                #region 取得列宽
                int[] arrColWidth = new int[dtSource.Columns.Count];
                foreach (DataColumn item in dtSource.Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
                #endregion
                #region 列头及样式
                //{
                //    HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                //    HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                //    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //    HSSFFont font = workbook.CreateFont() as HSSFFont;
                //    font.FontHeightInPoints = 10;
                //    font.Boldweight = 700;
                //    headStyle.SetFont(font);
                //    foreach (DataColumn column in dtSource.Columns)
                //    {
                //        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                //        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                //        //设置列宽
                //        int cwidth = (arrColWidth[column.Ordinal] + 1);
                //        if (cwidth > 50)
                //            cwidth = 50;
                //        sheet.SetColumnWidth(column.Ordinal, cwidth * 256);
                //    }
                //}
                #endregion
                #region 填充内容
                int rowIndex = 0;
                foreach (DataRow row in dtSource.Rows)
                {
                    HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                    foreach (DataColumn column in dtSource.Columns)
                    {
                        HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
                        string drValue = row[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String": //字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime": //日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle; //格式化显示
                                break;
                            case "System.Boolean": //布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16": //整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal": //浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull": //空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                    }
                    rowIndex++;
                }
                #endregion
            }
            #region 输出操作
            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            #endregion
        }
        #endregion

        #endregion

        #region 读取EXCEL

        #region 连接Excel  读取Excel数据   并返回DataSet数据集合
        /// <summary>
        /// 连接Excel  读取Excel数据   并返回DataSet数据集合
        /// </summary>
        /// <param name="filepath">Excel服务器路径</param>
        /// <param name="tableName">Excel表名称</param>
        /// <returns></returns>
        public static DataSet ExcelSqlConnectionForImpData(string filepath, int headRowIndex)
        {
            try
            {
                ArrayList arrSheets = ReadExcelSheets(filepath); //获取表名集合
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                foreach (object sheetName in arrSheets)
                {
                    if (!sheetName.ToString().Contains("FilterDatabase") && !sheetName.ToString().Contains("Print_Area") && sheetName.ToString() != "iayZ2B$" && !sheetName.ToString().Contains("FilterData") && !sheetName.ToString().Contains("Macro") && !sheetName.ToString().Contains("#Cols"))
                    {
                        bool readOk = false;
                        try
                        {
                            DataTable tmpDt = ReadExcelToDataTable(filepath, sheetName.ToString().Replace("'", ""));
                            ds.Tables.Add(tmpDt);
                            readOk = true;
                        }
                        catch (Exception ex)
                        {
                            readOk = false;
                        }
                        if (readOk)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                DataTable dt1 = ds.Tables[ds.Tables.Count - 1];
                                DataTable dt2 = new DataTable();
                                dt2.TableName = dt1.TableName;
                                for (int i = 0; i < dt1.Columns.Count; i++)
                                {
                                    DataColumn dc = new DataColumn();
                                    dc.DataType = typeof(System.String);
                                    dt2.Columns.Add(dc);
                                }
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    DataRow dr2 = dt2.NewRow();
                                    for (int ii = 0; ii < dt1.Columns.Count; ii++)
                                    {
                                        dr2[ii] = dt1.Rows[i][ii].ToString();
                                    }
                                    dt2.Rows.Add(dr2);
                                }

                                #region 将列头填充进在0行新增的行中，使datatable与导入的excel文件完全一致。
                                DataRow dr1 = dt2.NewRow();
                                Regex reg = new Regex(@"^F\d*$");//所有F+数字的字符串组合
                                for (int i = 0; i < dt1.Columns.Count; i++)
                                {
                                    if (!string.IsNullOrEmpty(dt1.Columns[i].ColumnName) && !reg.IsMatch(dt1.Columns[i].ColumnName.ToString()))
                                    {
                                        dr1[i] = dt1.Columns[i].ColumnName.ToString();
                                    }
                                }
                                dt2.Rows.InsertAt(dr1, 0);
                                #endregion

                                #region  如果有合并单元格的情况，则将指定的列名行的空白补齐
                                if (headRowIndex > 0 && dt2.Rows.Count > headRowIndex)
                                {
                                    for (int i = 0; i < dt2.Columns.Count; i++)
                                    {
                                        if (string.IsNullOrEmpty(dt2.Rows[headRowIndex][i].ToString()))
                                        {
                                            for (int ii = headRowIndex - 1; ii >= 0; ii--)
                                            {
                                                if (!string.IsNullOrEmpty(dt2.Rows[ii][i].ToString()))
                                                {
                                                    dt2.Rows[headRowIndex][i] = dt2.Rows[ii][i].ToString();
                                                    break;                                                                                            //如果找到并赋值，则停止循环。
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion


                                delEmpRow(ref dt2);
                                ds1.Tables.Add(dt2);
                            }
                        }
                    }
                }

                #region 将NoName值改为""
                foreach (DataTable item in ds1.Tables)
                {
                    if (item.Rows.Count > 0)
                    {
                        foreach (DataRow rowItem in item.Rows)
                        {
                            foreach (DataColumn colItem in item.Columns)
                            {
                                if (rowItem[colItem].ToString().Contains("NoName"))
                                {
                                    rowItem[colItem] = "";
                                }
                            }
                        }
                    }
                }
                #endregion

                return ds1;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        #region 过滤空行 delEmpRow(ref DataTable dt)
        /// <summary>
        /// 过滤空行
        /// </summary>
        /// <param name="dt"></param>
        private static void delEmpRow(ref DataTable dt)
        {
            Dictionary<int, int> colEmptyCheck = new Dictionary<int, int>();  //用于储存列的空值情况
            int dtRowsCount = dt.Rows.Count;
            string str = "";
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool IsHaveValue = false;
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    if (dt.Rows[i][ii] != DBNull.Value && dt.Rows[i][ii] != null && dt.Rows[i][ii].ToString().Trim() != "" && dt.Rows[i][ii].ToString().Trim() != "null*null")
                        IsHaveValue = true;
                    else
                    {
                        if (colEmptyCheck.ContainsKey(ii))
                            colEmptyCheck[ii]++;
                        else
                            colEmptyCheck.Add(ii, 1);
                    }

                }
                if (!IsHaveValue)
                    str += i + ",";
            }
            colEmptyCheck = colEmptyCheck.OrderByDescending(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (KeyValuePair<int, int> tmp in colEmptyCheck)
            {
                if (tmp.Value == dtRowsCount)
                    dt.Columns.RemoveAt(tmp.Key);
            }
            if (str != "")
            {
                string[] delId = str.Substring(0, str.Length - 1).Split(',');
                foreach (string item in delId)
                {
                    dt.Rows.RemoveAt(int.Parse(item));
                }
            }
        }
        #endregion

        #region NPOI-读取Excel指定Sheet的结构到【DataTable】
        /// <summary>
        ///  NPOI-读取Excel指定Sheet的结构到【DataTable】
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ReadExcelJGToDataTable(string filePath, string sheetName)
        {
            DataTable dtExcelData = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                {
                    #region .xlsx
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    ISheet sheet = xssfworkbook.GetSheet(sheetName);
                    #region 表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        dtExcelData.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    #region xls
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheet(sheetName);
                    #region 表头
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int headColIndex = headerRow.FirstCellNum; headColIndex < cellCount; headColIndex++)
                    {
                        if (headerRow.GetCell(headColIndex) != null)
                        {
                            dtExcelData.Columns.Add(new DataColumn("Columns" + headColIndex.ToString()));
                        }
                    }
                    #endregion
 
                    #endregion
                }
            }
            //File.Delete(filePath); //读取完后删除文件
            dtExcelData.TableName = sheetName;
            return dtExcelData;
        }
        #endregion

        #region NPOI-读取Excel指定Sheet的数据到【DataTable】
        /// <summary>
        ///  NPOI-读取Excel指定Sheet的数据到【DataTable】
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>返回DataTable</returns>
        public static DataTable ReadExcelToDataTable(string filePath, string sheetName)
        {
            DataTable dtExcelData = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                {
                    #region .xlsx
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    ISheet sheet = xssfworkbook.GetSheet(sheetName);
                    #region 表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        dtExcelData.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    #endregion

                    #region 数据
                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        DataRow dr = dtExcelData.NewRow();
                        bool hasValue = false;

                        IRow testRow = sheet.GetRow(i);
                        if (testRow == null)
                        {
                            string errorMsg = "行数问题";
                        }
                        foreach (int j in columns)
                        {
                            try
                            {

                                if (sheet.GetRow(i).GetCell(j) == null)
                                {
                                    dr[j] = null;
                                }
                                else
                                {
                                    switch (sheet.GetRow(i).GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = sheet.GetRow(i).GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dr[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dr[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            decimal de;
                                            if (decimal.TryParse(sheet.GetRow(i).GetCell(j).ToString(), out de))
                                            {
                                                dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dr[j] = sheet.GetRow(i).GetCell(j).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            break;
                                        default:
                                            dr[j] = null;
                                            break;
                                    }
                                }
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                string aa = ex.Message.ToString() + "方法：" + ex.TargetSite.ToString() + "行数：" + ex.StackTrace.ToString();
                                throw;
                            }

                        }
                        if (hasValue)
                        {
                            dtExcelData.Rows.Add(dr);
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region xls
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheet(sheetName);
                    #region 表头
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int headColIndex = headerRow.FirstCellNum; headColIndex < cellCount; headColIndex++)
                    {
                        if (headerRow.GetCell(headColIndex) != null)
                        {
                            dtExcelData.Columns.Add(new DataColumn("Columns" + headColIndex.ToString()));
                        }
                    }
                    #endregion
                    #region 数据
                    for (int rowIndex = (sheet.FirstRowNum + 1); rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        DataRow dataRow = dtExcelData.NewRow();
                        for (int colIndex = row.FirstCellNum; colIndex < dtExcelData.Columns.Count; colIndex++)
                        {
                            try
                            {
                                if (row.GetCell(colIndex) != null)
                                {
                                    switch (row.GetCell(colIndex).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(colIndex).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[colIndex] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[colIndex] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            decimal de;
                                            if (decimal.TryParse(row.GetCell(colIndex).ToString(), out de))
                                            {
                                                dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[colIndex] = row.GetCell(colIndex).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            break;
                                        default:
                                            dataRow[colIndex] = null;
                                            break;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        dtExcelData.Rows.Add(dataRow);
                    }
                    #endregion
                    #endregion
                }
            }
            //File.Delete(filePath); //读取完后删除文件
            dtExcelData.TableName = sheetName;
            return dtExcelData;
        }
        #endregion

        #region NPOI-读取Excel指定Sheet的数据到【DataTable】
        /// <summary>
        ///  NPOI-读取Excel指定Sheet的列头数据到【DataTable】
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <param name="headStartIndex">列头起始行</param>
        /// <param name="headEndIndex">列头结束行</param>
        /// <returns></returns>
        public static DataTable ReadExcelHeadInfoToDataTable(string filePath, string sheetName,int headStartIndex,int headEndIndex)
        {
            DataTable dtExcelData = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                {
                    #region .xlsx
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    ISheet sheet = xssfworkbook.GetSheet(sheetName);
                    #region 表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        dtExcelData.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    #endregion

                    #region 数据
                    int startIndex = headStartIndex != -1 ? headStartIndex : 0;
                    int endIndex = headEndIndex != -1 ? headEndIndex : sheet.LastRowNum;
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        DataRow dr = dtExcelData.NewRow();
                        bool hasValue = false;

                        IRow testRow = sheet.GetRow(i);
                        if (testRow == null)
                        {
                            string errorMsg = "行数问题";
                        }
                        foreach (int j in columns)
                        {
                            try
                            {

                                if (sheet.GetRow(i).GetCell(j) == null)
                                {
                                    dr[j] = null;
                                }
                                else
                                {
                                    switch (sheet.GetRow(i).GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = sheet.GetRow(i).GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dr[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dr[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            decimal de;
                                            if (decimal.TryParse(sheet.GetRow(i).GetCell(j).ToString(), out de))
                                            {
                                                dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dr[j] = sheet.GetRow(i).GetCell(j).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            break;
                                        default:
                                            dr[j] = null;
                                            break;
                                    }
                                }
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                string aa = ex.Message.ToString() + "方法：" + ex.TargetSite.ToString() + "行数：" + ex.StackTrace.ToString();
                                throw;
                            }

                        }
                        if (hasValue)
                        {
                            dtExcelData.Rows.Add(dr);
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region xls
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheet(sheetName);
                    int startIndex = headStartIndex != -1 ? headStartIndex : 0;
                    int endIndex = headEndIndex != -1 ? headEndIndex : sheet.LastRowNum;

                    #region 表头
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int headColIndex = headerRow.FirstCellNum; headColIndex < cellCount; headColIndex++)
                    {
                        if (headerRow.GetCell(headColIndex) != null)
                        {
                            dtExcelData.Columns.Add(new DataColumn("Columns" + headColIndex.ToString()));
                        }
                    }
                    #endregion

                    #region 数据
                    for (int rowIndex = startIndex; rowIndex <= endIndex; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        DataRow dataRow = dtExcelData.NewRow();
                        for (int colIndex = row.FirstCellNum; colIndex < dtExcelData.Columns.Count; colIndex++)
                        {
                            try
                            {
                                if (row.GetCell(colIndex) != null)
                                {
                                    switch (row.GetCell(colIndex).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(colIndex).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[colIndex] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[colIndex] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            decimal de;
                                            if (decimal.TryParse(row.GetCell(colIndex).ToString(), out de))
                                            {
                                                dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[colIndex] = row.GetCell(colIndex).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            break;
                                        default:
                                            dataRow[colIndex] = null;
                                            break;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        dtExcelData.Rows.Add(dataRow);
                    }
                    #endregion

                    #endregion
                }
            }
            //File.Delete(filePath); //读取完后删除文件
            dtExcelData.TableName = sheetName;
            return dtExcelData;
        }
        #endregion

        #region NPOI-读取Excel指定Sheet的数据到【DataTable】
        /// <summary>
        ///  NPOI-读取Excel指定Sheet的列头数据到【DataTable】
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <param name="headStartIndex">列头起始行</param>
        /// <param name="headEndIndex">列头结束行</param>
        /// <returns></returns>
        public static DataTable ReadExcelHeadInfoToList(string filePath, string sheetName, int headStartIndex, int headEndIndex)
        {
            DataTable dtExcelData = new DataTable();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                {
                    #region .xlsx
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    ISheet sheet = xssfworkbook.GetSheet(sheetName);
                    #region 表头
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<DbColums> columnsx = new List<DbColums>();
                    //List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        columnsx.Add(new DbColums() { colIndex = i });
                        //dtExcelData.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    #endregion

                    #region 数据
                    int startIndex = headStartIndex != -1 ? headStartIndex : 0;
                    int endIndex = headEndIndex != -1 ? headEndIndex : sheet.LastRowNum;
                    for (int i = startIndex; i <= endIndex; i++)
                    {
                        DbRowInfo drx = new DbRowInfo();
                        //DataRow dr = dtExcelData.NewRow();
                        //bool hasValue = false;

                        IRow testRow = sheet.GetRow(i);
                        if (testRow == null)
                        {
                            string errorMsg = "行数问题";
                        }
                        foreach (int j in columns)
                        {
                            try
                            {

                                if (sheet.GetRow(i).GetCell(j) == null)
                                {
                                    dr[j] = null;
                                }
                                else
                                {
                                    switch (sheet.GetRow(i).GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = sheet.GetRow(i).GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dr[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dr[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            decimal de;
                                            if (decimal.TryParse(sheet.GetRow(i).GetCell(j).ToString(), out de))
                                            {
                                                dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dr[j] = sheet.GetRow(i).GetCell(j).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            break;
                                        default:
                                            dr[j] = null;
                                            break;
                                    }
                                }
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                string aa = ex.Message.ToString() + "方法：" + ex.TargetSite.ToString() + "行数：" + ex.StackTrace.ToString();
                                throw;
                            }

                        }
                        if (hasValue)
                        {
                            dtExcelData.Rows.Add(dr);
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region xls
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheet(sheetName);
                    int startIndex = headStartIndex != -1 ? headStartIndex : 0;
                    int endIndex = headEndIndex != -1 ? headEndIndex : sheet.LastRowNum;

                    #region 表头
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int headColIndex = headerRow.FirstCellNum; headColIndex < cellCount; headColIndex++)
                    {
                        if (headerRow.GetCell(headColIndex) != null)
                        {
                            dtExcelData.Columns.Add(new DataColumn("Columns" + headColIndex.ToString()));
                        }
                    }
                    #endregion

                    #region 数据
                    for (int rowIndex = startIndex; rowIndex <= endIndex; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        DataRow dataRow = dtExcelData.NewRow();
                        for (int colIndex = row.FirstCellNum; colIndex < dtExcelData.Columns.Count; colIndex++)
                        {
                            try
                            {
                                if (row.GetCell(colIndex) != null)
                                {
                                    switch (row.GetCell(colIndex).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(colIndex).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[colIndex] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[colIndex] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            decimal de;
                                            if (decimal.TryParse(row.GetCell(colIndex).ToString(), out de))
                                            {
                                                dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[colIndex] = row.GetCell(colIndex).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            break;
                                        default:
                                            dataRow[colIndex] = null;
                                            break;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        dtExcelData.Rows.Add(dataRow);
                    }
                    #endregion

                    #endregion
                }
            }
            //File.Delete(filePath); //读取完后删除文件
            dtExcelData.TableName = sheetName;
            return dtExcelData;
        }
        #endregion

        #region NPOI-读取Excel所有Sheet的数据到【DataSet】
        /// <summary>
        ///  NPOI-读取Excel所有Sheet的数据到【DataSet】
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>返回DataSet</returns>
        public static DataSet ReadExcelToDataSet(string filePath)
        {
            DataSet ds = new DataSet();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                {
                    #region .xlsx
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    ArrayList arrSheets = ReadExcelSheets(filePath); //获取表名集合
                    foreach (object sheetName in arrSheets)
                    {
                        ISheet sheet = xssfworkbook.GetSheet(sheetName.ToString());
                        DataTable dtExcelData = new DataTable(sheetName.ToString());
                        #region 表头
                        IRow header = sheet.GetRow(sheet.FirstRowNum);
                        List<int> columns = new List<int>();
                        for (int i = 0; i < header.LastCellNum; i++)
                        {
                            dtExcelData.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        }
                        #endregion
                        #region 数据
                        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                        {
                            DataRow dr = dtExcelData.NewRow();
                            bool hasValue = false;
                            foreach (int j in columns)
                            {
                                switch (sheet.GetRow(i).GetCell(j).CellType)
                                {
                                    case CellType.String:
                                        string str = sheet.GetRow(i).GetCell(j).StringCellValue;
                                        if (str != null && str.Length > 0)
                                        {
                                            dr[j] = str.ToString();
                                        }
                                        else
                                        {
                                            dr[j] = null;
                                        }
                                        break;
                                    case CellType.Numeric:
                                        decimal de;
                                        if (decimal.TryParse(sheet.GetRow(i).GetCell(j).ToString(), out de))
                                        {
                                            dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).NumericCellValue);
                                        }
                                        else
                                        {
                                            dr[j] = sheet.GetRow(i).GetCell(j).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                        }
                                        break;
                                    case CellType.Boolean:
                                        dr[j] = Convert.ToString(sheet.GetRow(i).GetCell(j).BooleanCellValue);
                                        break;
                                    case CellType.Error:
                                        break;
                                    default:
                                        dr[j] = null;
                                        break;
                                }
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                            if (hasValue)
                            {
                                dtExcelData.Rows.Add(dr);
                            }
                        }
                        #endregion
                        ds.Tables.Add(dtExcelData);
                    }
                    #endregion
                }
                else
                {
                    #region .xls
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    ArrayList arrSheets = ReadExcelSheets(filePath); //获取表名集合
                    foreach (object sheetName in arrSheets)
                    {
                        ISheet sheet = workbook.GetSheet(sheetName.ToString());
                        DataTable dtExcelData = new DataTable(sheetName.ToString());
                        #region 表头
                        IRow headerRow = sheet.GetRow(0);
                        int cellCount = headerRow.LastCellNum;
                        for (int headColIndex = headerRow.FirstCellNum; headColIndex < cellCount; headColIndex++)
                        {
                            if (headerRow.GetCell(headColIndex).StringCellValue.Trim().Length > 0)
                            {
                                dtExcelData.Columns.Add(new DataColumn("Columns" + headColIndex.ToString()));
                            }
                        }
                        #endregion
                        #region 数据
                        for (int rowIndex = (sheet.FirstRowNum + 1); rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            IRow row = sheet.GetRow(rowIndex);
                            DataRow dataRow = dtExcelData.NewRow();
                            for (int colIndex = row.FirstCellNum; colIndex < dtExcelData.Columns.Count; colIndex++)
                            {
                                try
                                {
                                    if (row.GetCell(colIndex) != null)
                                    {
                                        switch (row.GetCell(colIndex).CellType)
                                        {
                                            case CellType.String:
                                                string str = row.GetCell(colIndex).StringCellValue;
                                                if (str != null && str.Length > 0)
                                                {
                                                    dataRow[colIndex] = str.ToString();
                                                }
                                                else
                                                {
                                                    dataRow[colIndex] = null;
                                                }
                                                break;
                                            case CellType.Numeric:
                                                decimal de;
                                                if (decimal.TryParse(row.GetCell(colIndex).ToString(), out de))
                                                {
                                                    dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).NumericCellValue);
                                                }
                                                else
                                                {
                                                    dataRow[colIndex] = row.GetCell(colIndex).DateCellValue;//.ToString("yyyy-MM-dd HH:mm:ss")
                                                }
                                                break;
                                            case CellType.Boolean:
                                                dataRow[colIndex] = Convert.ToString(row.GetCell(colIndex).BooleanCellValue);
                                                break;
                                            case CellType.Error:
                                                break;
                                            default:
                                                dataRow[colIndex] = null;
                                                break;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            dtExcelData.Rows.Add(dataRow);
                        }
                        #endregion
                        ds.Tables.Add(dtExcelData);
                    }
                    #endregion
                }
            }
            File.Delete(filePath); //读取完后删除文件
            return ds;
        }
        #endregion

        #endregion

        #region NPOI_Helper-获取Excel表名集合
        /// <summary>
        /// NPOI_Helper-获取Excel表名集合
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>返回ArrayList</returns>
        public static ArrayList ReadExcelSheets(string filePath)
        {
            ArrayList alSheetNames = new ArrayList();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                {
                    fs.Position = 0;
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                    int tableCount = xssfworkbook.NumberOfSheets;
                    for (int sheetIndex = 0; sheetIndex < tableCount; sheetIndex++)
                    {
                        alSheetNames.Add(xssfworkbook.GetSheetName(sheetIndex));
                    }
                    alSheetNames.Remove("Macro1");  //如果Excel中有宏，就会多一个Macro1
                }
                else
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fs);
                    int tableCount = workbook.NumberOfSheets;
                    for (int sheetIndex = 0; sheetIndex < tableCount; sheetIndex++)
                    {
                        alSheetNames.Add(workbook.GetSheetName(sheetIndex));
                    }
                    alSheetNames.Remove("Macro1");  //如果Excel中有宏，就会多一个Macro1
                }
            }
            return alSheetNames;
        }
        #endregion

        #region NPOI_Helper-获取单元格类型(xlsx)
        /// <summary>
        /// NPOI_Helper-获取单元格类型(xlsx)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:
                    return null;
                case CellType.Boolean: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:
                    return cell.NumericCellValue;
                case CellType.String: //STRING:
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:
                default:
                    return "=" + cell.CellFormula;
            }
        }
        #endregion

        #region NPOI-删除指定行之后的所有行
        /// <summary>
        ///  NPOI-删除指定行之后的所有行
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>返回DataTable</returns>
        public static void DelAfterRows(string filePath, string sheetName, int rowIndex)
        {
            DataSet ds = ExcelSqlConnectionForImpData(filePath, -1);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[sheetName];
                if (dt.Rows.Count - 1 >= rowIndex)
                {
                    while (dt.Rows.Count - 1 >= rowIndex)
                    {
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    }
                    if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".xlsx")
                    {
                        DataSetToXLSX(ds, filePath);
                    }
                    else
                    {
                        DataSetToXls(ds, filePath);
                    }

                }
            }

        }
        #endregion

        #endregion

        #region 辅助

        #region NPOI_Helper-根据DataTable创建Sheet【xlsx】
        /// <summary>
        /// NPOI_Helper-根据DataTable创建Sheet【xlsx】
        /// </summary>
        /// <param name="workbook">工作薄</param>
        /// <param name="dt">数据源</param>
        public static void CreateSheetByDataTable(IWorkbook workbook, DataTable dt)
        {
            ISheet sheet = workbook.CreateSheet(dt.TableName);
            #region 表头
            //#region 表头样式
            //ICellStyle headStyle = workbook.CreateCellStyle();
            //headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //IFont font = workbook.CreateFont();
            //font.FontHeightInPoints = 10;
            //font.Boldweight = 700;
            //headStyle.FillBackgroundColor = 44;
            //headStyle.FillForegroundColor = 44;
            //headStyle.SetFont(font);
            //#endregion
            //IRow headerRow = sheet.CreateRow(0);
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    ICell cell = headerRow.CreateCell(i);
            //    cell.CellStyle = headStyle;
            //    cell.SetCellValue(dt.Columns[i].ColumnName);
            //}
            #endregion
            #region 数据
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                IRow row = sheet.CreateRow(rowIndex);
                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    ICell cell = row.CreateCell(colIndex);
                    cell.SetCellValue(dt.Rows[rowIndex][colIndex].ToString());
                }
            }
            #endregion
        }
        #endregion


        #endregion
    }
}
