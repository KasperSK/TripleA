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
    /// <summary>
    /// Controller to handle transfer of OrderLines between web and database
    /// </summary>
    public class OrderLinesController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/OrderLines
        /// <summary>
        /// Get a list of all order lines
        /// </summary>
        /// <returns>List of all order lines</returns>
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
        /// <summary>
        /// Get a detailed order line
        /// </summary>
        /// <param name="id">Id of order line you want details from</param>
        /// <returns>Detailed Order line or an error code</returns>
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
        /// <summary>
        /// Input Changes to orderline
        /// </summary>
        /// <param name="id">Id of orderline to change</param>
        /// <param name="orderLine">Object with the changes</param>
        /// <returns>Status code</returns>
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
        /// <summary>
        /// Input a new orderline to the db
        /// </summary>
        /// <param name="orderLine">An object containing the detailde product</param>
        /// <returns>The insertet object or an error code</returns>
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
        /// <summary>
        /// Delete a orderline
        /// </summary>
        /// <param name="id">ID of order line to delete</param>
        /// <returns>the deleted tab</returns>
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
        /// internal helper method to check if orderline exists
        /// </summary>
        /// <param name="id">id of orderline to check</param>
        /// <returns>True if it exists</returns>
        private bool OrderLineExists(long id)
        {
            return db.OrderLines.Count(e => e.Id == id) > 0;
        }
    }
}