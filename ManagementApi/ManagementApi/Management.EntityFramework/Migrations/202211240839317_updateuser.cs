namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("ADMIN.USER", "Last_LoginTime", c => c.DateTime(nullable: false));
            DropColumn("ADMIN.USER", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("ADMIN.USER", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("ADMIN.USER", "Last_LoginTime");
        }
    }
}
