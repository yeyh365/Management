using Management.Domain.DBFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{

    public class LogicDeletableEntity : IDeleteFilter, IEntityCreate,IEntityUpdate
    {

        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string UpdataBy { get; set; }
        public DateTime? UpdataData { get; set; }
    }
}
