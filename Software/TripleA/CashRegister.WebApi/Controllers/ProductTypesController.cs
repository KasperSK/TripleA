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
    /// Controller to handle data transfer of productTypes between web and database
    /// </summary>
    public class ProductTypesController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductTypes
        /// <summary>
        /// Get a list of all producttypes
        /// </summary>
        /// <returns>List of types</returns>
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
        /// <summary>
        /// get detailes afor a single types
        /// </summary>
        /// <param name="id">id of type to find details for</param>
        /// <returns>Detailed types obejct</returns>
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
        /// <summary>
        /// Input changes to a type
        /// </summary>
        /// <param name="id">id of type to change</param>
        /// <param name="productType">Obejct with changes</param>
        /// <returns>Status of how it went</returns>
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
        /// <summary>
        /// Input new object to DB
        /// </summary>
        /// <param name="productTypeDetails">Obejct containing the new type with details</param>
        /// <returns>the detailed inserted type</returns>
        [ResponseType(typeof(ProductTypeDetailsDto))]
        public async Task<IHttpActionResult> PostProductType(ProductTypeDetailsDto productTypeDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workwrok = productTypeDetails.ProductGroups;
            List<ProductGroup> productGroups = new List<ProductGroup>();

            foreach (var i in workwrok)
            {
                var pG = from pg in db.ProductGroups where pg.Id == i select pg;
                pG.ForEach(pg => productGroups.Add(pg));
            }

            var productType = new ProductType {  Color = productTypeDetails.Color, Name = productTypeDetails.Name, Price = productTypeDetails.Price};

            productGroups?.ForEach(e => productType.ProductGroups.Add(e));

            db.ProductTypes.Add(productType);
            await db.SaveChangesAsync();

            productTypeDetails.Id = productType.Id;

            return CreatedAtRoute("DefaultApi", new { id = productTypeDetails.Id }, productTypeDetails);
        }

        // DELETE: api/ProductTypes/5
        /// <summary>
        /// To delete a type
        /// </summary>
        /// <param name="id">Id of type to delete</param>
        /// <returns>The deletet type or status code</returns>
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

        /// <summary>
        /// To dispose of db context
        /// </summary>
        /// <param name="disposing">true if disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Internal helper to check if type allready exist
        /// </summary>
        /// <param name="id">Id of type</param>
        /// <returns>True if it exists</returns>
        private bool ProductTypeExists(int id)
        {
            return db.ProductTypes.Count(e => e.Id == id) > 0;
        }
    }
}