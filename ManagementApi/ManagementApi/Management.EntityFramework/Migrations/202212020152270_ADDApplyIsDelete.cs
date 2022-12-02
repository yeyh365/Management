namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDApplyIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.APPLYPROCESS", "IsDelete", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.APPLYPROCESS", "IsDelete");
        }
    }
}
