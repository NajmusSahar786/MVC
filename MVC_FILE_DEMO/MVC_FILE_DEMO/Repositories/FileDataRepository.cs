using Dapper;
using MVC_FILE_DEMO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_FILE_DEMO.Repositories
{
    public class FileDataRepository
    {
        public SqlConnection con;
        public string constr;

        public void SaveFileDetails(FileDetailsModel objDet)
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);

            SqlParameter[] prm = new SqlParameter[2];
            prm[0] = new SqlParameter("@FileName", objDet.FileName);
            prm[1] = new SqlParameter("@FileContent", objDet.FileContent);
            con.Open();
            SqlCommand cmd = new SqlCommand("AddFileDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FileName", objDet.FileName);
            cmd.Parameters.AddWithValue("@FileContent", objDet.FileContent);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<FileDetailsModel> GetFileList()
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
            List<FileDetailsModel> _fileList = new List<FileDetailsModel>();
            con.Open();
            _fileList = SqlMapper.Query<FileDetailsModel>(con, "GetFileDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return _fileList;
        }
    }
}