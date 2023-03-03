namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDUserEmployeeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("ADMIN.USER", "EmployeeId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("ADMIN.USER", "EmployeeId");
        }
    }
}
