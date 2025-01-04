namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenAddedInAppointmentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Token");
        }
    }
}
