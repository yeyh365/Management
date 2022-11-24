using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.DBFilter
{
    public interface IEntityUpdate
    {
        /// <summary>
        /// 更新人的Id
        /// </summary>
        string UpdataBy { get; set; }
        /// <summary>
        /// 更新的时间
        /// </summary>
        DateTime? UpdataData { get; set; }
    }
}
