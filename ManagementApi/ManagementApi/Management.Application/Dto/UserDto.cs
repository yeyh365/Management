using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public DateTime Last_LoginTime { get; set; }
        public int Count { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
