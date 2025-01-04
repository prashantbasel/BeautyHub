namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFieldAddedInAppointments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "CategoryId");
            AddForeignKey("dbo.Appointments", "CategoryId", "dbo.Categories", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Appointments", new[] { "CategoryId" });
            DropColumn("dbo.Appointments", "CategoryId");
        }
    }
}
