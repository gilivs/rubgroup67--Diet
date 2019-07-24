using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalProject.Models;
//using System.Web.Http.Cors;

namespace FinalProject.Controllers
{
    // [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*",SupportsCredentials = true)]
    public class FoodReportController : ApiController
    {

        [HttpGet]
        [Route("api/foodReport")]
        public int Get()
        {
            return 1;
        }
        [HttpGet]
        [Route("api/therapistCommentReport")]
        public IEnumerable<FoodReport> GetTherapistComments( int NumPatient)
        {
            try
            {
                List<FoodReport> reports = new List<FoodReport>();
                FoodReport report = new FoodReport();
                reports = report.GetComments(NumPatient);
                return reports;
            }
            catch (Exception e)
            {
                throw new Exception(e + "בעיה בשליפת נתונים");
            }
        }
        //[HttpGet]
        //[Route("api/foodReport")]
        //public IEnumerable<FoodReport> GetTherapistComments(string Date, string Hour, int NumPatient)
        //{
        //    try
        //    {
        //        List<FoodReport> reports = new List<FoodReport>();
        //        FoodReport report = new FoodReport();
        //        reports = report.ReadFoodReport(Date, Hour, NumPatient);
        //        return reports;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e + "בעיה בשליפת נתונים");
        //    }
        //}


        [HttpPost]
        [Route("api/foodReport")]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Post([FromBody] FoodReport foodReport)
        {

            foodReport.InsertReport();
        }


        [HttpPost]
        [Route("api/foodReport")]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Post(string check)
        {
            FoodReport f = new FoodReport();
            f.InsertCheck();
        }
        //[HttpPut]
        //[Route("api/foodReport")]
        //public void PUT([FromBody]FoodReport r)
        //{

        //    FoodReport report = new FoodReport();

        //    report.UpdateReport(r);

        //}




    }
}