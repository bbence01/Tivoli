namespace Tivoli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hr3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestTivolis", "RequestType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestTivolis", "RequestType");
        }
    }
}
