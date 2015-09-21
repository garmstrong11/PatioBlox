namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertKeywordToHierarchy : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Keywords", name: "ExpansionId", newName: "ParentId");
            RenameIndex(table: "dbo.Keywords", name: "IX_ExpansionId", newName: "IX_ParentId");
            DropColumn("dbo.Keywords", "WordType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Keywords", "WordType", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Keywords", name: "IX_ParentId", newName: "IX_ExpansionId");
            RenameColumn(table: "dbo.Keywords", name: "ParentId", newName: "ExpansionId");
        }
    }
}
