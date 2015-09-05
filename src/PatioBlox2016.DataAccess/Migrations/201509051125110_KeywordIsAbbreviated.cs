namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class KeywordIsAbbreviated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Keywords", "IsAbbreviated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Keywords", "IsAbbreviated");
        }
    }
}
