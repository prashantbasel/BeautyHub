namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceTimes", "TimeRange", c => c.String(nullable: false));
            DropColumn("dbo.ServiceTimes", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceTimes", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.ServiceTimes", "TimeRange");
        }
    }
}
