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
    public class TherapistCommentsController : ApiController
    {
        [HttpGet]
        [Route("api/therapistComments")]
        //public IEnumerable<TherapistComments> Get(int NumPatient)
        //{
        //    List<TherapistComments> TherapistCommentsList = new List<TherapistComments>();
        //    TherapistComments TherapistComment = new TherapistComments();
        //    TherapistCommentsList = TherapistComment.ReadTherapistComments(NumPatient);
        //    return TherapistCommentsList;
        //}

        //[HttpGet]
        //[Route("api/therapistComments")]
        //public IEnumerable<TherapistComments> GetTherapistComments()
        //{
        //    try
        //    {
        //        List<TherapistComments> TherapistCommentsList = new List<TherapistComments>();
        //        TherapistComments TherapistComment = new TherapistComments();
        //        TherapistCommentsList = TherapistComment.ReadTherapistComments();
        //        return TherapistCommentsList;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("בעיה בשליפת נתונים");
        //    }
        //}


        [HttpPost]
        [Route("api/therapistComments")]
        public void Post(string idComment)
        {
            TherapistComments tc = new TherapistComments();
            tc.ChangeActiveTherapistComments(idComment);
        }
    }
}