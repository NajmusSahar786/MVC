using MVC_Practice_Test.Models;
using MVC_Practice_Test.Repositories;
using MVC_Practice_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_Practice_Test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewModelDemo()
        {
            DataRepository _Data = new DataRepository();
            ViewModelDemo objViewModel = new ViewModelDemo();
            objViewModel.allCourses = _Data.GetAllCourse();
            objViewModel.allDepartments = _Data.GetAllDepartment();
            objViewModel.allEmployees = _Data.GetAllEmployee();
            return View(objViewModel);
        }
        public ActionResult PartialViewDemo()
        {
            DataRepository _Data = new DataRepository();
            List<Employee> allEmployees = _Data.GetAllEmployee();
            return View(allEmployees);
        }
        public ActionResult DepartmentPartialDemo(string EmpCode)
        {
            DataRepository _Data = new DataRepository();

            Employee _Emp = _Data.GetAllEmployee().FindAll(emp => emp.EmpCode == EmpCode).FirstOrDefault();

            IEnumerable<Department> cDepartment = _Data.GetAllDepartment().FindAll(dep => dep.DepCode == _Emp.DepCode);
            return PartialView("DepartmentPartialView", cDepartment);
        }
        public ActionResult CoursePartialDemo(string EmpCode)
        {
            DataRepository _Data = new DataRepository();
            Employee _Emp = _Data.GetAllEmployee().FindAll(emp => emp.EmpCode == EmpCode).FirstOrDefault();
            List<Course> _course = new List<Course>();
            foreach (Course courses in _Emp.Courses)
            {
                _course.Add(_Data.GetAllCourse().FindAll(course => course.CourseCode == courses.CourseCode).FirstOrDefault());
            }
            return PartialView("CoursePartialView", _course);
        }

        public ActionResult ViewDataDemo()
        {
            DataRepository _Data = new DataRepository();
            ViewData["Employees"] = _Data.GetAllEmployee();
            ViewData["Departments"] = _Data.GetAllDepartment();
            ViewData["Courses"] = _Data.GetAllCourse();
            return View();
        }
        public ActionResult ViewBagDemo()
        {
            DataRepository _Data = new DataRepository();
            ViewBag.Employees = _Data.GetAllEmployee();
            ViewBag.Departments = _Data.GetAllDepartment();
            ViewBag.Courses = _Data.GetAllCourse();
            return View();
        }
        public ActionResult TempDataDemo()
        {
            DataRepository _Data = new DataRepository();
            // TempData demo uses dummyData to get List<Employees> only one time
            // for subsequent request to get List<Employees> it will use TempData
            TempData["Employees"] = _Data.GetAllEmployee();
            // This will keep Employees data untill next request is served
            TempData.Keep("Employees");
            TempData["Departments"] = _Data.GetAllDepartment();
            TempData.Keep("Departments");
            TempData["Courses"] = _Data.GetAllCourse();
            TempData.Keep("Courses");
            return View();
        }
        public ActionResult TupleDemo()
        {
            DataRepository _Data = new DataRepository();
            //Passing multiple Models in View using Tuple
            var allToupleData = new Tuple<List<Employee>, List<Department>, List<Course>>
                (
                _Data.GetAllEmployee(), _Data.GetAllDepartment(), _Data.GetAllCourse()
                );
            //we had defined a tuple which is having lists of Employees, Departments and Courses. 
            //After fill dummy data, we are passing this tuple to the View.
            return View(allToupleData);
            
        }
        public ActionResult HtmlRawDemo()
        {
            StringBuilder str = new StringBuilder();
            str.Append("<div>");
            str.Append("<table>");
            str.Append("<tr>");
            str.Append("<th>ID</th>");
            str.Append("<th>Name</th>");
            str.Append("<th>Subject</th>");
            str.Append("</table>");
            str.Append("</div>");
            TempData["str"] = str;
            TempData.Keep("str");
            return View();
        }
    }
}
