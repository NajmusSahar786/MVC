using MVC_Practice_Test.Models;
using MVC_Practice_Test.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Practice_Test.ViewModel
{
    public class RegistrationViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string SelectedGender { get; set; }
        public List<Gender> GenderList { get; set; }

        public EmpModel ResumeList { get; set; }
        public FileDetailsModel FileList { get; set; }

        public string SelectedCourse { get; set; }
        public List<Course> CourseList { get; set; }
        public List<Hobbies> HobbiesList { get; set; }
       
        //public List<Gender> GenderList
        //{
        //    get
        //    {
        //        DataRepository db = new DataRepository();
        //        return db.GetGenderList();
        //    }
        //}
       
       
    }
}