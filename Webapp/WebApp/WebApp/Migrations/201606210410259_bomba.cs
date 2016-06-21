namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bomba : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bombas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        AnuncioId = c.Int(nullable: false),
                        valor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bombas");
        }
    }
}
