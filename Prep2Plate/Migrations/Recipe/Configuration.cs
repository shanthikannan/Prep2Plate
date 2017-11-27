using Prep2Plate.Models;

namespace Prep2Plate.Migrations.Recipe
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Prep2Plate.Context.RecipeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Recipe";
        }

        protected override void Seed(Prep2Plate.Context.RecipeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            RecipeSearchResult recipeSearchResult = new RecipeSearchResult();
            recipeSearchResult.Id = "Id";
            recipeSearchResult.Ingredients = "Ingredients";
            recipeSearchResult.ImageUrl = "ImageUrl";
            recipeSearchResult.RecipeName = "RecipeName";

            context.RecipeSearchResults.AddOrUpdate(rsr => rsr.Id, recipeSearchResult);
            context.SaveChanges();
        }
    }
}
