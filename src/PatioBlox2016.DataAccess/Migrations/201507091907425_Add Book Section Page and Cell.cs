namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookSectionPageandCell : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductDescriptions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductDescriptions", "Description_Id", "dbo.Descriptions");
            DropIndex("dbo.ProductDescriptions", new[] { "Product_Id" });
            DropIndex("dbo.ProductDescriptions", new[] { "Description_Id" });
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobFileId = c.Int(nullable: false),
                        BookName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobFiles", t => t.JobFileId, cascadeDelete: true)
                .Index(t => t.JobFileId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Cells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        SourceRowIndex = c.Int(nullable: false),
                        PageId = c.Int(nullable: false),
                        Sku = c.Int(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                        PalletQty = c.Int(nullable: false),
                        Upc = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Descriptions", t => t.DescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .Index(t => t.PageId)
                .Index(t => t.DescriptionId);
            
            AddColumn("dbo.Descriptions", "Text", c => c.String());
            AddColumn("dbo.DescriptionUsages", "BookId", c => c.Int(nullable: false));
            DropColumn("dbo.Descriptions", "RawText");
            DropColumn("dbo.DescriptionUsages", "PatchId");
            DropTable("dbo.Products");
            DropTable("dbo.ProductDescriptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductDescriptions",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Description_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Description_Id });
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreferredDescriptionIndex = c.Int(nullable: false),
                        Sku = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DescriptionUsages", "PatchId", c => c.Int(nullable: false));
            AddColumn("dbo.Descriptions", "RawText", c => c.String());
            DropForeignKey("dbo.Pages", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Cells", "PageId", "dbo.Pages");
            DropForeignKey("dbo.Cells", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.Sections", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "JobFileId", "dbo.JobFiles");
            DropIndex("dbo.Cells", new[] { "DescriptionId" });
            DropIndex("dbo.Cells", new[] { "PageId" });
            DropIndex("dbo.Pages", new[] { "SectionId" });
            DropIndex("dbo.Sections", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "JobFileId" });
            DropColumn("dbo.DescriptionUsages", "BookId");
            DropColumn("dbo.Descriptions", "Text");
            DropTable("dbo.Cells");
            DropTable("dbo.Pages");
            DropTable("dbo.Sections");
            DropTable("dbo.Books");
            CreateIndex("dbo.ProductDescriptions", "Description_Id");
            CreateIndex("dbo.ProductDescriptions", "Product_Id");
            AddForeignKey("dbo.ProductDescriptions", "Description_Id", "dbo.Descriptions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductDescriptions", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
