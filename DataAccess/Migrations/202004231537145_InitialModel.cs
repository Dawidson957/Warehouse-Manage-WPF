namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Address = c.String(maxLength: 128, unicode: false),
                        City = c.String(maxLength: 64, unicode: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        ArticleNumber = c.String(nullable: false, maxLength: 64),
                        Location = c.String(maxLength: 4, unicode: false),
                        Quantity = c.Int(),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 64, unicode: false),
                        URL = c.String(maxLength: 512),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 255, unicode: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            
        }
    }
}
