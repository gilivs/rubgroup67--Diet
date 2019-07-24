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

    public class LaboratoryTestsController : ApiController
    {

        //POST api/values
        [HttpPost]
        [Route("api/laboratoryTests")]
        public void Post([FromBody]LaboratoryTests lt)
        {
            lt.insertLt();
        }

        [HttpPut]
        [Route("api/laboratoryTests")]
        public void Put([FromBody]LaboratoryTests lt)
        {
            lt.PutLt();

        }

        [HttpGet]
        [Route("api/laboratoryTests")]
        public IEnumerable<LaboratoryTests> Get()
        {
            try
            {
                List<LaboratoryTests> ltList = new List<LaboratoryTests>();
                LaboratoryTests lt = new LaboratoryTests();
                ltList = lt.Read();
                return ltList;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }
        }
        [HttpGet]
        [Route("api/laboratorytests")]
        public LaboratoryTests Get(int IdTest)
        {
            LaboratoryTests lt = new LaboratoryTests();
            return lt.Getlt(IdTest);
        }

        [HttpGet]
        [Route("api/laboratoryTests")]
        public IEnumerable<LaboratoryTests> GetLabWithPatient(int NumPatient)
        {
            LaboratoryTests l = new LaboratoryTests();
            return l.GetLab(NumPatient);
        }

        [HttpPost]
        [Route("api/laboratoryTests")]
        public void Post(string idTest)
        {
            LaboratoryTests lt = new LaboratoryTests();
            lt.ChangeActiveLabTest(idTest);
        }
    }
}







    
//The requested resource does not support http method 'GET'.
