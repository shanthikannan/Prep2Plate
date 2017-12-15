using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Prep2Plate.Models
{
    public class Recipe
    {
        public string name { get; set; }
        public List<string> recipeIngredient { get; set; }
        public int recipeYield { get; set; }
    }
 
}