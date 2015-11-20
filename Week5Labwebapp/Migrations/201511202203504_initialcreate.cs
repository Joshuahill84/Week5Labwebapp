namespace Week5Labwebapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Checkouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckoutDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        BorrowDate = c.DateTime(),
                        Book_Id = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        DateOfPublication = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checkouts", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Checkouts", "Book_Id", "dbo.Books");
            DropIndex("dbo.Checkouts", new[] { "Student_Id" });
            DropIndex("dbo.Checkouts", new[] { "Book_Id" });
            DropTable("dbo.Books");
            DropTable("dbo.Checkouts");
            DropTable("dbo.Students");
        }
    }
}
