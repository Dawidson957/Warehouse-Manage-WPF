namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTwoOptionalColumnsToProjectTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Status", c => c.String(maxLength: 64, unicode: false));
            AddColumn("dbo.Projects", "Comment", c => c.String(maxLength: 1024, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Comment");
            DropColumn("dbo.Projects", "Status");
        }
    }
}
