using EmpMng.DAL;
using EmpMng.Models.DbContext;
using EmpMng.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmpMng.Controllers
{
    public class DeptController : Controller
    {
        private readonly DeptDAL _deptDAL;
        public DeptController()
        {
            _deptDAL = new DeptDAL(new EmployeeManagmentEntities());

        }
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
        public ActionResult GetAlldept()
        {
            var data = _deptDAL.GetAlldept();
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
        public ActionResult GetAdeptByName(string BPPhoneNumber)
        {
            var data = _deptDAL.GetAdeptByName(BPPhoneNumber);

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
        public ActionResult AddOrEdit(int? deptId, string deptName, string budget,int employeeId)//Start to WORK
        {
            var data = _deptDAL.AddOrEdit(deptId, deptName, budget, employeeId);

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