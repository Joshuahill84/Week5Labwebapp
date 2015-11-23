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
    public class CheckoutsController : ApiController
    {
        private LibraryDBContext db = new LibraryDBContext();

        // GET: api/Checkouts
        public IQueryable<Checkout> GetCheckouts()
        {
            return db.Checkouts;
        }

        // GET: api/Checkouts/5
        [ResponseType(typeof(Checkout))]
        public IHttpActionResult GetCheckout(int id)
        {
            Checkout checkout = db.Checkouts.Find(id);
            if (checkout == null)
            {
                return NotFound();
            }

            return Ok(checkout);
        }

        // PUT: api/Checkouts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCheckout(int id, Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != checkout.Id)
            {
                return BadRequest();
            }

            db.Entry(checkout).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckoutExists(id))
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

        // POST: api/Checkouts
        [ResponseType(typeof(Checkout))]
        public IHttpActionResult PostCheckout(Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Checkouts.Add(checkout);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = checkout.Id }, checkout);
        }

        // DELETE: api/Checkouts/5
        [ResponseType(typeof(Checkout))]
        public IHttpActionResult DeleteCheckout(int id)
        {
            Checkout checkout = db.Checkouts.Find(id);
            if (checkout == null)
            {
                return NotFound();
            }

            db.Checkouts.Remove(checkout);
            db.SaveChanges();

            return Ok(checkout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckoutExists(int id)
        {
            return db.Checkouts.Count(e => e.Id == id) > 0;
        }
    }
}