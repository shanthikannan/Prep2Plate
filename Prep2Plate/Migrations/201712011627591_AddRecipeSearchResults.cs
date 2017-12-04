namespace Prep2Plate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipeSearchResults : DbMigration
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
