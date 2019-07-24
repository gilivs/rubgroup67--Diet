using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{

    public class Meal
    {
        public int Id { get; set; }
        public string MealTypeId { get; set; }
        public string Name { get; set; }
        public string[] Categories { get; set; }
        public List<Product> Products { get; set; }

        public string Description { get; set; }




        public int insert(Meal meal)
        {

            DBservices dbs = new DBservices();
            int numAffected = dbs.InsertMeal(meal);
            return numAffected;

        }

        public List<Meal> GetAll()
        {
            DBservices dbs = new DBservices();
            List<Meal> meals = dbs.GetAllMeals();
            return meals;
        }

        internal IEnumerable<Product> GetMealProducts(string id)
        {
            DBservices dbs = new DBservices();
            List<Product> cats = dbs.GetMealProducts(id);
            return cats;
        }

        internal bool DeleteMealCategories(string mealid, string prodId)
        {
            DBservices dbs = new DBservices();
            int afffected = dbs.DeleteMealProduct(mealid, prodId);
            return afffected == 1;
        }

        internal bool Update(Meal meal)
        {
            DBservices dbs = new DBservices();
            int afffected = dbs.UpdateMeal(meal);
            return afffected == 1;
        }

        internal bool AddProductToMeal(string mealid, string prodId)
        {
            DBservices dbs = new DBservices();
            int afffected = dbs.AddProductToMeal(mealid, prodId);
            return afffected == 1;
        }

        internal List<Meal> GetMealsOfDayInAWeek(int day, int week)
        {
            DBservices dbs = new DBservices();
            List<Meal> meals = dbs.GetMealsOfDayInAWeek(day, week);
            return meals;
        }

        //public List<DayMeal> GetWeekDate(int year)
        //{
        //    DBservices dbs = new DBservices();
        //    List<DayMeal> datew = dbs.GetDateWeek(year);
        //    return datew;
        //}



        internal bool CreateMealsOfDayInAWeek(DayMeal dm)
        {
            DBservices dbs = new DBservices();
            int afffected = dbs.CreateMealsOfDayInAWeek(dm);
            return afffected == 1;
        }

        internal bool DeleteMealsOfDayInAWeek(int dayMealId, int week, int day)
        {
            DBservices dbs = new DBservices();
            int afffected = dbs.DeleteMealsOfDayInAWeek(dayMealId, week, day);
            return afffected == 1;
        }

        internal List<Product> GetCatItems(int catId)
        {
            DBservices dbs = new DBservices();
            List<Product> lst = dbs.GetCatItems(catId);
            return lst;
        }

        internal List<DayMealView> GetMealViewByWeek(int week)
        {
            List<DayMealView> lst = new List<DayMealView>();
            DBservices dbs = new DBservices();


            int[] weekDay = { 1, 2, 3, 4, 5, 6, 7 };
            DayMealView dmv;
            for (int i = 0; i < weekDay.Length; i++)
            {
                dmv = new DayMealView();
                dmv.Day = weekDay[i];
                Meal m;
                for (int j = 1; j <= 6; j++)
                {
                    m = new Meal() { MealTypeId = j.ToString() };
                    m.Products = dbs.GetMealProductsByMealTypeIdAndWeekAndDay(week, weekDay[i], j);
                    dmv.WeekMeals.Add(m);
                }

                lst.Add(dmv);
            }

            return lst;
        }
    }
}