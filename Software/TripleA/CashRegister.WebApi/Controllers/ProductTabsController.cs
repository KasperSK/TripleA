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
    /// Controller to handle data transfer of productTabs between web and database
    /// </summary>
    public class ProductTabsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/ProductTabs
        /// <summary>
        /// Get a list of all product tabs
        /// </summary>
        /// <returns>List of all tabs</returns>
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
        /// <summary>
        /// Get a detailed tab
        /// </summary>
        /// <param name="id">Id of tab you want details from</param>
        /// <returns>Detailed tab or an error code</returns>
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
        /// <summary>
        /// Input changes to producttab
        /// </summary>
        /// <param name="id">Id of tab to change</param>
        /// <param name="productTab">Obejct with the changes</param>
        /// <returns>Status code</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductTab(int id, ProductTabDetailsDto productTabDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ProductTypeList = productTabDetails.ProductTypes;
            List<ProductType> productTypes = new List<ProductType>();

            foreach (var i in ProductTypeList)
            {
                var pT = from pt in db.ProductTypes where pt.Id == i select pt;
                pT.ForEach(pt => productTypes.Add(pt));
            }

            var productTab = db.ProductTabs.Find(id);

            if (productTab != null)
            {
                productTab.Active = productTabDetails.Active;
                productTab.Color = productTabDetails.Color;
                productTab.Name = productTabDetails.Name;
                productTab.Priority = productTabDetails.Priority;
            }

            productTab.ProductTypes.Clear();

            productTypes?.ForEach(e => productTab.ProductTypes.Add(e));



            if (id != productTab.Id)
            {
                return BadRequest();
            }

            db.Set<ProductTab>().Attach(productTab);
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
        /// <summary>
        /// Input a new product to the db
        /// </summary>
        /// <param name="productTabDetails">An object containing the detailed product</param>
        /// <returns>The insertet obejct or an error code</returns>
        [ResponseType(typeof(ProductTabDetailsDto))]
        public async Task<IHttpActionResult> PostProductTab(ProductTabDetailsDto productTabDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workwrok = productTabDetails.ProductTypes;
            List<ProductType> productTypes = new List<ProductType>();

            foreach (var i in workwrok)
            {
                var pT = from pt in db.ProductTypes where pt.Id == i select pt;
                pT.ForEach(pt => productTypes.Add(pt));
            }

            var productTab = new ProductTab {Active = productTabDetails.Active, Color = productTabDetails.Color, Name = productTabDetails.Name, Priority = productTabDetails.Priority};

            productTypes?.ForEach(e => productTab.ProductTypes.Add(e));

            db.ProductTabs.Add(productTab);
            await db.SaveChangesAsync();

            productTabDetails.Id = productTab.Id;

            return CreatedAtRoute("DefaultApi", new { id = productTabDetails.Id }, productTabDetails);
        }

        // DELETE: api/ProductTabs/5
        /// <summary>
        /// Delete a tab
        /// </summary>
        /// <param name="id">Id of tab to delete</param>
        /// <returns>The deleted tab</returns>
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
        /// <summary>
        /// To dispose of db context
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
        /// internal helper method to check if tab exists
        /// </summary>
        /// <param name="id">id of tab to check</param>
        /// <returns>True if it exists</returns>
        private bool ProductTabExists(int id)
        {
            return db.ProductTabs.Count(e => e.Id == id) > 0;
        }
    }
}