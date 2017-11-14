namespace Prep2Plate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDietPreference : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Customers");
            CreateTable(
                "dbo.DietPreferences",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        PreferedDiet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Customers", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Customers", "DietPreferenceId");
            AddForeignKey("dbo.Customers", "DietPreferenceId", "dbo.DietPreferences", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "DietPreference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "DietPreference", c => c.String());
            DropForeignKey("dbo.Customers", "DietPreferenceId", "dbo.DietPreferences");
            DropIndex("dbo.Customers", new[] { "DietPreferenceId" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.DietPreferences");
            AddPrimaryKey("dbo.Customers", "id");
        }
    }
}
