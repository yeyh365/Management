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
    [Table("MENU")]
    public class Menu : LogicDeletableEntity, IEntity<int>
    {
        [Key]
        [Column("ID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get ; set ; }
        public string MenuName { get; set; }
        public int PermissionId { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public string Style { get; set; }
        public int ParentId { get; set; }

    }
}
