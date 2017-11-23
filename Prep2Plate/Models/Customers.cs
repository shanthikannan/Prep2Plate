using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class Customers
    {
        public int Id { get; set; }

        
       [Required]
        [Display (Name = "Please Select your Dietry Preferences")]
        public string Diet { get; set; }

        public PreferredShoppingDay PreferredShoppingDay { get; set; }

        public PreferredCookingDay PreferredCookingDay { get; set; }

        public MealsToPrep MealsToPrep { get; set; }


    }
}