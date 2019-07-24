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
    public class SensitivitiesController : ApiController
    {
        [HttpGet]
        [Route("api/sensetivities")]
        public IEnumerable<Sensitivities> Get()
        {
          Sensitivities s = new Sensitivities();
            return s.Read();
        }

    }
}
