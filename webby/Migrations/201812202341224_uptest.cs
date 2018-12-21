namespace webby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uptest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentModels", "Name");
        }
    }
}
