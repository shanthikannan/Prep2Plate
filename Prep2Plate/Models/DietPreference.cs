using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class DietPreference
    {
        public byte Id { get; set; }
        [Required]
        public string PreferedDiet { get; set; }
    }
}