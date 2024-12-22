using EmpMng.Models.DbContext;
using EmpMng.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmpMng.DAL
{
    public class DeptDAL
    {
        #region Common
        private readonly EmployeeManagmentEntities _context;

        public DeptDAL(EmployeeManagmentEntities context)
        {
            _context = context;
        }


        private SqlConnection GetConnectionSql()
        {
            //var connectionStringName = ConfigurationManager.ConnectionStrings["RBSYNERGYEntities"];
            //string connectionString = connectionStringName.ConnectionString;
            const string connectionString = @"Data Source = 36.255.70.172,1433; Initial Catalog = EmployeeManagment; user id=SA;password=9lg0%wSrqR;";
            try
            {
                SqlConnection conCheck = new SqlConnection(connectionString);
                return new SqlConnection(connectionString);
            }
            catch (Exception e)
            {
                throw new Exception("Error : " + e.Message);
            }
        }


        #endregion

        public List<DeptViewModel> GetAlldept()
        {
            try
            {

                _context.Database.CommandTimeout = 6000;
                string query = string.Format(@" SELECT  d.[DepartmentId]
                    ,[DepartmentName],[ManagerId],e.EmployeeName,[Budget]
                    FROM [EmployeeManagment].[dbo].[tblDepartment] d
                    JOIN [EmployeeManagment].[dbo].[tblEmployee] e ON d.[ManagerId]=e.EmployeeId ");
                var data = _context.Database.SqlQuery<DeptViewModel>(query).ToList();
                return data;
            }
            catch (Exception e)
            {
                //ExceptionLogging.SendErrorToText(e);
                return null;
            }

        }

        public DeptViewModel GetAdeptByName(string deptname)
        {
            try
            {

                _context.Database.CommandTimeout = 6000;
                string query = string.Format(@" SELECT [DepartmentName] FROM [EmployeeManagment].[dbo].[tblDepartment] where DepartmentName='{0}' ", deptname);
                var data =  _context.Database.SqlQuery<DeptViewModel>(query).FirstOrDefault();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public int AddOrEdit(int? deptId, string deptName, string budget,int employeeId)
        {
            var data =  GetAdeptByName(deptName);


            if (deptId >= 0)
            {                
                _context.Database.CommandTimeout = 6000;
                string Sql = " Update [EmployeeManagment].[dbo].[tblDepartment] set DepartmentName='" + deptName + "',Budget='"+ budget + "',[ManagerId]='" + employeeId + "' where DepartmentId='" + deptId + "' ";

                var savedata = _context.Database.ExecuteSqlCommand(Sql);
                return savedata;
            }
            else
            {
                if (data == null)
                {
                    _context.Database.CommandTimeout = 6000;
                    string Sql = " Insert into [EmployeeManagment].[dbo].[tblDepartment] " +
                                    "([DepartmentName],[Budget],[ManagerId]) " +
                                    "values ('" + deptName + "','" + budget + "','" + employeeId + "') ";

                    var savedata = _context.Database.ExecuteSqlCommand(Sql);
                    return savedata;
                }
                else
                {
                    return 0;
                }
                
            }


        }

        public class PaginatedResult<T>
        {
            public List<T> Data { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        }


        public PaginatedResult<EmpViewModel> GetEmployeesPaginated(int pageNumber, int pageSize, string searchTerm = null)
        {
            // Set command timeout (optional)
            _context.Database.CommandTimeout = 6000;

            // Parameters for the stored procedure
            var pageParameter = new SqlParameter("@Page", pageNumber);
            var pageSizeParameter = new SqlParameter("@PageSize", pageSize);
            var searchParameter = new SqlParameter("@SearchTerm", searchTerm ?? (object)DBNull.Value);

            // Call the stored procedure to get the paginated data
            var employees = _context.Database.SqlQuery<EmpViewModel>(
                "EXEC [dbo].[GetAllEmployee] @Page, @PageSize,@SearchTerm",
                pageParameter,
                pageSizeParameter,
                searchParameter
            ).ToList();

            //var totalCountParameter1 = new SqlParameter("@Page", pageNumber);
            //var totalCountParameter2 = new SqlParameter("@PageSize", pageSize);

            // Call the stored procedure again to get the total count
            var totalCount = _context.Database.SqlQuery<int>(
                "EXEC [dbo].[GetTotalEmployeeCount] @SearchTerm",
                new SqlParameter("@SearchTerm", searchTerm ?? (object)DBNull.Value)
            ).FirstOrDefault();

            // Return paginated result
            return new PaginatedResult<EmpViewModel>
            {
                Data = employees,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }



        public List<EmpViewModel> GetAllEmp()
        {
            try
            {

                _context.Database.CommandTimeout = 6000;
                string query = string.Format(@" SELECT [EmployeeId]
                  ,[EmployeeName],[Email],[Phone],[Position],[DepartmentName]
                  ,[JoinDate],[Status],[Deleted]
                FROM [EmployeeManagment].[dbo].[tblEmployee] e
                JOIN [EmployeeManagment].[dbo].[tblDepartment] d ON e.DepartmentId=d.DepartmentId ");
                var data = _context.Database.SqlQuery<EmpViewModel>(query).ToList();
                return data;
            }
            catch (Exception e)
            {
                //ExceptionLogging.SendErrorToText(e);
                return null;
            }

        }

        public EmpViewModel GetAnEmpByPhnOrEmail(string phone,string email)
        {
            try
            {

                _context.Database.CommandTimeout = 6000;
                string query = string.Format(@" SELECT [Email],[Phone] FROM [EmployeeManagment].[dbo].[tblEmployee] where [Phone]='{0}' or [Email]='{1}' ", phone,email);
                var data = _context.Database.SqlQuery<EmpViewModel>(query).FirstOrDefault();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public int AddOrEditEmp(int? empId, string empName, string email, string phone, string position,int? departmentId, DateTime joinDate,bool status)
        {
            var data = GetAnEmpByPhnOrEmail(phone,email);


            if (empId >= 0)
            {
                _context.Database.CommandTimeout = 6000;
                string Sql = "";
                if (status==true)
                {
                    Sql = " Update [EmployeeManagment].[dbo].[tblEmployee] set [EmployeeName]='" + empName + "',[Email]='" + email + "',[Phone]='" + phone + "',[Position]='" + position + "',[DepartmentId]='" + departmentId + "',[JoinDate]='" + joinDate + "',[Status]='" + status + "', [Deleted] = 0 where EmployeeId='" + empId + "' ";
                }
                else
                {
                    Sql = " Update [EmployeeManagment].[dbo].[tblEmployee] set [EmployeeName]='" + empName + "',[Email]='" + email + "',[Phone]='" + phone + "',[Position]='" + position + "',[DepartmentId]='" + departmentId + "',[JoinDate]='" + joinDate + "',[Status]='" + status + "', [Deleted] = 1 where EmployeeId='" + empId + "' ";
                }
                

                var savedata = _context.Database.ExecuteSqlCommand(Sql);
                return savedata;
            }
            else
            {
                if (data == null)
                {
                    _context.Database.CommandTimeout = 6000;
                    string Sql = " Insert into [EmployeeManagment].[dbo].[tblEmployee] " +
                                    "([EmployeeName],[Email],[Phone],[Position],[DepartmentId],[JoinDate],[Status]) " +
                                    "values ('" + empName + "','" + email + "','" + phone + "','" + position + "','" + departmentId + "','" + joinDate + "','" + status + "') ";

                    var savedata = _context.Database.ExecuteSqlCommand(Sql);
                    return savedata;
                }
                else
                {
                    return 0;
                }

            }


        }
        public int EmpDeleted(int? empId)
        {

            _context.Database.CommandTimeout = 6000;
            string Sql = " Update [EmployeeManagment].[dbo].[tblEmployee] set [Status]= 0 ,[Deleted]=1 where EmployeeId='" + empId + "' ";

            var savedata = _context.Database.ExecuteSqlCommand(Sql);
            return savedata;            

        }

        public List<DeptAvgScoreViewModel> GetAllEmpReview()
        {
            try
            {

                _context.Database.CommandTimeout = 6000;
                string query = string.Format(@" SELECT d.DepartmentName, ROUND(AVG(p.ReviewScore), 3) AS ReviewScore
                            FROM [EmployeeManagment].[dbo].[tblPerformanceReview] p
                            JOIN [EmployeeManagment].[dbo].[tblEmployee] e ON p.EmployeeId = e.EmployeeId
                            JOIN [EmployeeManagment].[dbo].[tblDepartment] d ON e.DepartmentId = d.DepartmentId
                            GROUP BY d.DepartmentName; ");
                var data = _context.Database.SqlQuery<DeptAvgScoreViewModel>(query).ToList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public int AddOrEditEmpReview(int employeeId, string reviewnote, int reviewScore, DateTime reviewDate)
        {
            try
            {

                _context.Database.CommandTimeout = 6000;
                string Sql = " Insert into [EmployeeManagment].[dbo].[tblPerformanceReview] " +
                                "([EmployeeId],[ReviewDate],[ReviewScore],[ReviewNotes]) " +
                                "values ('" + employeeId + "','" + reviewDate + "','" + reviewScore + "','" + reviewnote + "' ) ";

                var savedata = _context.Database.ExecuteSqlCommand(Sql);
                return savedata;

            }
            catch (Exception e)
            {

                return 0;
            }

                

        }
    }
}