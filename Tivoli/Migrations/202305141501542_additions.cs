namespace Tivoli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class additions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestTivolis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        WorkgroupId = c.Int(nullable: false),
                        Status = c.String(),
                        RequestType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserTivolis", t => t.UserId)
                .ForeignKey("dbo.WorkgroupTivolis", t => t.WorkgroupId)
                .Index(t => t.UserId)
                .Index(t => t.WorkgroupId);
            
            CreateTable(
                "dbo.UserTivolis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmailConfirmed = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        LockoutEnd = c.DateTime(),
                        FailedLoginAttempts = c.Int(nullable: false),
                        PasswordLastSet = c.DateTime(nullable: false),
                        username = c.String(),
                        passwordHash = c.String(),
                        role = c.String(),
                        fullname = c.String(),
                        email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        workgroupId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.WorkgroupTivolis", t => t.workgroupId)
                .Index(t => t.workgroupId);
            
            CreateTable(
                "dbo.WorkgroupTivolis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        LeaderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestTivolis", "WorkgroupId", "dbo.WorkgroupTivolis");
            DropForeignKey("dbo.RequestTivolis", "UserId", "dbo.UserTivolis");
            DropForeignKey("dbo.UserTivolis", "workgroupId", "dbo.WorkgroupTivolis");
            DropIndex("dbo.UserTivolis", new[] { "workgroupId" });
            DropIndex("dbo.RequestTivolis", new[] { "WorkgroupId" });
            DropIndex("dbo.RequestTivolis", new[] { "UserId" });
            DropTable("dbo.WorkgroupTivolis");
            DropTable("dbo.UserTivolis");
            DropTable("dbo.RequestTivolis");
        }
    }
}
