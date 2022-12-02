namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDApplyIsDeleteBoolIsNOTNULL : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.APPLYPROCESS", "IsDelete", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.APPLYPROCESS", "IsDelete", c => c.Boolean(nullable: false));
        }
    }
}
