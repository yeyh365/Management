using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    [Table("workflow_order")]
    public class WorkflowOrder
    {
        public string Id { get; set; }
        public string DocumentCode { get; set; }
        public DateTime DocumentTime { get; set; }
        public string Status { get; set; }
        public string CreateUser { get; set; }
        public string IsDelete { get; set; }
        public string Mark { get; set; }
        public string WorkflowId { get; set; }

    }
}
