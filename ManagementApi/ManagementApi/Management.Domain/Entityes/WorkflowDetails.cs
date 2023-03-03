using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    [Table("workflow_details")]
    public class WorkflowDetails
    {
        public string Id { get; set; }
        public string WorkflowId { get; set; }
        public int FlowSerialnuber { get; set; }
        public string FlownodName { get; set; }
        public string PostId { get; set; }
        public string ProcessRole { get; set; }
    }
}
