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
    public class TherapistController : ApiController
    {
        [HttpGet]
        [Route("api/therapist")]
        public IEnumerable<Therapist> GetTherapist()
        {
            try
            {
                List<Therapist> TherapistList = new List<Therapist>();
                Therapist Therapist = new Therapist();
                TherapistList = Therapist.ReadTherapist();
                return TherapistList;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }
        }
    }
}