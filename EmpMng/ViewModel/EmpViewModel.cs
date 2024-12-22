using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpMng.ViewModel
{
    public class EmpViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }
}