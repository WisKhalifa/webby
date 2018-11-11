namespace webby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentModels", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostModels", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "Author_Id" });
            DropIndex("dbo.PostModels", new[] { "AuthorId" });
            AlterColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            DropColumn("dbo.CommentModels", "Author_Id");
            DropColumn("dbo.PostModels", "IsPublic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostModels", "IsPublic", c => c.Boolean(nullable: false));
            AddColumn("dbo.CommentModels", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.PostModels", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PostModels", "AuthorId");
            CreateIndex("dbo.CommentModels", "Author_Id");
            AddForeignKey("dbo.PostModels", "AuthorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CommentModels", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
