namespace Week7LabPin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interests", "Pin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interests", "Pin");
        }
    }
}
