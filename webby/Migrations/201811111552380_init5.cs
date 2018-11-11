namespace webby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentModels", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "AuthorId" });
            AddColumn("dbo.CommentModels", "Author_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.CommentModels", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.CommentModels", "Author_Id");
            AddForeignKey("dbo.CommentModels", "Author_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.PostModels", "CreatorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostModels", "CreatorId", c => c.String());
            DropForeignKey("dbo.CommentModels", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "Author_Id" });
            AlterColumn("dbo.CommentModels", "AuthorId", c => c.String(maxLength: 128));
            DropColumn("dbo.PostModels", "AuthorId");
            DropColumn("dbo.CommentModels", "Author_Id");
            CreateIndex("dbo.CommentModels", "AuthorId");
            AddForeignKey("dbo.CommentModels", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
