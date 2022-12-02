namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplyProcessTwo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.APPLYPROCESS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Account = c.String(),
                        ApplyTitle = c.String(),
                        ApplyBoby = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        ApproverDepartmentId = c.String(),
                        ApproverrDepartmenResult = c.Boolean(nullable: false),
                        ApproverDepartmentMeg = c.String(),
                        ApproverrDepartmentDate = c.DateTime(nullable: false),
                        GeneralManagerId = c.String(),
                        GeneralManagerResult = c.Boolean(nullable: false),
                        GeneralManagerMeg = c.String(),
                        GeneralManagerDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        ApplyState = c.Boolean(nullable: false),
                        ApplyResult = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.APPLYPROCESS");
        }
    }
}
