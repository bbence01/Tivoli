namespace Tivoli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hrr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkgroupTivolis", "LeaderId", "dbo.UserTivolis");
            DropIndex("dbo.WorkgroupTivolis", new[] { "LeaderId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.WorkgroupTivolis", "LeaderId");
            AddForeignKey("dbo.WorkgroupTivolis", "LeaderId", "dbo.UserTivolis", "id", cascadeDelete: true);
        }
    }
}
