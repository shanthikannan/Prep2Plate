namespace Prep2Plate.Migrations.UserMgmt
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeIdToName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecipeCalendarDatas", "RecipeName", c => c.String());
            DropColumn("dbo.RecipeCalendarDatas", "RecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecipeCalendarDatas", "RecipeId", c => c.String());
            DropColumn("dbo.RecipeCalendarDatas", "RecipeName");
        }
    }
}
