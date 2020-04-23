namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationshipBetweenCustomerAndProjectTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "CustomerID");
            AddForeignKey("dbo.Projects", "CustomerID", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "CustomerID" });
            DropColumn("dbo.Projects", "CustomerID");
        }
    }
}
