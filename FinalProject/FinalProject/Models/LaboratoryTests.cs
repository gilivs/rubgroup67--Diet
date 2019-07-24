using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class LaboratoryTests
    {
        private const string TableName = "[bgroup67_test2].[dbo].[LaboratoryTests]";
        private int idTest;
        private int numPatient;
        private string dateLab;
        private float albumin;
        private float lymphocytes;
        private float cholesterol;
        private float crp;
        private float proteinTotal;
        public int activeLab;

        public int IdTest { get => idTest; set => idTest = value; }
        public int NumPatient { get => numPatient; set => numPatient = value; }
        public string DateLab { get => dateLab; set => dateLab = value; }
        public float Albumin { get => albumin; set => albumin = value; }
        public float Lymphocytes { get => lymphocytes; set => lymphocytes = value; }
        public float Cholesterol { get => cholesterol; set => cholesterol = value; }
        public float Crp { get => crp; set => crp = value; }
        public float ProteinTotal { get => proteinTotal; set => proteinTotal = value; }
        public int ActiveLab { get => activeLab; set => activeLab = value; }

        public LaboratoryTests(int _idTest, int _numPatient, string _dateLab, float _albumin, float _lymphocytes, float _cholesterol, float _crp, float _proteinTotal, int _activeLab)
        {
            NumPatient = _numPatient;
            DateLab = _dateLab;
            Albumin = _albumin;
            Lymphocytes = _lymphocytes;
            Cholesterol = _cholesterol;
            Crp = _crp;
            ProteinTotal = _proteinTotal;
            IdTest = _idTest;
            ActiveLab = _activeLab;
        }

        public LaboratoryTests()
        {
        }

        public int insertLt()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.insertLabT(this);
            return numAffected;
        }

        public int PutLt()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.PutLt(this);
            return numAffected;
        }

        public List<LaboratoryTests> Read()
        {
            DBservices db = new DBservices();

            return db.GetLaboratoryTests();
        }

        public LaboratoryTests Getlt(int IdTest)
        {
            DBservices dbs = new DBservices();
            LaboratoryTests laboratoryTest = dbs.GetlabTest(IdTest);
            return laboratoryTest;
        }

        public List<LaboratoryTests> GetLab(int NumPatient)
        {
            DBservices dbs = new DBservices();
            return dbs.GetLab("dietDBConnectionString", TableName, NumPatient);

        }

        //public List<LaboratoryTests> Read()
        //{
        //    DBservices dbs = new DBservices();
        //    List<LaboratoryTests> laboratoryTestsList = dbs.GetLaboratoryTests();
        //    return laboratoryTestsList;
        //}

        public int ChangeActiveLabTest(string idTest)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.ChangeActiveLabTest(idTest);
            return numAffected;

        }
    }
}