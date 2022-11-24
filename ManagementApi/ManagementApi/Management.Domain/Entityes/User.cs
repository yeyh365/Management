using Management.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entityes
{
    [Table("USER", Schema = "ADMIN")]
    public class User : LogicDeletableEntity, IEntity<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("ID",TypeName="int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Mobile { get; set; }
        public string Email  { get; set; }
        public string Picture { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }


    }
}
