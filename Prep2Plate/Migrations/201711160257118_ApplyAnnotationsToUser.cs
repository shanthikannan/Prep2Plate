namespace Prep2Plate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationsToUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "EmailId", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Diet", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Diet", c => c.String());
            AlterColumn("dbo.Users", "EmailId", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
        }
    }
}
