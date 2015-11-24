using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CashRegister.WebApi.Models;

namespace CashRegister.WebApi.Controllers
{
    public class ProductTabsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductTabs
        public IQueryable<ProductTab> GetProductTabs()
        {
            return db.ProductTabs;
        }

        // GET: api/ProductTabs/5
        [ResponseType(typeof(ProductTab))]
        public async Task<IHttpActionResult> GetProductTab(int id)
        {
            ProductTab productTab = await db.ProductTabs.FindAsync(id);
            if (productTab == null)
            {
                return NotFound();
            }

            return Ok(productTab);
        }

        // PUT: api/ProductTabs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductTab(int id, ProductTab productTab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productTab.Id)
            {
                return BadRequest();
            }

            db.Entry(productTab).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTabExists(id))
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

        // POST: api/ProductTabs
        [ResponseType(typeof(ProductTab))]
        public async Task<IHttpActionResult> PostProductTab(ProductTab productTab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTabs.Add(productTab);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productTab.Id }, productTab);
        }

        // DELETE: api/ProductTabs/5
        [ResponseType(typeof(ProductTab))]
        public async Task<IHttpActionResult> DeleteProductTab(int id)
        {
            ProductTab productTab = await db.ProductTabs.FindAsync(id);
            if (productTab == null)
            {
                return NotFound();
            }

            db.ProductTabs.Remove(productTab);
            await db.SaveChangesAsync();

            return Ok(productTab);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTabExists(int id)
        {
            return db.ProductTabs.Count(e => e.Id == id) > 0;
        }
    }
}