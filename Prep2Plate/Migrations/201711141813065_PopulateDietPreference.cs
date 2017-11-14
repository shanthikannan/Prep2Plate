namespace Prep2Plate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDietPreference : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO DietPreferences(Id, PreferedDiet) VALUES (1, Vegan) ");
            Sql("INSERT INTO DietPreferences(Id, PreferedDiet) VALUES (2, Vegetarian) ");
            Sql("INSERT INTO DietPreferences(Id, PreferedDiet) VALUES (3, Raw Food) ");
            Sql("INSERT INTO DietPreferences(Id, PreferedDiet) VALUES (4, Ketogenic) ");
            Sql("INSERT INTO DietPreferences(Id, PreferedDiet) VALUES (5, Atkins) ");
            Sql("INSERT INTO DietPreferences(Id, PreferedDiet) VALUES (6, Mediterranean) ");



        }
        
        public override void Down()
        {
        }
    }
}
