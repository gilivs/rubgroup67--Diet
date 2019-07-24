using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Patient
    {

        public int NumPatient { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public float Age { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactRelation { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Diseases { get; set; }
        public int IdTexture { get; set; }
        public string Kind0fTextureFood { get; set; }
        public List<int> Sensitivities { get; set; }
        //public int NumSensitivity { get; set; }
        public string Classification { get; set; }
        public string Image { get; set; }
        public string Comments { get; set; }
        public int Active { get; set; }
        public StatusGrades Status { get; set; }
        public double BMI { get; internal set; }

        public Patient(int _numPatient, string _id, string _firstname, string _lastname, string _dateofbirth, float _age, string _contactname, string _contactphone, string _contactrelation, float _height, float _weight, string _diseases, int _idtexture, List<int> _sensitivities, string _classification, string _image, string _comments, int _active)
        {
            Id = _id;
            FirstName = _firstname;
            LastName = _lastname;
            DateOfBirth = _dateofbirth;
            Age = _age;
            ContactName = _contactname;
            ContactPhone = _contactphone;
            ContactRelation = _contactrelation;
            Height = _height;
            Weight = _weight;
            Diseases = _diseases;
            IdTexture = _idtexture;
            Sensitivities = _sensitivities;
            Classification = _classification;
            Image = _image;
            Comments = _comments;
            Active = _active;
            NumPatient = _numPatient;
        }

        public Patient()
        {
        }

        public int insert()
        {

            DBservices dbs = new DBservices();
            int numAffected = dbs.insert(this);
            return numAffected;

        }



        public List<Patient> Read()
        {

            DBservices db = new DBservices();

            return db.GetPatients();

        }
        public List<Patient> Read(string searchTXT)
        {

            DBservices db = new DBservices();

            return db.GetPatients(searchTXT);

        }

        

        public Patient GetPatient(int NumPatient)
        {
            DBservices dbs = new DBservices();
            Patient patient = dbs.GetPatient(NumPatient);
            return patient;
        }

        public int Update(Patient p)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.Update(p);

            return numAffected;
        }

        public int ChangeActive(string numPatient)
        {
            DBservices dbs = new DBservices();
            int numAffected = dbs.ChangePat(numPatient);
            return numAffected;

        }
        public List<Patient> ReadForAddLab()
        {
            DBservices db = new DBservices();

            return db.GetPatientsForAddLab();
        }
    }
}
