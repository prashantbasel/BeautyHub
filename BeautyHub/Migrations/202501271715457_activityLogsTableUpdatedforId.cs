namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activityLogsTableUpdatedforId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityLogs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityLogs", "UserId", c => c.Int(nullable: false));
        }
    }
}
