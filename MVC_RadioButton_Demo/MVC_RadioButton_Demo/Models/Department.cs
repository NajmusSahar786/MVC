using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_RadioButton_Demo.Models
{
    public class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public bool IsSelected { get; set; }
    }
}