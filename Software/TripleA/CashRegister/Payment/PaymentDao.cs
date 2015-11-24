using System;
using CashRegister.Dal;
using CashRegister.Models;

namespace CashRegister.Payment
{
    public class PaymentDao : IPaymentDao
    {
        private readonly IDalFacade _dalFacade;


        public PaymentDao(IDalFacade dalFacade)
        {
            _dalFacade = dalFacade;
        }

        // ------------------ Delete ----------------------- //
        public void Delete(Transaction transaction)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.TransactionRepository.Delete(transaction);
                uow.Save();
            }
        }

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

        public Transaction SelectByTransactionId(long id)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                return uow.TransactionRepository.GetById(id);
            }
        }


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