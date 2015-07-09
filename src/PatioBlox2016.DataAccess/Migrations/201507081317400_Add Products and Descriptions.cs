namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsandDescriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        RawText = c.String(),
                        Vendor = c.String(),
                        Size = c.String(),
                        Color = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreferredDescriptionIndex = c.Int(nullable: false),
                        Sku = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductDescriptions",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Description_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Description_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Descriptions", t => t.Description_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Description_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDescriptions", "Description_Id", "dbo.Descriptions");
            DropForeignKey("dbo.ProductDescriptions", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductDescriptions", new[] { "Description_Id" });
            DropIndex("dbo.ProductDescriptions", new[] { "Product_Id" });
            DropTable("dbo.ProductDescriptions");
            DropTable("dbo.Products");
            DropTable("dbo.Descriptions");
        }
    }
}
