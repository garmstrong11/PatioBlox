namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveJobSectionBookCellSectionName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Sections", "BookId", "dbo.Books");
            DropForeignKey("dbo.Cells", "BarcodeId", "dbo.Barcodes");
            DropForeignKey("dbo.Cells", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.Cells", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Sections", "SectionNameId", "dbo.SectionNames");
            DropIndex("dbo.Books", new[] { "JobId" });
            DropIndex("dbo.Sections", new[] { "SectionNameId" });
            DropIndex("dbo.Sections", new[] { "BookId" });
            DropIndex("dbo.Cells", new[] { "DescriptionId" });
            DropIndex("dbo.Cells", new[] { "BarcodeId" });
            DropIndex("dbo.Cells", new[] { "SectionId" });
            CreateTable(
                "dbo.BarcodeCorrections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarcodeId = c.Int(nullable: false),
                        CorrectedValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Barcodes", t => t.BarcodeId, cascadeDelete: true)
                .Index(t => t.BarcodeId);
            
            DropTable("dbo.Books");
            DropTable("dbo.Jobs");
            DropTable("dbo.Sections");
            DropTable("dbo.Cells");
            DropTable("dbo.SectionNames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SectionNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceRowIndex = c.Int(nullable: false),
                        Sku = c.Int(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                        PalletQty = c.Int(nullable: false),
                        BarcodeId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionNameId = c.Int(nullable: false),
                        SourceRowIndex = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrinergyJobId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Path = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        BookName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.BarcodeCorrections", "BarcodeId", "dbo.Barcodes");
            DropIndex("dbo.BarcodeCorrections", new[] { "BarcodeId" });
            DropTable("dbo.BarcodeCorrections");
            CreateIndex("dbo.Cells", "SectionId");
            CreateIndex("dbo.Cells", "BarcodeId");
            CreateIndex("dbo.Cells", "DescriptionId");
            CreateIndex("dbo.Sections", "BookId");
            CreateIndex("dbo.Sections", "SectionNameId");
            CreateIndex("dbo.Books", "JobId");
            AddForeignKey("dbo.Sections", "SectionNameId", "dbo.SectionNames", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cells", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cells", "DescriptionId", "dbo.Descriptions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cells", "BarcodeId", "dbo.Barcodes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sections", "BookId", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Books", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
        }
    }
}
