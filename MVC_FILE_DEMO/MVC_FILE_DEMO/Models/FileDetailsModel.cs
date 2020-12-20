using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_FILE_DEMO.Models
{
    public class FileDetailsModel
    {
        public int Id { get; set; }
        //[Display(Name = "Uploaded File")]
        [Display(Name = "File")]
        public string FileName { get; set; }
        [Display(Name = "SubmitDate")]
        public DateTime? SubmittedDate { get; set; }

        public byte[] FileContent { get; set; }
    }
}