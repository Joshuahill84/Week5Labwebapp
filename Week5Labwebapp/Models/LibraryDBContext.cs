namespace Week5Labwebapp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class LibraryDBContext : DbContext
    {
        // Your context has been configured to use a 'LibraryDBContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Week5Labwebapp.Models.LibraryDBContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LibraryDBContext' 
        // connection string in the application configuration file.
        public LibraryDBContext()
            : base("name=LibraryDBContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.


        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        public System.Data.Entity.DbSet<Week5Labwebapp.Models.Book> Books { get; set; }

        public DbSet<Checkout> Checkouts { get; set; }
       
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }


        public virtual ICollection<Checkout> checkouts { get; set; }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateOfPublication { get; set; }

        public virtual ICollection<Checkout> checkouts { get; set; }

    }

    public class Checkout
    {
        public int Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual Student Student { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}



    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
