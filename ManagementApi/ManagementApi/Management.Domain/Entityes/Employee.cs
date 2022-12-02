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
    [Table("EMPLOYEE")]
    public class Employee : LogicDeletableEntity, IEntity<int>
    {
        [Key]
        [Column("ID", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get ; set ; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentNumber { get; set; }
        public int PositionNumber { get; set; }

        public string CardId { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string GeneralManagerId { get; set; }



    }
}
