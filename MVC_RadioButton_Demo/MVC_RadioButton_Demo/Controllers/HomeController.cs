using MVC_RadioButton_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_RadioButton_Demo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Company objCompany = new Company();
            return View(objCompany);
        }

        [HttpPost]
        public string Index(Company objcom)
        {
            if (string.IsNullOrEmpty(objcom.SelectedDepartment))
            {
                return "You didnt select any department";
            }
            else
            {
                return "you selected department with id ="+objcom.SelectedDepartment;
            }
        }
    }
}