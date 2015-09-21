namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpcReplacement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BarcodeCorrections", "BarcodeId", "dbo.Barcodes");
            DropIndex("dbo.BarcodeCorrections", new[] { "BarcodeId" });
            DropIndex("dbo.Barcodes", new[] { "Upc" });
            CreateTable(
                "dbo.UpcReplacements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvalidUpc = c.String(nullable: false, maxLength: 25),
                        Replacement = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.InvalidUpc, unique: true);
            
            DropTable("dbo.BarcodeCorrections");
            DropTable("dbo.Barcodes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Barcodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Upc = c.String(nullable: false, maxLength: 20),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BarcodeCorrections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarcodeId = c.Int(nullable: false),
                        CorrectedValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.UpcReplacements", new[] { "InvalidUpc" });
            DropTable("dbo.UpcReplacements");
            CreateIndex("dbo.Barcodes", "Upc", unique: true);
            CreateIndex("dbo.BarcodeCorrections", "BarcodeId");
            AddForeignKey("dbo.BarcodeCorrections", "BarcodeId", "dbo.Barcodes", "Id", cascadeDelete: true);
        }
    }
}
