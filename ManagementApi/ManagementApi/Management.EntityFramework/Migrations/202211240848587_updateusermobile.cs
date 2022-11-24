namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusermobile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ADMIN.USER", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("ADMIN.USER", "Mobile", c => c.Int(nullable: false));
        }
    }
}
