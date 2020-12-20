using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Practice_Test.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  string EmailId { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public int Course { get; set; }
        public string CourseName { get; set; }
        public string Hobbies { get; set; }
        //public FileDetailsModel FileDetModel { get; set; }
        [Display(Name = "File")]
        public string FileName { get; set; }

        public byte[] FileContent { get; set; }


    }
}