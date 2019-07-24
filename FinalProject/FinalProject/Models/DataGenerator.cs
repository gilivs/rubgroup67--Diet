using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class DataGenerator
    {
        Random rnd;
        DBservices ds;
        private List<Patient> _patients;
        private List<Therapist> _theraphists;
        private List<Product> _products;
        public DataGenerator()
        {
            rnd = new Random();

            ds = new DBservices();
            _patients = GetAllPetients();
            _theraphists = GetAllTheraphists();
            _products = GetAllProducts();
        }

        internal bool Generate()
        {
            try
            {
                ds.DeleteData();
                foreach (Patient p in _patients)
                {
                    GenerateBloodData(p);
                    GenerateFoodReportData(p);
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        //bmi=50;
        //blood=25;
        //food=25 => 15 meals;
        private void GenerateFoodReportData(Patient p)
        {
            DateTime dt = DateTime.Now;
            var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            int fDay = 1;
            int lDay = lastDayOfMonth.Day;
            for (int i = fDay; i < lDay; i++)
            {
                for (int j = 0; j < 30; j++)
                {

                    Therapist randomTherapist = GetRandoTherapist();
                    DateTime randomDate = GetRandomDate(firstDayOfMonth, lastDayOfMonth);
                    Product product = GetRandomProduct();
                    bool isEat = GetRandomEatNotEat();
                    int affected = ds.GenerateFoodReportData(p, randomTherapist, randomDate, product, isEat);
                }
            }

        }

        private bool GetRandomEatNotEat()
        {
            Random gen = new Random();
            int prob = gen.Next(100);
            return prob <= 20;
        }
        private DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            var randomTest = new Random();
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;
            return newDate;
        }
        private Therapist GetRandoTherapist()
        {
            int r = rnd.Next(_theraphists.Count);
            return _theraphists[r];
        }

        private Product GetRandomProduct()
        {
            int ind = rnd.Next(_products.Count);
            return _products[ind];
        }

        private void GenerateBloodData(Patient p)
        {
            int affected = ds.GenerateBloodData(
                 p.NumPatient,
                 GetAlbium(),
                 GetCholesterol(),
                 GetProtein(),
                 GetCrp(),
                 GetLymphocytes()
                 );
        }

        private List<Product> GetAllProducts()
        {
            return ds.GetProducts("dietDBConnectionString");
        }

        private List<Therapist> GetAllTheraphists()
        {
            return ds.GetTherapist();
        }
        private List<Patient> GetAllPetients()
        {
            return ds.GetPatients();
        }

        private Double GetAlbium()
        {
            return Math.Round(rnd.NextDouble() * (7 - 2) + 2, 2);
        }

        private int GetCholesterol()
        {
            return rnd.Next(0, 160);
        }
        private int GetProtein()
        {
            return rnd.Next(12, 18);
        }
        private Double GetCrp()
        {
            return Math.Round(rnd.NextDouble() * (0.5 - 0), 0);
        }
        private int GetLymphocytes()
        {
            return rnd.Next(1200, 1500);
        }

    }
}