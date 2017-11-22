namespace Prep2Plate.Migrations.Recipe
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipesInitialModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Recipes", newName: "RecipeSearchResults");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RecipeSearchResults", newName: "Recipes");
        }
    }
}
