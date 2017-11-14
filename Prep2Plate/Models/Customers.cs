using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class Customers
    {
        public byte Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Dietary Preferences" )]
        public string Diet { get; set; }
        public DietPreference DietPreference { get; set; }
        public byte DietPreferenceId { get; set; }
    }
}