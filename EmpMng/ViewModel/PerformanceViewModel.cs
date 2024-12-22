using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpMng.ViewModel
{
    public class PerformanceViewModel
    {
        public int PerformanceId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public  DateTime ReviewDate { get; set; }
        public decimal ReviewScore { get; set; }
        public string ReviewNotes { get; set; }

    }
}