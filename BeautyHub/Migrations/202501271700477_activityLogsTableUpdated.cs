namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activityLogsTableUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityLogs", "UserRole", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityLogs", "UserRole", c => c.Int(nullable: false));
        }
    }
}
