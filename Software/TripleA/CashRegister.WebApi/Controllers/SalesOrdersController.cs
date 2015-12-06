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
    public class SalesOrdersController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();
        
        // GET: api/salesorder
        /// <summary>
        /// Get a list of all sales orders
        /// </summary>
        /// <returns>List of all sales</returns>
        public IEnumerable<SalesOrderDto> GetSalesOrders()
        {
            var SalesOrder = db.SalesOrders.Include(so => so.Lines);

            var salesOrderDtoList = new List<SalesOrderDto>(); 

            foreach (var salesOrder in SalesOrder)
            {
                var salesOrderDto = new SalesOrderDto();
                salesOrderDto.Status = salesOrder.Status;
                salesOrderDto.Date = salesOrder.Date;
                salesOrderDto.Id = salesOrder.Id;
                salesOrderDto.Total = salesOrder.Total;

                salesOrderDtoList.Add(salesOrderDto);
            }

            return salesOrderDtoList;
        }


        // GET: api/SalesOrders/5
        /// <summary>
        /// Get detailed Sales
        /// </summary>
        /// <param name="id">Id of the sale you want details from</param>
        /// <returns>Detailed sales or an error code</returns>
        [ResponseType(typeof(SalesOrder))]
        public async Task<IHttpActionResult> GetSalesOrder(long id)
        {
            SalesOrder salesOrder = await db.SalesOrders.FindAsync(id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            var salesOrderDto = new SalesOrderDetailsDto {Date = salesOrder.Date, Id = salesOrder.Id, Status = salesOrder.Status, Total = salesOrder.Total, Transactions = new List<long>(), Lines = new List<long>()};

            salesOrder.Transactions.ForEach(so => salesOrderDto.Transactions.Add(so.Id));
            salesOrder.Lines.ForEach(so => salesOrderDto.Lines.Add(so.Id));

            return Ok(salesOrderDto);
        }

        // PUT: api/SalesOrders/5
        /// <summary>
        /// Input changes to salesorder
        /// </summary>
        /// <param name="id">Id of sales to change</param>
        /// <param name="salesOrder">Object with the changes</param>
        /// <returns>Status code</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesOrder(long id, SalesOrder salesOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesOrder.Id)
            {
                return BadRequest();
            }

            db.Entry(salesOrder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderExists(id))
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

        // POST: api/SalesOrders
        /// <summary>
        /// Input a new product to db
        /// </summary>
        /// <param name="salesOrder">An object containing the detailed product</param>
        /// <returns>The insertet objecct or an error code</returns>
        [ResponseType(typeof(SalesOrder))]
        public async Task<IHttpActionResult> PostSalesOrder(SalesOrder salesOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesOrders.Add(salesOrder);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = salesOrder.Id }, salesOrder);
        }

        // DELETE: api/SalesOrders/5
        /// <summary>
        /// Delete a salesorder
        /// </summary>
        /// <param name="id">Id of salesoder to delete</param>
        /// <returns>the deleted salesorder</returns>
        [ResponseType(typeof(SalesOrder))]
        public async Task<IHttpActionResult> DeleteSalesOrder(long id)
        {
            SalesOrder salesOrder = await db.SalesOrders.FindAsync(id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            db.SalesOrders.Remove(salesOrder);
            await db.SaveChangesAsync();

            return Ok(salesOrder);
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
        /// internal helper method to check if salesorder exists
        /// </summary>
        /// <param name="id">id of salesorder to check</param>
        /// <returns>True if it exists</returns>
        private bool SalesOrderExists(long id)
        {
            return db.SalesOrders.Count(e => e.Id == id) > 0;
        }
    }
}