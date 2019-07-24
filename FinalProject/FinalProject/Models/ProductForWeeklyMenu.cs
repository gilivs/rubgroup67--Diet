using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ProductForWeeklyMenu
    {
        public int NumProduct_menu { get; set; }
        public int IdProduct { get; set; }
        public int IdDay { get; set; }
        public int WeekNumber { get; set; }
        public int IdMealType { get; set; }
        public int IsBlender { get; set; }
        public int MenuNumber { get; set; }
        public int IdCategory { get; set; }
        public string NameProduct { get; set; }




        public ProductForWeeklyMenu(int _numProduct_menu, int _idProduct, int _idDay, int _weekNumber, int _idMealType, int _isBlender, int _menuNumber, int _idCategory, string _nameProduct)
        {
            IdProduct = _idProduct;
            IdDay = _idDay;
            WeekNumber = _weekNumber;
            IdMealType = _idMealType;
            IsBlender = _isBlender;
            MenuNumber = _menuNumber;
            IdCategory = _idCategory;
            NumProduct_menu = _numProduct_menu;
            NameProduct = _nameProduct;
        }

        public ProductForWeeklyMenu()
        {
        }

        public int insertProdForWeekMenu()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertProductForWeekMenu(this);
            return numAffected;
        }

        public List<ProductForWeeklyMenu> ReadProducts(string date, string hour)
        {

            DBservices db = new DBservices();

            return db.GetProductsByMealType("dietDBConnectionString", date, hour);

        }
        //public List<ProductForWeeklyMenu> Read()
        //{
        //    DBservices db = new DBservices();

        //    return db.GetProductForWeekMenu();
        //}
    }
}