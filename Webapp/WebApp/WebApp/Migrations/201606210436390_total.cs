namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class total : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncios", "total", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anuncios", "total");
        }
    }
}
