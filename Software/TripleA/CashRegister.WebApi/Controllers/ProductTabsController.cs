﻿using System;
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
        [ResponseType(typeof(ProductTab))]
        public async Task<IHttpActionResult> GetProductTab(int id)
        {
            ProductTab productTab = await db.ProductTabs.Include(pt => pt.ProductTypes).SingleOrDefaultAsync(pt => pt.Id == id);
            if (productTab == null)
            {
                return NotFound();
            }

            var productTabDto = new ProductTabDetailsDto() {Active = productTab.Active, Color = productTab.Color, Id = productTab.Id, Name = productTab.Name, Priority = productTab.Priority, ProductTypes = new List<int>()};
            
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