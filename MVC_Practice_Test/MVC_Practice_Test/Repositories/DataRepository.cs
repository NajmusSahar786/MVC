using Dapper;
using MVC_Practice_Test.Models;
using MVC_Practice_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_Practice_Test.Repositories
{
    public class DataRepository
    {
        public SqlConnection con;
        public string constr;

        public List<Gender> GetGenderList()
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
            List<Gender> _List = new List<Gender>();
            con.Open();
            _List = SqlMapper.Query<Gender>(con, "GetGenders", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return _List;
        }
        public List<EmployeeModel> GetEmployeesList()
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
            List<EmployeeModel> _empList = new List<EmployeeModel>();
            con.Open();
            _empList = SqlMapper.Query<EmployeeModel>(con, "GetEmployees", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return _empList;
        }

        public void AddEmpDetails(RegistrationViewModel _objModel)
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("AddNewEmpDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", _objModel.Name);
            cmd.Parameters.AddWithValue("@EmailId", _objModel.Email);
            cmd.Parameters.AddWithValue("@FileName", _objModel.FileList.FileName);
            cmd.Parameters.AddWithValue("@FileContent", _objModel.FileList.FileContent);
            cmd.Parameters.AddWithValue("@GenderId", _objModel.SelectedGender);
            cmd.Parameters.AddWithValue("@CourseId", _objModel.SelectedCourse);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Hobbies> GetAllHobbies()
        {
            return new List<Hobbies>{
                new Hobbies{HobbyID=1, HobbyName="Singing", Checked = false},
                new Hobbies{HobbyID=2, HobbyName="Basket Ball.", Checked = false},
                new Hobbies{HobbyID=3, HobbyName="Listening Music", Checked = false},
                new Hobbies{HobbyID=4, HobbyName="Dancing", Checked = false},
                new Hobbies{HobbyID=5, HobbyName="Cooking", Checked = false},
                new Hobbies{HobbyID=6, HobbyName="Walking", Checked = false}
            };
        }
        public List<Employee> GetAllEmployee()
        {
            return new List<Employee>
            {
                new Employee{EmpCode="emp1",EmpName="Suresh Kumar",Courses=
                    new List<Course>{new Course{CourseCode=2, CourseName="M.C.A."},
                                     new Course{CourseCode=3, CourseName="B.C.A."}
                    },
                    DepCode=1
                },
                new Employee{EmpCode="emp2",EmpName="Rajesh Kumar",Courses=
                    new List<Course>{new Course{CourseCode=1, CourseName="B.Tech"},
                                     new Course{CourseCode=4, CourseName="M.Tech"}
                    },
                    DepCode=4
                },
                new Employee{EmpCode="emp3",EmpName="Mahesh Kumar",Courses=
                    new List<Course>{new Course{CourseCode=1, CourseName="B.Tech"},
                                     new Course{CourseCode=4, CourseName="M.Tech"}
                    },
                    DepCode=3
                },
                new Employee{EmpCode="emp4",EmpName="Kamlesh Kumar",Courses=
                    new List<Course>{new Course{CourseCode=2, CourseName="M.C.A."},
                                     new Course{CourseCode=4, CourseName="M.Tech"}
                    },
                    DepCode=2
                }
            };
        }


        public List<Course> GetAllCourse()
        {
            return new List<Course>{
                new Course{CourseCode=1, CourseName="B.Tech"},
                new Course{CourseCode=2, CourseName="M.C.A."},
                new Course{CourseCode=3, CourseName="B.C.A."},
                new Course{CourseCode=4, CourseName="M.Tech"}
            };
        }


        public List<Department> GetAllDepartment()
        {
            return new List<Department>
            {
                new Department{ DepCode=1,DepName="Computer Science & Engg."},
                new Department{ DepCode=2,DepName="Electronics"},
                new Department{ DepCode=3,DepName="Mechanical Engg."},
                new Department{ DepCode=4,DepName="Information Technology"}
            };
        }
       
    }
}