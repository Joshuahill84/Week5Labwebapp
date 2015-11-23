using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Week5Labwebapp.Models;

namespace Week5Labwebapp.Controllers
{
    public class BooksController : ApiController
    {
        private LibraryDBContext db = new LibraryDBContext();

        [Route("api/books/checkin/{bookid}")]
        [HttpPost]
        public IHttpActionResult CheckInBook(int BookId)
        {
            //what does checking in a book mean?
            // fill out column checkindate based on datetime.now
          

            Book book = db.Books.Find(BookId);
            if (book == null)
            {
                return NotFound();
            }

            var alreadycheckoutrow = book.checkouts.FirstOrDefault(x => x.ReturnDate == null);

            if (alreadycheckoutrow == null)
            {
                return BadRequest("book is now checked in");
            }
            alreadycheckoutrow.ReturnDate = DateTime.Now;
            db.SaveChanges();
            return Ok(alreadycheckoutrow);
           
        }

        [Route("api/books/checkout")]
        public IHttpActionResult CheckOutABook(CheckOutInfo info )
        {
            Book book = db.Books.Find(info.BookId);
            if (book == null)
            {
                return NotFound();
            }

            Student student = db.Students.Find(info.StudentId);
            if (student == null)
            {
                return NotFound();
            }
           // var hasacheckout = db.Checkouts.Where(x => x.Book == book && x.ReturnDate != null);
            var isalreadycheckout = book.checkouts.Any(x => x.ReturnDate == null);

            if (isalreadycheckout)
            {
                return BadRequest("book is checked out");
            }

            Checkout cob = new Checkout();

            cob.Book = book;
            cob.Student = student;
            cob.CheckoutDate = DateTime.Now;
            cob.DueDate = cob.CheckoutDate.AddMonths(1);

            db.Checkouts.Add(cob);
            db.SaveChanges();

            var result = new { CheckedOut = cob.CheckoutDate.ToShortDateString(), DueBackOn = cob.DueDate.Value.ToShortDateString(), Book = cob.Book.Title, Student= cob.Student.Name };
            return Ok(result);

        }

        // GET: api/Books
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}