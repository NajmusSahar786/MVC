using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Practice_Test.Models
{
    public class Employee
    {
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int DepCode { get; set; }
        public List<Course> Courses { get; set; }
    }
}