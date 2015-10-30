namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigJobSources : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobSources", "JobPath", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.JobSources", "JobPath", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.JobSources", new[] { "JobPath" });
            DropColumn("dbo.JobSources", "JobPath");
        }
    }
}
