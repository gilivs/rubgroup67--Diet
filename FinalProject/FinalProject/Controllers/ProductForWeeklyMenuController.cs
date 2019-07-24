using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProductForWeeklyMenuController : ApiController
    {
        // write the action methods here
        //POST api/values
        //[HttpPost]
        //[Route("api/productForWeeklyMenu")]
        //public void Post([FromBody]ProductForWeeklyMenu prodWeekMenu)
        //{
        //    prodWeekMenu.insertProdForWeekMenu();
        //}


        //[HttpGet]
        //[Route("api/productForWeeklyMenu")]
        //public IEnumerable<ProductForWeeklyMenu> Get()
        //{
        //    try
        //    {
        //        List<ProductForWeeklyMenu> lproductMenu = new List<ProductForWeeklyMenu>();
        //        ProductForWeeklyMenu PForWeeklyMenu = new ProductForWeeklyMenu();
        //        lproductMenu = PForWeeklyMenu.Read();
        //        return lproductMenu;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("בעיה בשליפת נתונים");
        //    }
        //}

        [HttpPost]
        [Route("api/productForWeeklyMenu")]
        public void Post([FromBody]List<ProductForWeeklyMenu> productList)
        {
            foreach (var item in productList)
            {
                item.insertProdForWeekMenu();
            }

        }

        [HttpGet]
        [Route("api/productForWeeklyMenu")]
        public IEnumerable<ProductForWeeklyMenu> GetProductsByMealType(string Date, string hour)
        {
            try
            {
                List<ProductForWeeklyMenu> lp = new List<ProductForWeeklyMenu>();
                ProductForWeeklyMenu P = new ProductForWeeklyMenu();
                lp = P.ReadProducts(Date, hour);
                return lp;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }
        }
    }
}
