using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class FoodReport
    {

        private int idReport;
        private string date;
        private string hour;
        private int numPatient;
        private int numTherapist;
        private string mealName;
        private int eat;
        private int eat_hlaf;
        private int no_eat;
        private int idProduct;
        private string comments;
        private string nameTherapist;



        public int IdReport { get => idReport; set => idReport = value; }
        public string Date { get => date; set => date = value; }
        public string Hour { get => hour; set => hour = value; }
        public int NumPatient { get => numPatient; set => numPatient = value; }
        public int NumTherapist { get => numTherapist; set => numTherapist = value; }
        public string MealName { get => mealName; set => mealName = value; }
        public int Eat { get => eat; set => eat = value; }
        public int Eat_hlaf { get => eat_hlaf; set => eat_hlaf = value; }
        public int No_eat { get => no_eat; set => no_eat = value; }
        public int IdProduct { get => idProduct; set => idProduct = value; }
        public string Comments { get => comments; set => comments = value; }
        public string NameTherapist { get => nameTherapist; set => nameTherapist = value; }

        public FoodReport(string _date, string _hour, int _numPatient, int _numTherapist, string _mealName, int _eat, int _eatHalf, int _noEat, int _idProduct, string _comments, string _nameTherapist)
        {
            Date = _date;
            Hour = _hour;
            NumPatient = _numPatient;
            NumTherapist = _numTherapist;
            MealName = _mealName;
            Eat = _eat;
            Eat_hlaf = _eatHalf;
            No_eat = _noEat;
            IdProduct = _idProduct;
            Comments = _comments;
            NameTherapist = _nameTherapist;
        }

        public FoodReport()
        {
        }



        //public List<FoodReport> ReadFoodReport(string date, string hour, int numPatient)
        //{
        //    DBservices db = new DBservices();

        //    return db.GetTherapisReports("dietDBConnectionString", date, hour, numPatient);
        //}

        public int InsertReport()
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.InsertReport(this);
            return numAffected;
        }

        public int InsertCheck()
        {
            return 1;
        }

        public List<FoodReport> GetFoodReportByPatienty(int NumPatient)
        {
            DBservices dbs = new DBservices();
            return dbs.GetFoodReportByPatienty("dietDBConnectionString", NumPatient);

        }
        public List<FoodReport> GetComments(int NumPatient)
        {
            DBservices dbs = new DBservices();
            return dbs.GetTherapistCommentsForPatient( NumPatient);

        }


        //public int ChangeActiveTherapistComments(string idComment)
        //{
        //    DBservices dbs = new DBservices();
        //    int numAffected = dbs.ChangeActiveTheraCom(idComment);
        //    return numAffected;

        //}

        //public int UpdateReport(FoodReport r)
        //{
        //    DBservices dbs = new DBservices();
        //    int numAffected = dbs.UpdateReport(r);

        //    return numAffected;
        //}
    }
}