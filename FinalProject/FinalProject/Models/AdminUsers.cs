
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class AdminUsers
    {
        LaboratoryTests laboratoryTests;
        FoodReport foodReport;
        public int NumUser { get; set; }
        public string IdUser { get; set; }
        public string UserName { get; set; }
        public string EmailUser { get; set; }
        public string PasswordUser { get; set; }
        public string Role { get; set; }
        public int roleId { get; set; }


        public AdminUsers(int _numUser, string _idUser, string _userName, string _emailUser, string _passwordUser, string _role, int _roleId)
        {
            NumUser = _numUser;
            IdUser = _idUser;
            UserName = _userName;
            EmailUser = _emailUser;
            PasswordUser = _passwordUser;
            Role = _role;
            roleId = _roleId;
        }

        public DashBoardVM GetDashBoardData()
        {
            laboratoryTests = new LaboratoryTests();
            foodReport = new FoodReport();
            DBservices ds = new DBservices();
            //List<Patient> patients = ds.GetPatients();
            List<Patient> patients = ds.GetPatientsforSmart();
     
            foreach (Patient p in patients)
            {
                List<LaboratoryTests> labratoryTestList = laboratoryTests.GetLab(p.NumPatient);
                List<FoodReport> foodReportList = foodReport.GetFoodReportByPatienty(p.NumPatient);
                double bmi = p.Weight / (p.Height * p.Height);
                bmi = Math.Round(bmi, 4);
                p.BMI = bmi;

                CalcUserStatus(labratoryTestList.FirstOrDefault(), foodReportList, p.BMI, p);
            }

            DashBoardVM dvm = new DashBoardVM();
            dvm.Patients = patients;
            return dvm;
        }

        public Patient GetDashBoardDataByPerson(int numPatient , float height, float weight)
        {
            Patient p = new Patient();
            laboratoryTests = new LaboratoryTests();
            foodReport = new FoodReport();
            DBservices ds = new DBservices();
            //List<Patient> patients = ds.GetPatients();
           

            
                List<LaboratoryTests> labratoryTestList = laboratoryTests.GetLab(numPatient);
                List<FoodReport> foodReportList = foodReport.GetFoodReportByPatienty(numPatient);
                double bmi = weight / (height * height);
                bmi = Math.Round(bmi, 4);
                p.BMI = bmi;

                CalcUserStatus(labratoryTestList.FirstOrDefault(), foodReportList, p.BMI, p);
            

            //DashBoardVM dvm = new DashBoardVM();
            //dvm.Patients = patients;
            return p;
        }

        private void CalcUserStatus(LaboratoryTests laboratoryTests,
            List<FoodReport> foodReportList, double _bmi, Patient p)
        {
            int bloodeGrade = CalcAvgBloodeTest(laboratoryTests);//max of 25 points
            double foodGrade = CalcAvgFoodReport(foodReportList);
            int bmiGrade = CalcAvgBMI(_bmi);
            p.Status = new StatusGrades(bmiGrade, bloodeGrade, foodGrade);
        }

        private int CalcAvgBMI(double bmi)
        {
            bmi *= 10000;
            if (bmi >= 22 && bmi <= 27) return 50;
            else
            {
                return 30;
            }
        }

        private double CalcAvgFoodReport(List<FoodReport> foodReportList)
        {
            double numOfEats = 0;
            foreach (var item in foodReportList)
            {
                if (item.Eat == 1) { numOfEats++; }
            }
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            double perDay = numOfEats / days;
            if (perDay >= 15)
            {
                return 25;
            }
            double weightedPercent = (perDay / 15) * 100;
            double weightedGrade = (weightedPercent * 25) / 100;
            return weightedGrade;
        }

        private int CalcAvgBloodeTest(LaboratoryTests laboratoryTests)
        {
            if (laboratoryTests == null) return 0;
            LabRange lr = new LabRange();
            List<LabRange> labRanges = lr.GetLabRange();

            int bloodeGrade = 0;
            LabRange Albumin = labRanges.FirstOrDefault(x => x.Name == "Albumin");
            LabRange Cholesterol = labRanges.FirstOrDefault(x => x.Name == "Cholesterol");
            LabRange ProteinTotal = labRanges.FirstOrDefault(x => x.Name == "ProteinTotal");
            LabRange CRP = labRanges.FirstOrDefault(x => x.Name == "CRP");
            LabRange Lymphocytes = labRanges.FirstOrDefault(x => x.Name == "Lymphocytes");

            if (laboratoryTests.Albumin >= Albumin.MinP && laboratoryTests.Albumin <= Albumin.MaxP)
            {
                bloodeGrade += 5;
            }
            if (laboratoryTests.Cholesterol >= Cholesterol.MinP && laboratoryTests.Cholesterol <= Cholesterol.MaxP)
            {
                bloodeGrade += 5;
            }
            if (laboratoryTests.ProteinTotal >= ProteinTotal.MinP && laboratoryTests.ProteinTotal <= ProteinTotal.MaxP)
            {
                bloodeGrade += 5;
            }
            if (laboratoryTests.Crp >= CRP.MinP && laboratoryTests.Crp <= CRP.MaxP)
            {
                bloodeGrade += 5;
            }
            if (laboratoryTests.Lymphocytes >= Lymphocytes.MinP && laboratoryTests.Lymphocytes <= Lymphocytes.MaxP)
            {
                bloodeGrade += 5;
            }
            return bloodeGrade;
        }

        public AdminUsers()
        {
        }

        //public List<AdminUsers> Get()
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.Get("dietDBConnectionString", "PersonTbl_new");
        //}

        public AdminUsers Exist(string EmailUser, string PasswordUser)
        {
            DBservices dbs = new DBservices();
            return dbs.Exist("dietDBConnectionString", "AdminUsers", EmailUser, PasswordUser);
        }

        public AdminUsers AppUser(string userName, string passwordUser)
        {
            DBservices dbs = new DBservices();
            return dbs.ExistUserApp("dietDBConnectionString", "AdminUsers", userName, passwordUser);
        }


    }
}