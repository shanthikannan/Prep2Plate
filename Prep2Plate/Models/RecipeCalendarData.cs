using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class RecipeCalendarData
    {
        [Key, Column(Order = 0)]
        public string UserName { get; set; }

        [Key, Column(Order = 1)]
        public int DayOfTheWeek { get; set; }

        [Key, Column(Order = 2)]
        public int TypeOfMeal { get; set; }

        public string RecipeId { get; set; }
    }
}