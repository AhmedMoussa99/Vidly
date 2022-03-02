namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makegenrenotnull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "genre_Id", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "genre_Id" });
            AlterColumn("dbo.Movies", "genre_Id", c => c.Int());
            CreateIndex("dbo.Movies", "genre_Id");
            AddForeignKey("dbo.Movies", "genre_Id", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "genre_Id", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "genre_Id" });
            AlterColumn("dbo.Movies", "genre_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "genre_Id");
            AddForeignKey("dbo.Movies", "genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
