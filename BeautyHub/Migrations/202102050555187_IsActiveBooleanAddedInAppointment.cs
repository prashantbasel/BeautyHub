namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveBooleanAddedInAppointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "IsActive");
        }
    }
}
