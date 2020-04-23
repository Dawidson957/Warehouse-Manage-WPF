namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationshipBetweenProjectAndDeviceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "ProjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.Devices", "ProjectID");
            AddForeignKey("dbo.Devices", "ProjectID", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Devices", new[] { "ProjectID" });
            DropColumn("dbo.Devices", "ProjectID");
        }
    }
}
