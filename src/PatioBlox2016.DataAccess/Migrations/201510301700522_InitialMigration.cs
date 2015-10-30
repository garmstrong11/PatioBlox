namespace PatioBlox2016.DataAccess.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class InitialMigration : DbMigration
  {
    public override void Up()
    {
      CreateTable(
        "dbo.Descriptions",
        c => new
        {
          Id = c.Int(false, true),
          Text = c.String(false, 255),
          Vendor = c.String(maxLength: 50),
          Size = c.String(maxLength: 50),
          Color = c.String(maxLength: 50),
          Name = c.String(maxLength: 50),
          InsertDate = c.DateTime(false)
        })
        .PrimaryKey(t => t.Id)
        .Index(t => t.Text, unique: true);

      CreateTable(
        "dbo.Keywords",
        c => new
        {
          Id = c.Int(false, true),
          Word = c.String(false, 25),
          ParentId = c.Int()
        })
        .PrimaryKey(t => t.Id)
        .ForeignKey("dbo.Keywords", t => t.ParentId)
        .Index(t => t.Word, unique: true)
        .Index(t => t.ParentId);

      CreateTable(
        "dbo.UpcReplacements",
        c => new
        {
          Id = c.Int(false, true),
          InvalidUpc = c.String(false, 25),
          Replacement = c.String(false, 25)
        })
        .PrimaryKey(t => t.Id)
        .Index(t => t.InvalidUpc, unique: true);
    }

    public override void Down()
    {
      DropForeignKey("dbo.Keywords", "ParentId", "dbo.Keywords");
      DropIndex("dbo.UpcReplacements", new[] {"InvalidUpc"});
      DropIndex("dbo.Keywords", new[] {"ParentId"});
      DropIndex("dbo.Keywords", new[] {"Word"});
      DropIndex("dbo.Descriptions", new[] {"Text"});
      DropTable("dbo.UpcReplacements");
      DropTable("dbo.Keywords");
      DropTable("dbo.Descriptions");
    }
  }
}
