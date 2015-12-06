using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    /// Interface for the payment data acces class
    /// </summary>
    public interface IPaymentDao
    {
        
        /// <summary>
        /// To delete a transaction
        /// </summary>
        /// <param name="transaction">the transaction to delete</param>
        void Delete(Transaction transaction);

        /// <summary>
        /// to update a transaction
        /// </summary>
        /// <param name="transaction">the transaction to update</param>
        void Update(Transaction transaction);

        /// <summary>
        /// to insert a transaction
        /// </summary>
        /// <param name="transaction">the transaction to be insertet</param>
        void Insert(Transaction transaction);

        /// <summary>
        /// To selecet a transaction by its id
        /// </summary>
        /// <param name="id">The id to find</param>
        /// <returns>A transaction coupled to the id</returns>
        Transaction SelectByTransactionId(long id);

    }
}
