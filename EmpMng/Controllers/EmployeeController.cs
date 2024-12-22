using EmpMng.DAL;
using EmpMng.Models.DbContext;
using EmpMng.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpMng.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DeptDAL _deptDAL;
        public EmployeeController()
        {
            _deptDAL = new DeptDAL(new EmployeeManagmentEntities());

        }
        // GET: Employee
        
        public ActionResult Index()
        {
            List<DeptViewModel> deptList = _deptDAL.GetAlldept();

            IEnumerable<SelectListItem> selectListItems = deptList.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.DepartmentName
            });

            ViewBag.DepartmentList = selectListItems;
            //ViewData["DepartmentList"] = _deptDAL.GetAlldept();

            return View();
        }

        public JsonResult GetEmployeesPaginated(int pageNumber, int pageSize)
        {
            var result = _deptDAL.GetEmployeesPaginated(pageNumber, pageSize);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEmp(int page = 1, int pageSize = 2)
        {

            page = page < 1 ? 1 : page;
            var paginatedData = _deptDAL.GetEmployeesPaginated(page, pageSize);

            // Return the data as JSON
            return Json(new
            {
                Data = paginatedData.Data,          // List of employee data for current page
                TotalCount = paginatedData.TotalCount, // Total number of employees
                PageNumber = paginatedData.PageNumber, // Current page number
                PageSize = paginatedData.PageSize     // Number of records per page
            }, JsonRequestBehavior.AllowGet);

            //var data = _deptDAL.GetAllEmp();
            ////return View(data);            
            //var returnObj = new
            //{
            //    Data = data,
            //};
            //return new JsonResult()
            //{
            //    Data = returnObj,
            //    MaxJsonLength = 86753090,
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //};

        }

        public ActionResult AddOrEditEmp(int? empId, string empName, string email, string phone, string position, int? departmentId, DateTime joinDate, bool status)//WORK
        {
            var data = _deptDAL.AddOrEditEmp(empId, empName, email, phone, position, departmentId, joinDate, status);

            var returnObj = new
            {
                Data = data,
            };
            return new JsonResult()
            {
                Data = returnObj,
                MaxJsonLength = 86753090,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return null;

        }

        public ActionResult EmpDeleted(int? empId)//WORK
        {
            var data = _deptDAL.EmpDeleted(empId);

            var returnObj = new
            {
                Data = data,
            };
            return new JsonResult()
            {
                Data = returnObj,
                MaxJsonLength = 86753090,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return null;

        }
    }
}