namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Author = c.String(),
                        Image = c.String(),
                        Price = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.CategoryId)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        BookId = c.String(),
                        CustomerId = c.String(),
                        Delivery = c.Int(),
                        Payment = c.Int(),
                        OrderPrice = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "Book_BookId" });
            DropIndex("dbo.Books", new[] { "Order_OrderId" });
            DropIndex("dbo.Books", new[] { "CategoryId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
