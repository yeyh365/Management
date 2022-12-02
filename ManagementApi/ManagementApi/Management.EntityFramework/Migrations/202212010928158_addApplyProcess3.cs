namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplyProcess3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.APPLYPROCESS", "ApplyDate", c => c.DateTime());
            AlterColumn("dbo.APPLYPROCESS", "ApproverrDepartmenResult", c => c.Boolean());
            AlterColumn("dbo.APPLYPROCESS", "ApproverrDepartmentDate", c => c.DateTime());
            AlterColumn("dbo.APPLYPROCESS", "GeneralManagerDate", c => c.DateTime());
            AlterColumn("dbo.APPLYPROCESS", "ExpiredDate", c => c.DateTime());
            AlterColumn("dbo.APPLYPROCESS", "ApplyState", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.APPLYPROCESS", "ApplyState", c => c.Boolean(nullable: false));
            AlterColumn("dbo.APPLYPROCESS", "ExpiredDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.APPLYPROCESS", "GeneralManagerDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.APPLYPROCESS", "ApproverrDepartmentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.APPLYPROCESS", "ApproverrDepartmenResult", c => c.Boolean(nullable: false));
            AlterColumn("dbo.APPLYPROCESS", "ApplyDate", c => c.DateTime(nullable: false));
        }
    }
}
