using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
    }

    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("libraryCS")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                    .Property(x => x.Address).HasColumnName("StudentAddress");

            modelBuilder.Entity<Student>()
                    .Property(x => x.Id).HasColumnName("StudentId")
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Student>()
                    .Property(x => x.Name).HasColumnName("StudentName")
                    .IsRequired();

            modelBuilder.Entity<Student>()
                    .Property(x => x.DateOfBirth).HasColumnName("DOB");
        }

        public DbSet<Student> Students { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var db = new LibraryContext();
            db.Database.Log = s => Console.WriteLine(s);

            var allStudents = db.Students.ToList();

            Console.ReadLine();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
