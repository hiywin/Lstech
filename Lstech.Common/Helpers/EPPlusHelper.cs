using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Lstech.Common.Helpers
{
    public class EPPlusHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static byte[] ExcelExport(string sheet, DataTable table)
        {
            var codes = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            byte[] fileContents;
            try
            {
                //获取表的列名
                var titles = new List<string>();
                foreach (DataColumn col in table.Columns)
                {
                    titles.Add(col.ColumnName);
                }
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheet);

                    for (int i = 0; i < titles.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = titles[i];
                    }

                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        DataRow row = table.Rows[j];
                        for (int k = 0; k < titles.Count; k++)
                        {
                            worksheet.Cells[codes[k] + (j + 2)].Value = row[k];
                        }

                    }

                    fileContents = package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return fileContents;
        }

        /// <summary>
        /// 单个excel导出多个sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dictable"></param>
        /// <returns></returns>
        public static byte[] ExcelExport(Dictionary<string, DataTable> dicTable)
        {
            var codes = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
                "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };
            byte[] fileContents;
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    foreach (var item in dicTable)
                    {
                        //获取表的列名
                        var titles = new List<string>();
                        foreach (DataColumn col in item.Value.Columns)
                        {
                            titles.Add(col.ColumnName);
                        }
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(item.Key);

                        for (int i = 0; i < titles.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = titles[i];
                        }

                        for (int j = 0; j < item.Value.Rows.Count; j++)
                        {
                            DataRow row = item.Value.Rows[j];
                            for (int k = 0; k < titles.Count; k++)
                            {
                                worksheet.Cells[codes[k] + (j + 2)].Value = row[k];
                            }

                        }
                    }

                    fileContents = package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return fileContents;
        }

        /// <summary>
        /// 读取Excel组装成DataTable列表
        /// </summary>
        /// <param name="stream">Excel流文件</param>
        /// <param name="titleIndex">标题行索引</param>
        /// <returns>Excel表中一个sheet对应一个DataTable</returns>
        public static List<DataTable> ExcelImport(Stream stream, int titleIndex)
        {
            List<DataTable> result = new List<DataTable>();
            try
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    var sheets = package.Workbook.Worksheets;
                    foreach (var sheet in sheets)
                    {
                        int rowCount = sheet.Dimension.Rows;
                        int colCount = sheet.Dimension.Columns;

                        #region 标题做成表头
                        var table = new DataTable();
                        var lstTitle = new List<string>();
                        for (int i = 1; i <= colCount; i++)
                        {
                            var title = sheet.Cells[titleIndex, i].Value.ToString();
                            table.Columns.Add(title, Type.GetType("System.String"));
                            lstTitle.Add(title);
                        }
                        #endregion

                        for (int i = titleIndex + 1; i <= rowCount; i++)
                        {
                            DataRow row = table.NewRow();
                            for (int j = 1; j <= colCount; j++)
                            {
                                row[lstTitle[j - 1]] = sheet.Cells[i, j].Value.ToString();
                            }
                            table.Rows.Add(row);
                        }

                        result.Add(table);
                    }

                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }
    }
}
