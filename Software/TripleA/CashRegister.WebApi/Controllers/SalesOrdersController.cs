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
    public class SalesOrdersController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/SalesOrders
        public IQueryable<SalesOrderDto> GetSalesOrders()
        {
            var SalesOrder = from so in db.SalesOrders
                select
                    new SalesOrderDto()
                    {
                        Id = so.Id,
                        Date = so.Date,
                        Status = so.Status,
                        Total = so.Total
                    };

            return SalesOrder;
        }

        // GET: api/SalesOrders/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesOrderExists(long id)
        {
            return db.SalesOrders.Count(e => e.Id == id) > 0;
        }
    }
}