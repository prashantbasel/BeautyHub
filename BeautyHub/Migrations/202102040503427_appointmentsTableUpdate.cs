namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentsTableUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Token", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Token");
        }
    }
}
