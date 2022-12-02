namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplyProcess4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.APPLYPROCESS", "GeneralManagerResult", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.APPLYPROCESS", "GeneralManagerResult", c => c.Boolean(nullable: false));
        }
    }
}
