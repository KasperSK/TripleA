using System;
using CashRegister.Dal;
using CashRegister.Models;

namespace CashRegister.Payment
{
    /// <summary>
    /// Dataasccess class for the payment controller
    /// </summary>
    public class PaymentDao : IPaymentDao
    {
        /// <summary>
        /// The dal facade used by the data access object
        /// </summary>
        private readonly IDalFacade _dalFacade;

        /// <summary>
        /// Contructor 
        /// </summary>
        /// <param name="dalFacade">Dalfacade to be used by the dao</param>
        public PaymentDao(IDalFacade dalFacade)
        {
            _dalFacade = dalFacade;
        }

        /// <summary>
        /// Funtion to delete a transaction
        /// </summary>
        /// <param name="transaction">Transaction to be deletet</param>
        // ------------------ Delete ----------------------- //
        public void Delete(Transaction transaction)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.TransactionRepository.Delete(transaction);
                uow.Save();
            }
        }

        /// <summary>
        /// Funticon to insert a transaction in the database
        /// </summary>
        /// <param name="transaction">Transaction to be inserted</param>
        // ------------------ Insert ----------------------- //
        public void Insert(Transaction transaction)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.SalesOrderRepository.Update(transaction.SalesOrder);
                uow.TransactionRepository.Insert(transaction);
                uow.Save();
            }
        }

        /// <summary>
        /// Funktion to get a transaction by its id
        /// </summary>
        /// <param name="id">Id of the transaction to get</param>
        /// <returns>The transaction coupled to the id</returns>
        public Transaction SelectByTransactionId(long id)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                return uow.TransactionRepository.GetById(id);
            }
        }

        /// <summary>
        /// To update a transaction
        /// </summary>
        /// <param name="transaction">Transaction to be updated</param>
        // ------------------ Update ----------------------- //
        public void Update(Transaction transaction)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.TransactionRepository.Update(transaction);
                uow.Save();
            }
        }
    }
}