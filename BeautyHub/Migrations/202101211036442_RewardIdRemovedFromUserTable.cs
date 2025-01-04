namespace BeautyHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RewardIdRemovedFromUserTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RewardId", "dbo.Rewards");
            DropIndex("dbo.AspNetUsers", new[] { "RewardId" });
            AddColumn("dbo.Rewards", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rewards", "UserId");
            AddForeignKey("dbo.Rewards", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "RewardId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RewardId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Rewards", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Rewards", new[] { "UserId" });
            DropColumn("dbo.Rewards", "UserId");
            CreateIndex("dbo.AspNetUsers", "RewardId");
            AddForeignKey("dbo.AspNetUsers", "RewardId", "dbo.Rewards", "Id", cascadeDelete: true);
        }
    }
}
