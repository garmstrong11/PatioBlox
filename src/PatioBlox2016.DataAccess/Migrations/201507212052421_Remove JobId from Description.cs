namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveJobIdfromDescription : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "JobFileId", "dbo.JobFiles");
            DropForeignKey("dbo.JobFiles", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.DescriptionUsages", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.Cells", "PageId", "dbo.Pages");
            DropForeignKey("dbo.Pages", "SectionId", "dbo.Sections");
            DropIndex("dbo.Books", new[] { "JobFileId" });
            DropIndex("dbo.JobFiles", new[] { "JobId" });
            DropIndex("dbo.Pages", new[] { "SectionId" });
            DropIndex("dbo.Cells", new[] { "PageId" });
            DropIndex("dbo.DescriptionUsages", new[] { "DescriptionId" });
            CreateTable(
                "dbo.Barcodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Upc = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Upc, unique: true);
            
            CreateTable(
                "dbo.SectionNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "JobId", c => c.Int(nullable: false));
            AddColumn("dbo.Sections", "SectionNameId", c => c.Int(nullable: false));
            AddColumn("dbo.Cells", "BarcodeId", c => c.Int(nullable: false));
            AddColumn("dbo.Cells", "SectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Descriptions", "Vendor", c => c.String(maxLength: 50));
            AlterColumn("dbo.Descriptions", "Size", c => c.String(maxLength: 50));
            AlterColumn("dbo.Descriptions", "Color", c => c.String(maxLength: 50));
            AlterColumn("dbo.Descriptions", "Name", c => c.String(maxLength: 50));
            CreateIndex("dbo.Books", "JobId");
            CreateIndex("dbo.Cells", "BarcodeId");
            CreateIndex("dbo.Cells", "SectionId");
            CreateIndex("dbo.Sections", "SectionNameId");
            AddForeignKey("dbo.Books", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cells", "BarcodeId", "dbo.Barcodes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cells", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sections", "SectionNameId", "dbo.SectionNames", "Id", cascadeDelete: true);
            DropColumn("dbo.Books", "JobFileId");
            DropColumn("dbo.Sections", "Name");
            DropColumn("dbo.Cells", "Index");
            DropColumn("dbo.Cells", "PageId");
            DropColumn("dbo.Cells", "Upc");
            DropTable("dbo.JobFiles");
            DropTable("dbo.Pages");
            DropTable("dbo.DescriptionUsages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DescriptionUsages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        RowIndex = c.Int(nullable: false),
                        Sku = c.Int(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cells", "Upc", c => c.String());
            AddColumn("dbo.Cells", "PageId", c => c.Int(nullable: false));
            AddColumn("dbo.Cells", "Index", c => c.Int(nullable: false));
            AddColumn("dbo.Sections", "Name", c => c.String());
            AddColumn("dbo.Books", "JobFileId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sections", "SectionNameId", "dbo.SectionNames");
            DropForeignKey("dbo.Cells", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Cells", "BarcodeId", "dbo.Barcodes");
            DropForeignKey("dbo.Books", "JobId", "dbo.Jobs");
            DropIndex("dbo.Sections", new[] { "SectionNameId" });
            DropIndex("dbo.Barcodes", new[] { "Upc" });
            DropIndex("dbo.Cells", new[] { "SectionId" });
            DropIndex("dbo.Cells", new[] { "BarcodeId" });
            DropIndex("dbo.Books", new[] { "JobId" });
            AlterColumn("dbo.Descriptions", "Name", c => c.String());
            AlterColumn("dbo.Descriptions", "Color", c => c.String());
            AlterColumn("dbo.Descriptions", "Size", c => c.String());
            AlterColumn("dbo.Descriptions", "Vendor", c => c.String());
            DropColumn("dbo.Cells", "SectionId");
            DropColumn("dbo.Cells", "BarcodeId");
            DropColumn("dbo.Sections", "SectionNameId");
            DropColumn("dbo.Books", "JobId");
            DropTable("dbo.SectionNames");
            DropTable("dbo.Barcodes");
            CreateIndex("dbo.DescriptionUsages", "DescriptionId");
            CreateIndex("dbo.Cells", "PageId");
            CreateIndex("dbo.Pages", "SectionId");
            CreateIndex("dbo.JobFiles", "JobId");
            CreateIndex("dbo.Books", "JobFileId");
            AddForeignKey("dbo.Pages", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cells", "PageId", "dbo.Pages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DescriptionUsages", "DescriptionId", "dbo.Descriptions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobFiles", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Books", "JobFileId", "dbo.JobFiles", "Id", cascadeDelete: true);
        }
    }
}
