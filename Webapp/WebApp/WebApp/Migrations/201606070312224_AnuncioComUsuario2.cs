namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnuncioComUsuario2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "Senha", c => c.String(nullable: false));
            DropColumn("dbo.Usuarios", "Idade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "Idade", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuarios", "Senha", c => c.String());
        }
    }
}
