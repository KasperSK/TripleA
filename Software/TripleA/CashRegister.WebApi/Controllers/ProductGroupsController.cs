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
    /// <summary>
    /// Controller to handle data transfer of productGroups between web and database
    /// </summary>
    public class ProductGroupsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductGroups
        /// <summary>
        /// Get a list of all product groups
        /// </summary>
        /// <returns>List of products groups</returns>
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
        /// <summary>
        /// Get the details of a single product group
        /// </summary>
        /// <param name="id">Id of the productgroup to get details from</param>
        /// <returns>The detailed productgroup</returns>
        [ResponseType(typeof(ProductGroupDetailsDto))]
        public async Task<IHttpActionResult> GetProductGroup(long id)
        {
            ProductGroup productGroup =
                await db.ProductGroups.Include(pg => pg.Products).SingleOrDefaultAsync(pg => pg.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            var productGroupDetailsDto = new ProductGroupDetailsDto() {Id = productGroup.Id, Name = productGroup.Name, Products = new List<long>()};
            productGroup.Products.ForEach(p => productGroupDetailsDto.Products.Add(p.Id));

            return Ok(productGroupDetailsDto);
        }

        // PUT: api/ProductGroups/5
        /// <summary>
        /// Input a change to a productgroup specified by id
        /// </summary>
        /// <param name="id">id of the group to update</param>
        /// <param name="productGroup">the group itself</param>
        /// <returns>Status of the transaction</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductGroup(long id, ProductGroupDetailsDto productGroupDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ProductList = productGroupDetails.Products;
            List<Product> products = new List<Product>();

            foreach (var i in ProductList)
            {
                var product = from pr in db.Products where pr.Id == i select pr;
                product.ForEach(pr => products.Add(pr));
            }

            var productGroup = db.ProductGroups.Find(id);

            if (productGroup != null)
            {
                productGroup.Name = productGroupDetails.Name;
            }

            productGroup.Products.Clear();

            products?.ForEach(e=> productGroup.Products.Add(e));

            if (id != productGroup.Id)
            {
                return BadRequest();
            }

            db.Set<ProductGroup>().Attach(productGroup);
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
        /// <summary>
        /// Insert a new productgroup
        /// </summary>
        /// <param name="productGroupDetails">The Detailed dto to insert</param>
        /// <returns>The dto insert or a bad status</returns>
        [ResponseType(typeof(ProductGroupDetailsDto))]
        public async Task<IHttpActionResult> PostProductGroup(ProductGroupDetailsDto productGroupDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workwrok = productGroupDetails.Products;
            List<Product> products = new List<Product>();

            foreach (var i in workwrok)
            {
                var p = from pt in db.Products where pt.Id == i select pt;
                p.ForEach(pr => products.Add(pr));
            }

            var productGroup = new ProductGroup { Name = productGroupDetails.Name };

            products?.ForEach(e => productGroup.Products.Add(e));

            db.ProductGroups.Add(productGroup);
            await db.SaveChangesAsync();

            productGroupDetails.Id = productGroup.Id;

            return CreatedAtRoute("DefaultApi", new { id = productGroupDetails.Id }, productGroupDetails);
        }

        // DELETE: api/ProductGroups/5
        /// <summary>
        /// Delete a productgroup 
        /// </summary>
        /// <param name="id">Id of group to delete</param>
        /// <returns>The deleted product</returns>
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
        /// <summary>
        /// Method to dispose of db context
        /// </summary>
        /// <param name="disposing">True if disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Internal helper to check if productgroup is in the db
        /// </summary>
        /// <param name="id">id of group to find</param>
        /// <returns>true if it exists else false</returns>
        private bool ProductGroupExists(long id)
        {
            return db.ProductGroups.Count(e => e.Id == id) > 0;
        }
    }
}