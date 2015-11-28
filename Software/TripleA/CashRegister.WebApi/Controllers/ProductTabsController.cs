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
using WebGrease.Css.Extensions;

namespace CashRegister.WebApi.Controllers
{
    public class ProductTabsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductTabs
        public IQueryable<ProductTabDto> GetProductTabs()
        {
            var productTab = from pt in db.ProductTabs
                             select
                                 new ProductTabDto
                                 {
                                     Id = pt.Id,
                                     Name = pt.Name,
                                     Priority = pt.Priority
                                 };
            return productTab;
        }

        // GET: api/ProductTabs/5
        [ResponseType(typeof(ProductTabDetailsDto))]
        public async Task<IHttpActionResult> GetProductTab(int id)
        {
            ProductTab productTab = await db.ProductTabs.Include(pt => pt.ProductTypes).SingleOrDefaultAsync(pt => pt.Id == id);
            if (productTab == null)
            {
                return NotFound();
            }

            var productTabDto = new ProductTabDetailsDto() { Active = productTab.Active, Color = productTab.Color, Id = productTab.Id, Name = productTab.Name, Priority = productTab.Priority, ProductTypes = new List<int>() };

            productTab.ProductTypes.ForEach(pt => productTabDto.ProductTypes.Add(pt.Id));

            return Ok(productTabDto);
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
        [ResponseType(typeof(ProductTabDetailsDto))]
        public async Task<IHttpActionResult> PostProductTab(ProductTabDetailsDto productTabDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workwrok = productTabDetails.ProductTypes;
            IQueryable<ProductType> productTypes = null;

            foreach (var i in workwrok)
            {
                productTypes =  from pt in db.ProductTypes where pt.Id == i select pt;
            }

            var productTab = new ProductTab {Active = productTabDetails.Active, Color = productTabDetails.Color, Name = productTabDetails.Name, Priority = productTabDetails.Priority};

            productTypes?.ForEach(e => productTab.ProductTypes.Add(e));

            db.ProductTabs.Add(productTab);
            await db.SaveChangesAsync();

            productTabDetails.Id = productTab.Id;

            return CreatedAtRoute("DefaultApi", new { id = productTabDetails.Id }, productTabDetails);
        }

        // DELETE: api/ProductTabs/5
        [ResponseType(typeof(ProductTabDto))]
        public async Task<IHttpActionResult> DeleteProductTab(int id)
        {
            ProductTab productTab = await db.ProductTabs.FindAsync(id);
            if (productTab == null)
            {
                return NotFound();
            }

            db.ProductTabs.Remove(productTab);
            await db.SaveChangesAsync();

            var productTabDto = new ProductTabDto {Id = productTab.Id, Name = productTab.Name, Priority = productTab.Priority};

            return Ok(productTabDto);
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