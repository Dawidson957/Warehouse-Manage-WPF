namespace Warehouse_Manage_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOptionalForLocationColumnToDeviceTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "Location", c => c.String(maxLength: 4, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "Location", c => c.String(nullable: false, maxLength: 4, unicode: false));
        }
    }
}
