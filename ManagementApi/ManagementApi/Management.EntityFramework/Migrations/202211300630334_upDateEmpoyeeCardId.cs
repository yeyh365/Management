namespace Management.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upDateEmpoyeeCardId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EMPLOYEE", "CardId", c => c.String());
            DropColumn("dbo.EMPLOYEE", "CredId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EMPLOYEE", "CredId", c => c.String());
            DropColumn("dbo.EMPLOYEE", "CardId");
        }
    }
}
