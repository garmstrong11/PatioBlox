namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddSkutoDescriptionUsage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DescriptionUsages", "Sku", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DescriptionUsages", "Sku");
        }
    }
}
