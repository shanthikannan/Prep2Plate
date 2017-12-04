namespace Prep2Plate.Migrations.UserMgmt
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCalendar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipeCalendarDatas",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        DayOfTheWeek = c.Int(nullable: false),
                        TypeOfMeal = c.Int(nullable: false),
                        RecipeId = c.String(),
                    })
                .PrimaryKey(t => new { t.UserName, t.DayOfTheWeek, t.TypeOfMeal });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipeCalendarDatas");
        }
    }
}
