namespace PatioBlox2016.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveJobIdfromDescriptionAgain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Descriptions", "JobId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Descriptions", "JobId", c => c.Int(nullable: false));
        }
    }
}
