namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeMobile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EMPLOYEE", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EMPLOYEE", "Mobile", c => c.Int(nullable: false));
        }
    }
}
