using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
