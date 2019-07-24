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

    public class LabRangeController: ApiController
    {



        [HttpGet]
        [Route("api/labRange")]
        public IEnumerable<LabRange> Get()
        {
            try
            {
                List<LabRange> lrList = new List<LabRange>();
                LabRange lr = new LabRange();
                lrList = lr.GetLabRange();
                return lrList;
            }
            catch (Exception e)
            {
                throw new Exception("בעיה בשליפת נתונים");
            }

        }
    }
  




    }



