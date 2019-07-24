using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class TherapistComments
    {
        private const string TableName = "[bgroup67_test2].[dbo].[Therapist_Comments]";

        private int idComment;
        private string dateCommentTherapist;
        private int numPatient;
        private int numTherapist;
        private string foodConsumption;
        private string generalComment;
        private int numProduct_menu;
        public int activeCommentTherapist;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameTherapist { get; set; }
        public string Id { get; set; }

        public int IdComment { get => idComment; set => idComment = value; }
        public string DateCommentTherapist { get => dateCommentTherapist; set => dateCommentTherapist = value; }
        public int NumPatient { get => numPatient; set => numPatient = value; }
        public int NumTherapist { get => numTherapist; set => numTherapist = value; }
        public string FoodConsumption { get => foodConsumption; set => foodConsumption = value; }
        public string GeneralComment { get => generalComment; set => generalComment = value; }
        public int NumProduct_menu { get => numProduct_menu; set => numProduct_menu = value; }
        public int ActiveCommentTherapist { get => activeCommentTherapist; set => activeCommentTherapist = value; }

        public TherapistComments(int _idComment, string _dateCommentTherapist, int _numPatient, int _numTherapist, string _foodConsumption, string _generalComment, int _numProduct_menu, int _activeCommentTherapist)
        {
            IdComment = _idComment;
            DateCommentTherapist = _dateCommentTherapist;
            NumPatient = _numPatient;
            NumTherapist = _numTherapist;
            FoodConsumption = _foodConsumption;
            GeneralComment = _generalComment;
            NumProduct_menu = _numProduct_menu;
            ActiveCommentTherapist = _activeCommentTherapist;
        }

        public TherapistComments()
        {
        }

        public List<TherapistComments> ReadTherapistComments()
        {
            DBservices db = new DBservices();

            return db.GetTherapistComments();
        }


        public int ChangeActiveTherapistComments(string idComment)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.ChangeActiveTheraCom(idComment);
            return numAffected;

        }

        public List<FoodReport> ReadTherapistComments(int NumPatient)
        {
            DBservices db = new DBservices();
            return db.GetTherapistCommentsForPatient(NumPatient);

        }

    }
}