//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmpMng.Models.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPerformanceReview
    {
        public int PerformanceId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public Nullable<decimal> ReviewScore { get; set; }
        public string ReviewNotes { get; set; }
    
        public virtual tblEmployee tblEmployee { get; set; }
    }
}
