namespace Webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
