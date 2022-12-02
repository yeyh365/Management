namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEmployee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EMPLOYEE", "DepartmentNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.EMPLOYEE", "PositionNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EMPLOYEE", "PositionNumber", c => c.String());
            AlterColumn("dbo.EMPLOYEE", "DepartmentNumber", c => c.String());
        }
    }
}
