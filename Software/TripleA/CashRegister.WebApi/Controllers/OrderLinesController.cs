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
    public class OrderLinesController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/OrderLines
        public IQueryable<OrderlineDto> GetOrderLines()
        {
            var OrderLines = from o in db.OrderLines
                select new OrderlineDto()
                {
                    Id = o.Id,
                    Quantity = o.Quantity,
                    ProductName = o.Product.Name,
                    UnitPrice = o.UnitPrice,
                    Discount = o.Discount.Percent,
                };
            return OrderLines;
        }

        // GET: api/OrderLines/5
        [ResponseType(typeof(OrderLine))]
        public async Task<IHttpActionResult> GetOrderLine(long id)
        {
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            return Ok(orderLine);
        }

        // PUT: api/OrderLines/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderLine(long id, OrderLine orderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderLine.Id)
            {
                return BadRequest();
            }

            db.Entry(orderLine).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineExists(id))
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

        // POST: api/OrderLines
        [ResponseType(typeof(OrderLine))]
        public async Task<IHttpActionResult> PostOrderLine(OrderLine orderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderLines.Add(orderLine);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = orderLine.Id }, orderLine);
        }

        // DELETE: api/OrderLines/5
        [ResponseType(typeof(OrderLine))]
        public async Task<IHttpActionResult> DeleteOrderLine(long id)
        {
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            db.OrderLines.Remove(orderLine);
            await db.SaveChangesAsync();

            return Ok(orderLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderLineExists(long id)
        {
            return db.OrderLines.Count(e => e.Id == id) > 0;
        }
    }
}