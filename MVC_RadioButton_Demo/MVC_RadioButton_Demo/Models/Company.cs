using MVC_RadioButton_Demo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_RadioButton_Demo.Models
{
    public class Company
    {
        public string SelectedDepartment { get; set; }
        public List<Department> Departments
        {
            get
            {
                DataRepository db = new DataRepository();
                return db.GetDepartmentList();
            }
         }
    }
}