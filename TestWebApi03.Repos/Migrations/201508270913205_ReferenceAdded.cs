namespace TestWebApi03.Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferenceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Residents", "Room_Id", c => c.Int());
            CreateIndex("dbo.Residents", "Room_Id");
            AddForeignKey("dbo.Residents", "Room_Id", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Residents", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Residents", new[] { "Room_Id" });
            DropColumn("dbo.Residents", "Room_Id");
        }
    }
}
