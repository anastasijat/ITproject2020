namespace ITproject2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Performances", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Seats", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seats", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Performances", "Price");
        }
    }
}
