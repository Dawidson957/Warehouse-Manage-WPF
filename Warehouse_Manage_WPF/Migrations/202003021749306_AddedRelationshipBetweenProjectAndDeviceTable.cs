namespace Warehouse_Manage_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationshipBetweenProjectAndDeviceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectDevices",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.DeviceId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.DeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectDevices", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.ProjectDevices", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectDevices", new[] { "DeviceId" });
            DropIndex("dbo.ProjectDevices", new[] { "ProjectId" });
            DropTable("dbo.ProjectDevices");
        }
    }
}
