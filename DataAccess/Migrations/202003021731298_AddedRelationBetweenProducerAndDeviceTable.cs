namespace Warehouse_Manage_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationBetweenProducerAndDeviceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "ProducerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Devices", "ProducerID");
            AddForeignKey("dbo.Devices", "ProducerID", "dbo.Producers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "ProducerID", "dbo.Producers");
            DropIndex("dbo.Devices", new[] { "ProducerID" });
            DropColumn("dbo.Devices", "ProducerID");
        }
    }
}
