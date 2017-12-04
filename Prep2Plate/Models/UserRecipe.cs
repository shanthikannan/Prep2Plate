using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class UserRecipe
    {
        [Key, Column(Order = 0)]
        public string RecipeId { get; set; }

        public string ImageUrl { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string RecipeSourceUrl { get; set; }

        [Key, Column(Order = 1)]
        public string UserName { get; set; }
    }
}