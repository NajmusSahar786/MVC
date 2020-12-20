using MVC_Practice_Test.Models;
using MVC_Practice_Test.Repositories;
using MVC_Practice_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Practice_Test.Controllers
{
    public class EmployeeController : Controller
    {
        DataRepository _Data = new DataRepository();

        [HttpGet]
        public FileResult DownLoadFile(int ID)
        {
            List<EmployeeModel> ObjFiles = _Data.GetEmployeesList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(ID)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }
        [HttpGet]
        public PartialViewResult GetAllEmployees()
        {
            List<EmployeeModel> DetList = _Data.GetEmployeesList();
            return PartialView("GetAllEmployees", DetList);
        }
        // GET: Employee
        public ActionResult Index()
        {
            RegistrationViewModel objViewModel = new RegistrationViewModel();
            objViewModel.GenderList = _Data.GetGenderList();
     
            return View(objViewModel);
        }
    
        [HttpGet]
        public ActionResult AddEmployee()
        {
            RegistrationViewModel objViewModel = new RegistrationViewModel();
            objViewModel.GenderList = _Data.GetGenderList();
            objViewModel.CourseList = _Data.GetAllCourse();
            return View(objViewModel);
        }

        [HttpPost]
        public ActionResult AddEmployee(RegistrationViewModel model)
        {
            RegistrationViewModel objmodel = new RegistrationViewModel();

            objmodel.Email = model.Email;
            objmodel.Name = model.Name;
            objmodel.SelectedGender = model.SelectedGender;
            objmodel.SelectedCourse = model.SelectedCourse;
            objmodel.ResumeList = model.ResumeList;
            string FileExt = Path.GetExtension(model.ResumeList.files.FileName).ToUpper();
            Stream str = model.ResumeList.files.InputStream;
            BinaryReader br = new BinaryReader(str);
            Byte[] FileData = br.ReadBytes((Int32)str.Length);
            FileDetailsModel filemodel = new FileDetailsModel();
            filemodel.FileName = model.ResumeList.files.FileName;
            filemodel.FileContent = FileData;
            objmodel.FileList = filemodel;
           
            if (ModelState.IsValid)
            {
                DataRepository _repo = new DataRepository();
                _repo.AddEmpDetails(objmodel);
            }
            // return View();
            return RedirectToAction("AddEmployee");
        }
    }
}