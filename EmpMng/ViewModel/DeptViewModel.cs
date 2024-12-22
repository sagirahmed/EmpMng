using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpMng.ViewModel
{
    public class DeptViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }
        public string EmployeeName { get; set; }
        public int ManagerId { get; set; }

    }
}