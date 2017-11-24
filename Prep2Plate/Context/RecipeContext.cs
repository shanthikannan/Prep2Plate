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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<RecipeContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RecipeSearchResult> RecipeSearchResults { get; set; }
    }
}