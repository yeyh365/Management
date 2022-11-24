namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDUSER : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ADMIN.USER",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Mobile = c.Int(nullable: false),
                        Email = c.String(),
                        Picture = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdataBy = c.String(),
                        UpdataData = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("ADMIN.USER");
        }
    }
}
