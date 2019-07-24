using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProductController : ApiController
    {
        // POST api/values

        [HttpPost]
        [Route("api/products")]
        public void Post([FromBody]Product product)
        {

            product.insertProduct();
        }


        [HttpGet]
        [Route("api/products")]
        public IEnumerable<Product> Get()
        {
            try
            {
                List<Product> lo = new List<Product>();
                Product P = new Product();
                lo = P.Read();
                return lo;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }
        }


        //[HttpGet]
        //[Route("api/products")]
        //public IEnumerable<Product> GetProductsByMealType(string Date, string hour)
        //{
        //    try
        //    {
        //        List<Product> lp = new List<Product>();
        //        Product P = new Product();
        //        lp = P.ReadProducts(Date, hour);
        //        return lp;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("בעיה בשליפת נתונים");
        //    }
        //}


        //[HttpGet]
        //[Route("api/patients")]
        //public Patient Get(string Id)
        //{
        //    Patient p = new Patient();
        //    return p.GetPatient(Id);
        //}

        //[HttpPut]
        //[Route("api/patients")]
        //public void PUT([FromBody]Patient p)
        //{

        //    Patient P = new Patient();

        //    P.Update(p);

        //}





        //[HttpGet]
        //[Route("api/phone")]
        //public IEnumerable<Phone> Get(string Filter)
        //{
        //    try
        //    {
        //        List<Phone> Lp = new List<Phone>();
        //        Phone P = new Phone();
        //        Lp = P.ReadFilterOut(Filter);
        //        return Lp;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("בעיה בשליפת נתונים");
        //    }

        //}
    }
}
