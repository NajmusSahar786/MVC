using Dapper;
using MVC_RadioButton_Demo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_RadioButton_Demo.Repositories
{
    public class DataRepository
    {
        public SqlConnection con;
        public string constr;

        public List<Department> GetDepartmentList()
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
            List<Department> _empList = new List<Department>();
            con.Open();
            _empList = SqlMapper.Query<Department>(con, "GetDepartments", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return _empList;
        }
    }
}