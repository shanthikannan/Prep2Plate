namespace Prep2Plate.Migrations.UserMgmt
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPref : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Preferences = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            DropTable("dbo.Customers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Diet = c.String(nullable: false),
                        PreferredShoppingDay_Day = c.String(),
                        PreferredCookingDay_Day = c.String(),
                        MealsToPrep_Meal = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.UserPreferences");
        }
    }
}
