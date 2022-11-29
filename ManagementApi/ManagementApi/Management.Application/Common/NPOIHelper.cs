using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Common
{
    public class NPOIHelper : IExcelHelper
    {
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="columns"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public byte[] Export<T>(List<T> data, Dictionary<string, string> columns, string sheetName = null){
            columns = columns ?? new Dictionary<string, string>();
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName ?? "Sheet1");
            ExportToSheet<T>(sheet, data, columns);
            var ms = new MemoryStream();
            workbook.Write(ms);
            return ms.ToArray();
        }

        private void ExportToSheet<T>(ISheet sheet, List<T> data, Dictionary<string, string> columns)
        {
            IRow headerRow = sheet.CreateRow(0);
            IDataFormat dataFormatCustom = sheet.Workbook.CreateDataFormat();
            ICellStyle dateStyle = sheet.Workbook.CreateCellStyle();
            dateStyle.DataFormat = dataFormatCustom.GetFormat("yyyy-MM-dd HH:mm:ss");

            var dicIndexer = new Dictionary<int, PropertyInfo>();
            var props = typeof(T).GetProperties();

            //设置标题行
            int i = 0;
            foreach (var item in columns)
            {
                headerRow.CreateCell(i).SetCellValue(item.Value);

                //匹配属性
                var prop = props.FirstOrDefault(p => p.Name.ToLower() == item.Key.ToLower());
                if (prop != null)
                {
                    dicIndexer.Add(i, prop);
                    i++;
                }
            }

            //填充数据
            for (i = 0; i < data.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                foreach (var item in dicIndexer)
                {
                    var value = item.Value.GetValue(data[i]);
                    if (value != null)
                    {
                        if (value is DateTime)
                        {
                            var cell = row.CreateCell(item.Key);
                            cell.SetCellValue((DateTime)value);
                            cell.CellStyle = dateStyle;
                        }
                        else
                        {
                            row.CreateCell(item.Key).SetCellValue(value.ToString());
                        }
                    }
                }
            }
        }

    }
}
