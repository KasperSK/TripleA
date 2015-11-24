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
    public class ProductGroupsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductGroups
        public IQueryable<ProductGroupDto> GetProductGroups()
        {
            var productgroup = from pg in db.ProductGroups
                select new ProductGroupDto()
                {
                    Id = pg.Id,
                    Name = pg.Name
                };
            return productgroup;
        }

        // GET: api/ProductGroups/5
        [ResponseType(typeof(ProductGroup))]
        public async Task<IHttpActionResult> GetProductGroup(long id)
        {
            ProductGroup productGroup = await db.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return Ok(productGroup);
        }

        // PUT: api/ProductGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductGroup(long id, ProductGroup productGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(productGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductGroupExists(id))
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

        // POST: api/ProductGroups
        [ResponseType(typeof(ProductGroup))]
        public async Task<IHttpActionResult> PostProductGroup(ProductGroup productGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductGroups.Add(productGroup);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productGroup.Id }, productGroup);
        }

        // DELETE: api/ProductGroups/5
        [ResponseType(typeof(ProductGroup))]
        public async Task<IHttpActionResult> DeleteProductGroup(long id)
        {
            ProductGroup productGroup = await db.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            db.ProductGroups.Remove(productGroup);
            await db.SaveChangesAsync();

            return Ok(productGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductGroupExists(long id)
        {
            return db.ProductGroups.Count(e => e.Id == id) > 0;
        }
    }
}