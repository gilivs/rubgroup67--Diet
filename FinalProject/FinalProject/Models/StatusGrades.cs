using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class StatusGrades
    {
        public StatusGrades(double _bmi, double _bloode, double _eatting)
        {
            BMI = _bmi;
            Bloode = _bloode;
            Eatting = _eatting;
        }
        public double BMI { get; set; }
        public double Bloode { get; set; }
        public double Eatting { get; set; }
    }
}