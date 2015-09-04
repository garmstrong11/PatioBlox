namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeywordSingleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Keywords", "ExpansionId", c => c.Int());
            CreateIndex("dbo.Keywords", "ExpansionId");
            AddForeignKey("dbo.Keywords", "ExpansionId", "dbo.Keywords", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Keywords", "ExpansionId", "dbo.Keywords");
            DropIndex("dbo.Keywords", new[] { "ExpansionId" });
            DropColumn("dbo.Keywords", "ExpansionId");
        }
    }
}
