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
    public class MealTypeController : ApiController
    {



        [HttpGet]
        [Route("api/mealType")]
        public IEnumerable<MealType> Get()
        {
            MealType mt = new MealType();
            return mt.GetMT();
        }
        [HttpGet]
        [Route("api/meals")]
        public IEnumerable<Meal> GetMeals()
        {
            Meal m = new Meal();
            return m.GetAll();
        }

        [HttpGet]
        [Route("api/meal/{id}/products")]
        public IEnumerable<Product> GetMealCategories(string id)
        {
            Meal m = new Meal();
            return m.GetMealProducts(id);
        }
        [HttpDelete]
        [Route("api/meal/{mealid}/products/{prodId}")]
        public bool DeleteMealCategories(string mealid, string prodId)
        {
            Meal m = new Meal();
            return m.DeleteMealCategories(mealid, prodId);
        }

        [HttpPost]
        [Route("api/meal/{mealid}/product/{prodId}")]
        public bool AddProductToMeal(string mealid, string prodId)
        {
            Meal m = new Meal();
            return m.AddProductToMeal(mealid, prodId);
        }

        [HttpPost]
        [Route("api/createMeal")]
        public bool CreateMeal(Meal meal)
        {
            Meal m = new Meal();
            m.insert(meal);

            return true;
        }

        [HttpPost]
        [Route("api/updateMeal")]
        public bool UpdateMeal(Meal meal)
        {
            Meal m = new Meal();
            m.Update(meal);

            return true;
        }

        [HttpGet]
        [Route("api/DayMeals/{day}/week/{week}")]
        public List<Meal> GetMealsOfDayInAWeek(int day, int week)
        {
            Meal m = new Meal();
            List<Meal> meals = m.GetMealsOfDayInAWeek(day, week);

            return meals;
        }


        [HttpGet]
        [Route("api/DayMeals")]
        public List<DayMeal> GetWeekDate(int Year)
        {
            DayMeal d = new DayMeal();
            List<DayMeal> datesW = d.GetWeekDate(Year);

            return datesW;
        }


        [HttpPost]
        [Route("api/DayMeals")]
        public bool CreateMealsOfDayInAWeek(DayMeal dm)
        {
            Meal m = new Meal();
            bool saved = m.CreateMealsOfDayInAWeek(dm);

            return saved;
        }

        [HttpDelete]
        [Route("api/DayMeals/{dayMealId}/{day}/{week}")]
        public bool DeleteMealsOfDayInAWeek(int dayMealId, int day, int week)
        {
            Meal m = new Meal();
            bool saved = m.DeleteMealsOfDayInAWeek(dayMealId, week, day);

            return saved;
        }


        [HttpGet]
        [Route("api/getCatItems/{catId}")]
        public List<Product> GetCatItems(int catId)
        {
            Meal m = new Meal();
            List<Product> lst = m.GetCatItems(catId);

            return lst;

        }
        [HttpGet]
        [Route("api/GetMealViewByWeek/{week}")]
        public List<DayMealView> GetMealViewByWeek(int week)
        {
            Meal m = new Meal();
            List<DayMealView> lst = m.GetMealViewByWeek(week);

            return lst;
        }

    }
}
