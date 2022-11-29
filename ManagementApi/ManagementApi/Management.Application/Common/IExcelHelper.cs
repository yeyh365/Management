using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Common
{
    public interface IExcelHelper
    {

        /// <summary>
        /// 导出Excel columns={propName,columnName}
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columns">Excel和T属性映射关系{propName,columnName}</param>
        /// <returns></returns>
        byte[] Export<T>(List<T> data, Dictionary<string, string> columns, string sheetName = null);
    }
}
