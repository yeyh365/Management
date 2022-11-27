using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Dto
{
    public class LogonUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
    }
}
