using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class Customers
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Diet { get; set; }

        public PreferredShoppingDay PreferredShoppingDay { get; set; }

        public PreferredCookingDay PreferredCookingDay { get; set; }

        public MealsToPrep MealsToPrep { get; set; }


    }
}