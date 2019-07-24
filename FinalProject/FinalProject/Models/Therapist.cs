using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Therapist
    {
        private const string TableName = "[bgroup67_test2].[dbo].[Therapist]";

        public int NumTherapist { get; set; }
        public string IdTherapist { get; set; }
        public string NameTherapist { get; set; }


        public Therapist(int _numTherapist, string _idTherapist, string _nameTherapist)
        {
            NumTherapist = _numTherapist;
            IdTherapist = _idTherapist;
            NameTherapist = _nameTherapist;
        }

        public Therapist()
        {
        }

        public List<Therapist> ReadTherapist()
        {
            DBservices db = new DBservices();

            return db.GetTherapist();
        }
    }
}