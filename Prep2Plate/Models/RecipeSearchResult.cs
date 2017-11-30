using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prep2Plate.Models
{
    public class RecipeSearchResult
    {
        [Key]
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string RecipeName { get; set; } 
        public string Ingredients { get; set; }
        //Add a string for recipeSourceUrl
        public string RecipeSourceUrl { get; set; }
    }
}