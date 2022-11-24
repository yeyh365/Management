namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DEPARTEMENT",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartementNumber = c.String(),
                        DepartmentName = c.String(),
                        EmployeeId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EMPLOYEE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeName = c.String(),
                        DepartmentNumber = c.String(),
                        PositionNumber = c.String(),
                        CredId = c.String(),
                        Sex = c.String(),
                        Mobile = c.Int(nullable: false),
                        Email = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdataBy = c.String(),
                        UpdataData = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EMPLOYEE_PROJECT_MAP",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ProjectNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MENU",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        PermissionId = c.Int(nullable: false),
                        Url = c.String(),
                        Sort = c.Int(nullable: false),
                        Style = c.String(),
                        ParentId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdataBy = c.String(),
                        UpdataData = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PERMISSION",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(),
                        Remark = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdataBy = c.String(),
                        UpdataData = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.POSITTION",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PosititonNumber = c.String(),
                        DepartmentNumber = c.String(),
                        PosititonName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PROJECT",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjecNumber = c.String(),
                        ProjectName = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ROLE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        PermissionId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdataBy = c.String(),
                        UpdataData = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ROLE_PERMISSION_MAP",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdataBy = c.String(),
                        UpdataData = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.USER_ROLE_MAP",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.USER_ROLE_MAP");
            DropTable("dbo.ROLE_PERMISSION_MAP");
            DropTable("dbo.ROLE");
            DropTable("dbo.PROJECT");
            DropTable("dbo.POSITTION");
            DropTable("dbo.PERMISSION");
            DropTable("dbo.MENU");
            DropTable("dbo.EMPLOYEE_PROJECT_MAP");
            DropTable("dbo.EMPLOYEE");
            DropTable("dbo.DEPARTEMENT");
        }
    }
}
