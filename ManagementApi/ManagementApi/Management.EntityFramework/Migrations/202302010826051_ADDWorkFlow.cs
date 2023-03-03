namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDWorkFlow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WF_FlowInstance",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NodeSN = c.String(),
                        NodeName = c.String(),
                        WFStatus = c.String(),
                        StarterId = c.Guid(),
                        Starter = c.String(),
                        OperatorId = c.Guid(),
                        Operator = c.String(),
                        ToDoerId = c.Guid(),
                        ToDoer = c.String(),
                        Operated = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        RequisitionId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WF_FlowRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WorkId = c.Guid(),
                        CurrentNodeSN = c.String(),
                        CurrentNode = c.String(),
                        OperatorId = c.Guid(),
                        Operator = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        IsPass = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeaveRequest",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Reason = c.String(),
                        Duration = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WF_Node",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NodeSN = c.String(),
                        NodeName = c.String(),
                        OperatorId = c.Guid(),
                        Operator = c.String(),
                        NextNodeSN = c.String(),
                        LastNodeSN = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WF_Node");
            DropTable("dbo.LeaveRequest");
            DropTable("dbo.WF_FlowRecord");
            DropTable("dbo.WF_FlowInstance");
        }
    }
}
