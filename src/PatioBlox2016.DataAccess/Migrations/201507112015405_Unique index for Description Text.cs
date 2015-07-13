namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UniqueindexforDescriptionText : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Descriptions", "Text", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Descriptions", "Text", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Descriptions", new[] { "Text" });
            AlterColumn("dbo.Descriptions", "Text", c => c.String());
        }
    }
}
