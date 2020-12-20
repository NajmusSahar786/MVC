using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Practice_Test.Models
{
    public class Hobbies
    {
        public int HobbyID { get; set; }
        public string HobbyName { get; set; }
        public bool Checked
        {
            get;
            set;
        }
    }
}