using iTextSharp.text;
using iTextSharp.text.pdf;
using MVC_FILE_DEMO.Models;
using MVC_FILE_DEMO.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC_FILE_DEMO.Controllers
{
    public class HomeController : Controller
    {
        FileDataRepository _data = new FileDataRepository();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase files)
        {
            string FileExt = Path.GetExtension(files.FileName).ToUpper();
            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader br = new BinaryReader(str);
                Byte[] FileData = br.ReadBytes((Int32)str.Length);
                FileDetailsModel _model = new FileDetailsModel();
                _model.FileName = files.FileName;
                _model.FileContent = FileData;
                _data.SaveFileDetails(_model);
                ViewBag.FileStatus = "File Uploaded Successfully";
                return RedirectToAction("FileUpload");

            }
            else
            {
                ViewBag.FileStatus = "Invalid file format.";
                return View();
            }
         }

        [HttpGet]
        public FileResult DownLoadFile(int ID)
        {
            List<FileDetailsModel> ObjFiles = _data.GetFileList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(ID)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<FileDetailsModel> DetList = _data.GetFileList();
            return PartialView("FileDetails", DetList);
        }
        
      
        [HttpGet]
        public FileResult DownLoadExport()
        {
            string Filename = string.Empty;
            List<FileDetailsModel> lst = _data.GetFileList();
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("Id", "Id");
            columns.Add("FileName", "FileName");
            columns.Add("SubmittedDate", "SubmittedDate");
            columns.Add("FileContent", "FileContent");
            Filename = "Test" + "_" + DateTime.Now.ToString("ddMMyyyyhhmm") + ".pdf";
            ExportToPDF(Filename, lst, columns,"TestPDFReport");
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + Filename);
            return File(filePath, "application/pdf", Filename);
        }
        public static DataTable ConvertListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //values[i] = Props[i].GetValue(item, null);
                    if (Props[i].GetValue(item, null).GetType() == System.Type.GetType("System.DateTime"))
                        values[i] = Convert.ToDateTime(Props[i].GetValue(item, null)).ToString("dd/MM/yyyy");
                    else
                        values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
      
        public static void ExportToPDF<T>(string FileName, List<T> lstobject, Dictionary<string, string> columns, string headerName)
        {
            //Document doc = new Document(PageSize.A4);
            Document doc = new Document(PageSize.A4_LANDSCAPE);
            doc.SetPageSize(iTextSharp.text.PageSize.A4_LANDSCAPE.Rotate());
            try
            {
                using (PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + FileName), FileMode.Create)))
                {
                    doc.Open();

                    if (!string.IsNullOrEmpty(headerName))
                    {
                        Paragraph para = new Paragraph("" + headerName + " \n \n", new Font(Font.FontFamily.TIMES_ROMAN, 15, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        doc.Add(para);
                    }
                    PdfPTable tbl = new PdfPTable(columns.Count);
                    tbl.TotalWidth = columns.Count * 80;
                    float[] widths = new float[columns.Count];

                    for (int index = 0; index < columns.Count; index++)
                        widths[index] = 80f;

                    tbl.SetWidths(widths);
                    Font myfont = FontFactory.GetFont("TIMES_ROMAN", 12, Font.BOLD);
                    Font myfont2 = FontFactory.GetFont("TIMES_ROMAN", 10);
                    DataTable dt = ConvertListToDataTable<T>(lstobject);

                    foreach (var item in columns)
                    {
                        tbl.AddCell(new Phrase(item.Value, myfont));
                    }
               
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (var column in columns)
                        {
                            tbl.AddCell(new Phrase(row[column.Key].ToString(), myfont2));
                        }
                    }
                    doc.Add(tbl);
                    doc.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}