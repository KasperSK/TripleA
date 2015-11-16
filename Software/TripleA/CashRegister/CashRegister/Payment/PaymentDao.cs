using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.DAL;
using CashRegister.Models;
using CashRegister.Payment;

namespace CashRegister.Payment
{
    class PaymentDao : IPaymentDao
    {
        private IDalFacade _dalFacade;


        public PaymentDao(IDalFacade dalFacade)
        {
            _dalFacade = dalFacade;
        }

        // ------------------ Delete ----------------------- //
        public void Delete(Transaction transaction)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.TransactionRepository.Delete(transaction);
                uow.Save();
            }
        }

        public void Delete(PaymentType paymentType)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.PaymentTypeRepository.Delete(paymentType);
                uow.Save();
            }
        }

        // ------------------ Insert ----------------------- //
        public void Insert(Transaction transaction)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.TransactionRepository.Insert(transaction);
                uow.Save();
            }
        }

        public void Insert(PaymentType paymentType)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.PaymentTypeRepository.Insert(paymentType);
                uow.Save();
            }
        }

        // ------------------ Select ----------------------- //
        public PaymentType SelectByPaymentTypeId(long id)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.PaymentTypeRepository.GetById(id);
            }
        }

        public Transaction SelectByTransactionId(long id)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.TransactionRepository.GetById(id);
            }
        }


        // ------------------ Update ----------------------- //
        public void Update(Transaction transaction)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.TransactionRepository.Update(transaction);
                uow.Save();
            }
        }

        public void Update(PaymentType paymentType)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.PaymentTypeRepository.Update(paymentType);
                uow.Save();
            }
        }
    }
}
