namespace Tivoli.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyModelChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "workgroupId");
          
           
        }
        
        public override void Down()
        {
            
          
            AddColumn("dbo.Users", "workgroupId", c => c.Int());
        }
    }
}
