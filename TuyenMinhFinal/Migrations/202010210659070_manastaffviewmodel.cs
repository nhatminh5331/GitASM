namespace TuyenMinhFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manastaffviewmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Phone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Age");
        }
    }
}
