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
using SimpleWebAPI.Models;

namespace SimpleWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Products
        public IQueryable<product_table> Getproduct_table()
        {
            return db.product_table;
        }

        // GET: api/Products/5
        [ResponseType(typeof(product_table))]
        public IHttpActionResult Getproduct_table(int id)
        {
            product_table product_table = db.product_table.Find(id);
            if (product_table == null)
            {
                return NotFound();
            }

            return Ok(product_table);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct_table(int id, product_table product_table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_table.productId)
            {
                return BadRequest();
            }

            db.Entry(product_table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!product_tableExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(product_table))]
        public IHttpActionResult Postproduct_table(product_table product_table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.product_table.Add(product_table);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product_table.productId }, product_table);
        }

        // DELETE: api/Products/5
        [NonAction]
        [ResponseType(typeof(product_table))]
        public IHttpActionResult Deleteproduct_table(int id)
        {
            product_table product_table = db.product_table.Find(id);
            if (product_table == null)
            {
                return NotFound();
            }

            db.product_table.Remove(product_table);
            db.SaveChanges();

            return Ok(product_table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool product_tableExists(int id)
        {
            return db.product_table.Count(e => e.productId == id) > 0;
        }
    }
}