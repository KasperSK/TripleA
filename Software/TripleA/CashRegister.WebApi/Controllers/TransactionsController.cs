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
    /// Controller to handle data transfer of Transactions betweeen web and database
    /// </summary>
    public class TransactionsController : ApiController
    {
        private CashRegisterContext db = new CashRegisterContext();

        // GET: api/Transactions
        /// <summary>
        /// Get a list of all transaction
        /// </summary>
        /// <returns>List of all transactions</returns>
        public IQueryable<TransactionDto> GetTransactions()
        {
            var Transactions = from t in db.Transactions

                select new TransactionDto()
                {
                    Id = t.Id,
                    Description = t.Description,
                    Date = t.Date,
                    Price = t.Price,
                    PaymentType = t.PaymentType,
                    Status = t.Status
                };

            return Transactions;
        }

        // GET: api/Transactions/5
        /// <summary>
        /// Get a detailed transactions
        /// </summary>
        /// <param name="id">Id of tab you want details from</param>
        /// <returns>Detailed tab or an error code</returns>
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> GetTransaction(long id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        /// <summary>
        /// Input changes to transaction
        /// </summary>
        /// <param name="id">Id of transaction to change</param>
        /// <param name="transaction">Object with the change</param>
        /// <returns>Status code</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransaction(long id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        /// <summary>
        /// Input changes to Transactions
        /// </summary>
        /// <param name="transaction">An object containing the detailed product</param>
        /// <returns>The insertet object or an error code</returns>
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transactions.Add(transaction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        /// <summary>
        /// Delete a Transaction
        /// </summary>
        /// <param name="id">Id of transaction to delete</param>
        /// <returns>The deleted transaction</returns>
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> DeleteTransaction(long id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();

            return Ok(transaction);
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
        /// internal helper method to check if Transaction exists
        /// </summary>
        /// <param name="id">Id of the tab to check</param>
        /// <returns>True if it exists</returns>
        private bool TransactionExists(long id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}