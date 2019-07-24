using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Product
    {

        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int IdCategory { get; set; }
        public string MainGroup { get; set; }
        public float Calories { get; set; }
        public float Weight { get; set; }
        public string CategoryName { get; set; }
        




        public Product(int _idproduct, string _nameproduct, int _idcategory, string _maingroup,  float _calories, float _weight, string _categoryName)
        {
            IdProduct = _idproduct;
            NameProduct = _nameproduct;
            IdCategory = _idcategory;
            MainGroup = _maingroup;
            Calories = _calories;
            Weight = _weight;
            CategoryName = _categoryName;
           


        }

        public Product()
        {
        }

        public int insertProduct()
        {

            DBservices dbs = new DBservices();
            int numAffected = dbs.insertProduct(this);
            return numAffected;

        }



        public List<Product> Read()
        {

            DBservices db = new DBservices();

            return db.GetProducts("dietDBConnectionString");

        }
    

        //public Patient GetPatient(string Id)
        //{
        //    DBservices dbs = new DBservices();
        //    Patient patient = dbs.GetPatient(Id);
        //    return patient;
        //}

        //public int Update(Patient p)
        //{
        //    DBservices dbs = new DBservices();
        //    int numAffected = dbs.Update(p);

        //    return numAffected;
        //}



    }
}
