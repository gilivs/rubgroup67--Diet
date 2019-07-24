using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{

    public class DayMealView
    {
        public DayMealView()
        {
            this.WeekMeals = new List<Meal>();
        }
        public List<Meal> WeekMeals { get; set; }
        public int Day { get; set; }
    }
}