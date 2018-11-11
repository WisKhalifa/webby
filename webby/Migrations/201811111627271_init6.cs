namespace webby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PostModels", new[] { "Author_Id" });
            DropColumn("dbo.PostModels", "AuthorId");
            RenameColumn(table: "dbo.PostModels", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.PostModels", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PostModels", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PostModels", new[] { "AuthorId" });
            AlterColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PostModels", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.PostModels", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.PostModels", "Author_Id");
        }
    }
}
