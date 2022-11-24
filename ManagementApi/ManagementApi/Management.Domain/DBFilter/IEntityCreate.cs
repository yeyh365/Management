using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.DBFilter
{
    public interface IEntityCreate
    {
        /// <summary>
        /// 创建人的ID
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// 创建的时间
        /// </summary>
        DateTime? CreatedDate { get; set; }
    }
}
