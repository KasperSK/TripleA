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
    public class ProductTypesController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductTypes
        public IQueryable<ProductTypeDto> GetProductTypes()
        {
            var productType = from pt in db.ProductTypes
                select
                    new ProductTypeDto()
                    {
                        Id = pt.Id,
                        Name = pt.Name
                    };
            return productType;
        }

        // GET: api/ProductTypes/5
        [ResponseType(typeof(ProductTypeDetailsDto))]
        public async Task<IHttpActionResult> GetProductType(int id)
        {
            ProductType productType = await db.ProductTypes.Include(pt => pt.ProductGroups).SingleOrDefaultAsync(pt => pt.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            var productTypeDto = new ProductTypeDetailsDto {Color = productType.Color, Name = productType.Name, Id = productType.Id, Price = productType.Price, ProductGroups = new List<long>()};

            productType.ProductGroups.ForEach(pt => productTypeDto.ProductGroups.Add(pt.Id));

            return Ok(productTypeDto);
        }

        // PUT: api/ProductTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductType(int id, ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productType.Id)
            {
                return BadRequest();
            }

            db.Entry(productType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

        // POST: api/ProductTypes
        [ResponseType(typeof(ProductType))]
        public async Task<IHttpActionResult> PostProductType(ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTypes.Add(productType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productType.Id }, productType);
        }

        // DELETE: api/ProductTypes/5
        [ResponseType(typeof(ProductType))]
        public async Task<IHttpActionResult> DeleteProductType(int id)
        {
            ProductType productType = await db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            db.ProductTypes.Remove(productType);
            await db.SaveChangesAsync();

            return Ok(productType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTypeExists(int id)
        {
            return db.ProductTypes.Count(e => e.Id == id) > 0;
        }
    }
}