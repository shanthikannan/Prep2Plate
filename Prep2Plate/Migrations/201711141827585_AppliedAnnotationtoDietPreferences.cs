namespace Prep2Plate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppliedAnnotationtoDietPreferences : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DietPreferences", "PreferedDiet", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DietPreferences", "PreferedDiet", c => c.String());
        }
    }
}
