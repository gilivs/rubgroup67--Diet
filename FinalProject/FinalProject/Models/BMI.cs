using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class BMI
    {

        public int IdBMI { get; set; }
        public int NumPatient { get; set; }
        public string Date { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public float Bmi { get; set; }

        public BMI(int _idBMI, int _numPatient, string _date, float _weight, float _height, float _BMI)
        {
            IdBMI = _idBMI;
            NumPatient = _numPatient;
            Date = _date;
            Weight = _weight;
            Height = _height;
            Bmi = _BMI;

        }

        public BMI()
        {

        }
        public List<BMI> GetBMI(int NumPatient)
        {
            DBservices dbs = new DBservices();
            return dbs.GetBMI("dietDBConnectionString", "[Bmi]", NumPatient);

        }

        //public int insertBMI()
        //{

        //    DBservices dbs = new DBservices();
        //    int numAffected = dbs.insertBMI(this);
        //    return numAffected;

        //}
    }

}