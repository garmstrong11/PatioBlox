namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionUsages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DescriptionUsages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatchId = c.Int(nullable: false),
                        RowIndex = c.Int(nullable: false),
                        DescriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Descriptions", t => t.DescriptionId, cascadeDelete: true)
                .Index(t => t.DescriptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DescriptionUsages", "DescriptionId", "dbo.Descriptions");
            DropIndex("dbo.DescriptionUsages", new[] { "DescriptionId" });
            DropTable("dbo.DescriptionUsages");
        }
    }
}
