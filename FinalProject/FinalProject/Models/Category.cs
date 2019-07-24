using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Category
    {
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }

        public List<Category> ReadCategory()
        {
            DBservices dbs = new DBservices(); 
            return dbs.ReadCategory("dietDBConnectionString", "CategoryMeal");
        }
        //
    }
}