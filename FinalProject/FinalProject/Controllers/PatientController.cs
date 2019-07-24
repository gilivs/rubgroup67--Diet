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
    public class PatientController : ApiController
    {
        // POST api/values

        [HttpPost]
        [Route("api/patients")]
        public void Post([FromBody]Patient patient)
        {

            patient.insert();
        }


        [HttpGet]
        [Route("api/patients")]
        public IEnumerable<Patient> Get()
        {
            try
            {
                List<Patient> lo = new List<Patient>();
                Patient P = new Patient();
                lo = P.Read();
                return lo;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }
        }

        [HttpGet]
        [Route("api/patients")]
        public IEnumerable<Patient> GetBySearch(string searchTXT)
        {
            try
            {
                List<Patient> lo = new List<Patient>();
                Patient P = new Patient();
                if (searchTXT == "")
                {
                    lo = P.Read();
                }
                else {
                    lo = P.Read(searchTXT);
                }
                return lo;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }
        }






        [HttpGet]
        [Route("api/patients")]
        public Patient Get(int NumPatient)
        {
            Patient p = new Patient();
            return p.GetPatient(NumPatient);
        }

        [HttpPut]
        [Route("api/patients")]
        public void PUT([FromBody]Patient p)
        {

            Patient P = new Patient();

            P.Update(p);

        }

        [HttpPost]
        [Route("api/patients")]
        public void Post(string numPatient)
        {
            Patient P = new Patient();
            P.ChangeActive(numPatient);
        }
    }
}
