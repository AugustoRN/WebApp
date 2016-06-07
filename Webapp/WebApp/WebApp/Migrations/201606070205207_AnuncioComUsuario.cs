namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnuncioComUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncios", "UsuarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.Anuncios", "UsuarioId");
            AddForeignKey("dbo.Anuncios", "UsuarioId", "dbo.Usuarios", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anuncios", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Anuncios", new[] { "UsuarioId" });
            DropColumn("dbo.Anuncios", "UsuarioId");
        }
    }
}
