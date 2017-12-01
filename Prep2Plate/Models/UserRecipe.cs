using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class UserRecipe
    {
        public string RecipeId { get; set; }
        public string ImageUrl { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string RecipeSourceUrl { get; set; }
        public string UserName { get; set; }
    }
}