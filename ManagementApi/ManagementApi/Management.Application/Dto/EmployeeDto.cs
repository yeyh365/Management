using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentNumber { get; set; }
        public int PositionNumber { get; set; }
        public string CardId { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Count { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
    }
}
