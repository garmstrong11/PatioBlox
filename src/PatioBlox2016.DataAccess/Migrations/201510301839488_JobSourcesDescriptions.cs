namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobSourcesDescriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobSourceDescriptions",
                c => new
                    {
                        JobSource_Id = c.Int(nullable: false),
                        Description_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobSource_Id, t.Description_Id })
                .ForeignKey("dbo.JobSources", t => t.JobSource_Id, cascadeDelete: true)
                .ForeignKey("dbo.Descriptions", t => t.Description_Id, cascadeDelete: true)
                .Index(t => t.JobSource_Id)
                .Index(t => t.Description_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobSourceDescriptions", "Description_Id", "dbo.Descriptions");
            DropForeignKey("dbo.JobSourceDescriptions", "JobSource_Id", "dbo.JobSources");
            DropIndex("dbo.JobSourceDescriptions", new[] { "Description_Id" });
            DropIndex("dbo.JobSourceDescriptions", new[] { "JobSource_Id" });
            DropTable("dbo.JobSourceDescriptions");
            DropTable("dbo.JobSources");
        }
    }
}
