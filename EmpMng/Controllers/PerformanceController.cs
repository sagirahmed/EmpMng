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
    public class PerformanceController : Controller
    {
        private readonly DeptDAL _deptDAL;
        public PerformanceController()
        {
            _deptDAL = new DeptDAL(new EmployeeManagmentEntities());

        }
        // GET: Performance
        public ActionResult Index()
        {
            List<EmpViewModel> deptList = _deptDAL.GetAllEmp();

            IEnumerable<SelectListItem> selectListItems = deptList.Select(d => new SelectListItem
            {
                Value = d.EmployeeId.ToString(),
                Text = d.EmployeeName
            });

            ViewBag.EmployeeList = selectListItems;

            return View();
        }

        public ActionResult GetAllEmpReview()
        {
            var data = _deptDAL.GetAllEmpReview();
            //return View(data);            
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

        }

        public ActionResult AddOrEditEmpReview(int employeeId, string reviewnote,int reviewScore, DateTime reviewDate)//WORK
        {
            var data = _deptDAL.AddOrEditEmpReview(employeeId, reviewnote, reviewScore, reviewDate);

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