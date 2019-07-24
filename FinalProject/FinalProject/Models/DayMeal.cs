using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class DayMeal
    {
        public int Day { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int MealId { get; set; }
        public string Description{ get; set; }
        public DateTime Date { get; set; }
        public string DateToWeek { get; set; }


        public List<DayMeal> GetWeekDate(int year)
        {
            DBservices dbs = new DBservices();
            List<DayMeal> datew = dbs.GetDateWeek(year);
            return datew;
        }

    }
}