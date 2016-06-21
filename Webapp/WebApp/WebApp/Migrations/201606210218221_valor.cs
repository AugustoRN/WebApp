namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncios", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anuncios", "Valor");
        }
    }
}
