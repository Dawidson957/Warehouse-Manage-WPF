namespace Warehouse_Manage_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRelationshipBetweenDevicesAndProjectsToOneToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectDevices", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectDevices", "DeviceId", "dbo.Devices");
            DropIndex("dbo.ProjectDevices", new[] { "ProjectId" });
            DropIndex("dbo.ProjectDevices", new[] { "DeviceId" });
            AddColumn("dbo.Devices", "ProjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.Devices", "ProjectID");
            AddForeignKey("dbo.Devices", "ProjectID", "dbo.Projects", "Id", cascadeDelete: true);
            DropTable("dbo.ProjectDevices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectDevices",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.DeviceId });
            
            DropForeignKey("dbo.Devices", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Devices", new[] { "ProjectID" });
            DropColumn("dbo.Devices", "ProjectID");
            CreateIndex("dbo.ProjectDevices", "DeviceId");
            CreateIndex("dbo.ProjectDevices", "ProjectId");
            AddForeignKey("dbo.ProjectDevices", "DeviceId", "dbo.Devices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectDevices", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
