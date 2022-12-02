namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEmployeeId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EMPLOYEE", "EmployeeId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EMPLOYEE", "EmployeeId", c => c.Int(nullable: false));
        }
    }
}
