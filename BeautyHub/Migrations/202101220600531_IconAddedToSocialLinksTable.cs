namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IconAddedToSocialLinksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SocialLinks", "Icon", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SocialLinks", "Icon");
        }
    }
}
