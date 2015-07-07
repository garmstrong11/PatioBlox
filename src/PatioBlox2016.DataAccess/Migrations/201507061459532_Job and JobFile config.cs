namespace PatioBlox2016.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class JobandJobFileconfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobFiles", "FileName", c => c.String(maxLength: 255));
            AlterColumn("dbo.Jobs", "Path", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Path", c => c.String());
            AlterColumn("dbo.JobFiles", "FileName", c => c.String());
        }
    }
}
