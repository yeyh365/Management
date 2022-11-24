using Management.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    [Table("POSITTION")]
    public class Posittion : IEntity<int>
    {
        [Key]
        [Column("ID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PosititonNumber { get; set; }
        public string DepartmentNumber { get; set; }
        public string PosititonName { get; set; }

    }
}
