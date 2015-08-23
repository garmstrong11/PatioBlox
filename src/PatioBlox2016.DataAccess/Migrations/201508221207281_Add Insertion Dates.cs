namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddInsertionDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Barcodes", "InsertDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Descriptions", "InsertDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sections", "SourceRowIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "SourceRowIndex");
            DropColumn("dbo.Descriptions", "InsertDate");
            DropColumn("dbo.Barcodes", "InsertDate");
        }
    }
}
