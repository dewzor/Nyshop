namespace Webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Published", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Published");
        }
    }
}
