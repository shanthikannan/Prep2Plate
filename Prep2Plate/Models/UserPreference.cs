using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class UserPreference
    {
        [Key]
        public string UserName { get; set; }
        public string Preferences { get; set; }

    }
}