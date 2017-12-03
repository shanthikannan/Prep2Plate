namespace Prep2Plate.Migrations.UserMgmt
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaveUsersAndRecipes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRecipes",
                c => new
                    {
                        RecipeId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        RecipeName = c.String(),
                        Ingredients = c.String(),
                        RecipeSourceUrl = c.String(),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.UserName });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRecipes");
        }
    }
}
