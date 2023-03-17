using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Dto
{
    public class WorkflowMenuDto: SearchDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 申请人ID
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 申请人账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 申请标题
        /// </summary>
        public string WorkflowTitle { get; set; }
        /// <summary>
        /// 申请内容
        /// </summary>
        public string WorkflowBoby { get; set; }
        /// <summary>
        /// 流程Id
        /// </summary>
        public string WorkflowId { get; set; }
        public string DocumentCode { get; set; }
        public DateTime DocumentTime { get; set; }
        public string Status { get; set; }
        public string CreateUser { get; set; }
        public string IsDelete { get; set; }
        public string Mark { get; set; }
        public string WorkflowName { get; set; }
        public string AssigneeId { get; set; }
        public DateTime HandleDate { get; set; }
        public string Remarks { get; set; }
        public int AuditStatus { get; set; }
        public int IsAudit { get; set; }
        public string FlowSerialnunber { get; set; }
        public int Count { get; set; }
    }
}
