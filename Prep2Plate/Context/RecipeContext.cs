using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Prep2Plate.Models;

namespace Prep2Plate.Context
{
    public class RecipeContext : DbContext
    {
        public RecipeContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}