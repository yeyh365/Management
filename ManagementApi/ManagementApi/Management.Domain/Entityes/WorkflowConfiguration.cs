using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    [Table("workflow_configuration")]
    public class WorkflowConfiguration
    {
        public string Id { get; set; }
        public string WorkflowCode { get; set; }
        public string WorkflowName { get; set; }
        public string IsStart { get; set; }
        public string CreateUser { get; set;}
        public DateTime CreateTime { get; set; }

        public string IsDelete { get; set;}
    }
}
