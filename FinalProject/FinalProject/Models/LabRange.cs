using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class LabRange
    {
        private const string TableName = "[bgroup67_test2].[dbo].[Labroratory_Range]";
        private int numParameter;
        private string name;
        private float maxP;
        private float minP;
        private string unit;
        private float goodMax;
        private float goodMin;

        public int NumParameter { get => numParameter; set => numParameter = value; }
        public string Name { get => name; set => name = value; }
        public float MaxP { get => maxP; set => maxP = value; }
        public float MinP { get => minP; set => minP = value; }
        public string Unit { get => unit; set => unit = value; }
        public float GoodMax { get => goodMax; set => goodMax = value; }
        public float GoodMin { get => goodMin; set => goodMin = value; }



        public LabRange(int _numParameter, string _name, float _maxP, float _minP, string _unit, float _goodMax, float _goodMin)
        {
            numParameter = _numParameter;
            Name = _name;
            MaxP = _maxP;
            MinP = _minP;
            Unit = _unit;
            GoodMax = _goodMax;
            goodMin = _goodMin;
        }

        public LabRange()
        {
        }

    
        public List<LabRange> GetLabRange()
        {
            DBservices db = new DBservices();

            return db.GetLabRange("dietDBConnectionString", TableName);
        }

      

    }
}