namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobFilenameto255chars : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobFiles", "FileName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobFiles", "FileName", c => c.String(maxLength: 50));
        }
    }
}
