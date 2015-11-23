namespace Week5Labwebapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckOutDatesFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Checkouts", "BorrowDate", "DueDate");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Checkouts", "DueDate", "BorrowDate");
        }
    }
}
