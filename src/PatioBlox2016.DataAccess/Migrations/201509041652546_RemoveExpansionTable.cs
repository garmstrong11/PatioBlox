namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExpansionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expansions", "KeywordId", "dbo.Keywords");
            DropIndex("dbo.Expansions", new[] { "KeywordId" });
            CreateIndex("dbo.Keywords", "Word", unique: true);
            DropTable("dbo.Expansions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Expansions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false, maxLength: 25),
                        KeywordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Keywords", new[] { "Word" });
            CreateIndex("dbo.Expansions", "KeywordId");
            AddForeignKey("dbo.Expansions", "KeywordId", "dbo.Keywords", "Id", cascadeDelete: true);
        }
    }
}
