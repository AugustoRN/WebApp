namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnuncioComUsuario3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Usuarios", "Senha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "Senha", c => c.String(nullable: false));
        }
    }
}
