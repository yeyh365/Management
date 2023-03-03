using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    [Table("workflow_records")]
    public class WorkflowRecords
    {
        public string Id { get; set; }
        public string WorkflowName { get; set; }
        public string AssigneeId { get; set; }
        public DateTime HandleDate { get; set; }
        public string Remarks { get; set; }
        public string WorkflowId { get; set; }
        public int AuditStatus { get; set; }
        public int IsAudit { get; set; }
        public string FlowSerialnunber { get; set; }
        public string DocumentCode { get; set; }
    }
}
