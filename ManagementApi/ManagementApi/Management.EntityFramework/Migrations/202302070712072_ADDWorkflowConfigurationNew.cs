namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDWorkflowConfigurationNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.workflow_configuration",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WorkflowCode = c.String(),
                        WorkflowName = c.String(),
                        IsStart = c.String(),
                        CreateUser = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.workflow_details",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WorkflowId = c.String(),
                        FlowSerialnuber = c.Int(nullable: false),
                        FlownodName = c.String(),
                        PostId = c.String(),
                        ProcessRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.workflow_order",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DocumentCode = c.String(),
                        DocumentTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        CreateUser = c.String(),
                        IsDelete = c.String(),
                        Mark = c.String(),
                        WorkflowId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.workflow_records",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WorkflowName = c.String(),
                        AssigneeId = c.String(),
                        HandleDate = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        WorkflowId = c.String(),
                        AuditStatus = c.Int(nullable: false),
                        IsAudit = c.Int(nullable: false),
                        FlowSerialnunber = c.String(),
                        DocumentCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.workflow_records");
            DropTable("dbo.workflow_order");
            DropTable("dbo.workflow_details");
            DropTable("dbo.workflow_configuration");
        }
    }
}
