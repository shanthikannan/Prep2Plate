using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display (Name = "Enter your Email")]
        public string EmailId { get; set; }

        [Required]
        [Display (Name = "Choose Your Diet")]
        public string Diet { get; set; }
    }
}