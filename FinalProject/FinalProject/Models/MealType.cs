using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class MealType
    {

        public int IdMealType { get; set; }
        public string NameMealType { get; set; }

        public MealType(int _idMealType, string _nameMealType)
        {
            IdMealType = _idMealType;
            NameMealType = _nameMealType;

        }

        public MealType()
        {

        }
        public List<MealType> GetMT()
        {
            DBservices dbs = new DBservices();
            return dbs.GetMT("dietDBConnectionString", "MealType");

        }

      
    }

}