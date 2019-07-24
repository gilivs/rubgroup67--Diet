using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Sensitivities
    {
        public int NumSensitivity { get; set; }
        public string NameSensitivity { get; set; }

        public List<Sensitivities> Read()
        {
            DBservices dbs = new DBservices(); 
            return dbs.ReadSen("dietDBConnectionString", "Sensitivities");
        }
    }
}