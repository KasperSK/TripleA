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
    public class ProductsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/Products
        public IQueryable<ProductDto> GetProducts()
        {
            var products = from p in db.Products
                select
                    new ProductDto()
                    {
                        Id = p.Id,
                        Name = p.Name
                    };
            return products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductDetailsDto))]
        public async Task<IHttpActionResult> GetProduct(long id)
        {
            //var product = await db.Products.Include(p => p.ProductGroups).Select(p =>
            //    new ProductDetailsDto()
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Price = p.Price,
            //        Saleable = p.Saleable,
            //        ProductGroups = new List<ProductGroup>()
            //    }).SingleOrDefaultAsync(p => p.Id == id);

            var pm = await db.Products.Include(p => p.ProductGroups).SingleOrDefaultAsync(p => p.Id == id);

            if (pm == null)
            {
                return NotFound();
            }

            var productDto = new ProductDetailsDto {Id = pm.Id, Name = pm.Name, Price = pm.Price, Saleable = pm.Saleable, ProductGroups = new List<long>()};
            pm.ProductGroups.ForEach(pg => productDto.ProductGroups.Add(pg.Id));

            return Ok(productDto);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(long id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        [ResponseType(typeof(ProductDetailsDto))]
        public async Task<IHttpActionResult> PostProduct(ProductDetailsDto productDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workwrok = productDetails.ProductGroups;
            IQueryable<ProductGroup> productGroups = null;

            foreach (var i in workwrok)
            {
                productGroups = from pt in db.ProductGroups where pt.Id == i select pt;
            }

            var product = new Product(productDetails.Name, productDetails.Price, productDetails.Saleable);
            
            productGroups?.ForEach(e => product.ProductGroups.Add(e));

            db.Products.Add(product);
            await db.SaveChangesAsync();

            productDetails.Id = product.Id;

            return CreatedAtRoute("DefaultApi", new { id = productDetails.Id }, productDetails);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(long id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(long id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}