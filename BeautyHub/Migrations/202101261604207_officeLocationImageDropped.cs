namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class officeLocationImageDropped : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OfficeLocations", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfficeLocations", "Image", c => c.Binary(nullable: false));
        }
    }
}
