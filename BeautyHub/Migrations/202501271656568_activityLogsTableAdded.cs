namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activityLogsTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserRole = c.Int(nullable: false),
                        Action = c.String(),
                        Resource = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Details = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActivityLogs");
        }
    }
}
