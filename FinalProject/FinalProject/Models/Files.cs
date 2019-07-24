using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Files
    {

        public string FileName { get; set; }
        public string Path { get; set; }


        public Files(string _fileName, string _path)
        {
            FileName = _fileName;
            Path = _path;
        }
        public Files()
        { }

        //public List<Files> GetFiles()
        //{
        //    DBservices db = new DBservices();
        //    List<Files> filesList = new List<Files>();
        //    filesList = db.GetFiles();
        //    return filesList;
        //}
    }
}