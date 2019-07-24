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
    public class BMIController : ApiController
    {
        //[HttpPost]
        //[Route("api/BMI")]
        //public void Post([FromBody]BMI bmi)
        //{

        //    bmi.insertBMI();
        //}


        [HttpGet]
        [Route("api/BMI")]
        public IEnumerable<BMI>Get(int NumPatient)
        {
            BMI b = new BMI();
            return b.GetBMI(NumPatient);
        }


    }
}
