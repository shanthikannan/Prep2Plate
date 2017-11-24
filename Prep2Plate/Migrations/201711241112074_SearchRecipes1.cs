namespace Prep2Plate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchRecipes1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipeSearchResults",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        RecipeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipeSearchResults");
        }
    }
}
