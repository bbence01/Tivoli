namespace Tivoli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRequestsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        WorkgroupId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Workgroups", t => t.WorkgroupId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WorkgroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequests", "WorkgroupId", "dbo.Workgroups");
            DropForeignKey("dbo.UserRequests", "UserId", "dbo.Users");
            DropIndex("dbo.UserRequests", new[] { "WorkgroupId" });
            DropIndex("dbo.UserRequests", new[] { "UserId" });
            DropTable("dbo.UserRequests");
        }
    }
}
