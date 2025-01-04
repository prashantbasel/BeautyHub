namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotationsAddedInAppointment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "Token", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "Token", c => c.String());
        }
    }
}
