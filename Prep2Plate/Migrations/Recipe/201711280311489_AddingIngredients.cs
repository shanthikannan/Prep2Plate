namespace Prep2Plate.Migrations.Recipe
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIngredients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecipeSearchResults", "RecipeSourceUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecipeSearchResults", "RecipeSourceUrl");
        }
    }
}
