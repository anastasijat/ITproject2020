namespace ITproject2020.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "ClientId", "dbo.Clients");
            DropIndex("dbo.Reservations", new[] { "ClientId" });
            AddColumn("dbo.Reservations", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "User_Id");
            AddForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Reservations", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "ClientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            DropColumn("dbo.Reservations", "User_Id");
            CreateIndex("dbo.Reservations", "ClientId");
            AddForeignKey("dbo.Reservations", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
