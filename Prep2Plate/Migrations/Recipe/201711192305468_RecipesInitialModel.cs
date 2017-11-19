namespace Prep2Plate.Migrations.Recipe
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipesInitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
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
            DropTable("dbo.Recipes");
        }
    }
}
