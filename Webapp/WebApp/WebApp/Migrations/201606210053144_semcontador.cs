namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class semcontador : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Anuncios", "Contador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Anuncios", "Contador", c => c.Int(nullable: false));
        }
    }
}
