namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotationsApplied : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Appointments", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.OfficeLocations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.OfficeLocations", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.OfficeLocations", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.OfficeLocations", "MapLink", c => c.String(nullable: false));
            AlterColumn("dbo.OfficeLocations", "Image", c => c.Binary(nullable: false));
            AlterColumn("dbo.Services", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Services", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Services", "Image", c => c.Binary(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Image", c => c.Binary(nullable: false));
            AlterColumn("dbo.Banners", "Image", c => c.Binary(nullable: false));
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Logo", c => c.Binary(nullable: false));
            AlterColumn("dbo.SocialLinks", "LinkName", c => c.String(nullable: false));
            AlterColumn("dbo.SocialLinks", "Link", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Name", c => c.String());
            AlterColumn("dbo.SocialLinks", "Link", c => c.String());
            AlterColumn("dbo.SocialLinks", "LinkName", c => c.String());
            AlterColumn("dbo.Companies", "Logo", c => c.Binary());
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Companies", "Email", c => c.String());
            AlterColumn("dbo.Companies", "Name", c => c.String());
            AlterColumn("dbo.Banners", "Image", c => c.Binary());
            AlterColumn("dbo.Categories", "Image", c => c.Binary());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Services", "Image", c => c.Binary());
            AlterColumn("dbo.Services", "Description", c => c.String());
            AlterColumn("dbo.Services", "Name", c => c.String());
            AlterColumn("dbo.OfficeLocations", "Image", c => c.Binary());
            AlterColumn("dbo.OfficeLocations", "MapLink", c => c.String());
            AlterColumn("dbo.OfficeLocations", "PhoneNumber", c => c.String());
            AlterColumn("dbo.OfficeLocations", "Email", c => c.String());
            AlterColumn("dbo.OfficeLocations", "Address", c => c.String());
            AlterColumn("dbo.Appointments", "PhoneNumber", c => c.String());
            DropColumn("dbo.Appointments", "LastName");
            DropColumn("dbo.Appointments", "FirstName");
        }
    }
}
