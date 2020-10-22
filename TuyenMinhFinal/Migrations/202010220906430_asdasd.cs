namespace TuyenMinhFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdasd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trainers", "WorkingPlace");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainers", "WorkingPlace", c => c.String());
        }
    }
}
