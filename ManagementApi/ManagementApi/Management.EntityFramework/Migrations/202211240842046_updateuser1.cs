namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ADMIN.USER", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("ADMIN.USER", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
