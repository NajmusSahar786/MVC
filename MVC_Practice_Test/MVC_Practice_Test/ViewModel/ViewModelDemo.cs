using MVC_Practice_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Practice_Test.ViewModel
{
    public class ViewModelDemo
    {
        public List<Employee> allEmployees { get; set; }
        public List<Course> allCourses { get; set; }
        public List<Department> allDepartments { get; set; }
    }
}