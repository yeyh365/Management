namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplyProcess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EMPLOYEE", "GeneralManagerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EMPLOYEE", "GeneralManagerId");
        }
    }
}
