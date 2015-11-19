using CashRegister.Payment;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using CashRegister.CashDrawers;
using CashRegister.Models;
using CashRegister.Receipts;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Payment
{
    [TestFixture]
    class PaymentControllerunitTest
    {
        private PaymentController _uut;

        private CashPayment cashPayment;
        private Nets nets;
        private Transaction _cashTransaction;
        private Transaction _netTransaction;

        private List<PaymentProvider> _paymentProviders;

        private IPaymentDao _fakePaymentDao;
        private IReceiptController _fakeReceiptController;
        private ICashDrawer _fakeCashDrawer;
        
        [SetUp]
        public void SetUp()
        {
            cashPayment = new CashPayment(1000);
            nets = new Nets();

            _paymentProviders = new List<PaymentProvider>() { cashPayment, nets };

            _fakePaymentDao = Substitute.For<IPaymentDao>();
            _fakeReceiptController = Substitute.For<IReceiptController>();
            _fakeCashDrawer = Substitute.For<ICashDrawer>();

            _uut = new PaymentController(_paymentProviders, _fakeReceiptController,_fakePaymentDao,_fakeCashDrawer);

            _cashTransaction = new Transaction()
            {

                Description = "noget",
                PaymentType = PaymentType.Cash,
                Price = 100,
            };

            _netTransaction = new Transaction()
            {
                Description = "noget",
                PaymentType = PaymentType.Nets,
                Price = 100,
            };
        }

        [Test]
        public void Ctor_PaymentProviderListContainsNetsAndCash_PaymentDescriptorContainsTypeNetAndCash()
        {
            var paymentDescriptor = _uut.PaymentProviderDescriptors;

            CollectionAssert.AreEquivalent(new[] { PaymentType.Cash, PaymentType.Nets,}, paymentDescriptor.Select(p => p.Type).ToList());
        }

        [Test]
        public void Ctor_PaymentProviderListContainsNetsAndCash_PaymentDescriptorContainsTypeCash()
        {
            List<PaymentProvider> paymentProviders = null;

            var uut = new PaymentController(paymentProviders, _fakeReceiptController, _fakePaymentDao, _fakeCashDrawer);
            var paymentDescriptor = uut.PaymentProviderDescriptors;

            CollectionAssert.AreEquivalent(new[] { PaymentType.Cash }, paymentDescriptor.Select(p => p.Type).ToList());
            Assert.That(paymentDescriptor.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ExecuteTransaction_TransactionCompleted_CallCashDrawerOpen()
        {
            _uut.ExecuteTransaction(_cashTransaction);

            _fakeCashDrawer.Received(1).Open();
        }

        [Test]
        public void ExecuteTransaction_TransactionCompleted_CallPaymentDaoInsert()
        {

            _uut.ExecuteTransaction(_cashTransaction);

            _fakePaymentDao.Received(1).Insert(_cashTransaction);
        }

        [Test]
        public void ExecuteTransaction_TransactionCompleted_TransaktionstatusIsCompleted()
        {
            _uut.ExecuteTransaction(_cashTransaction);

            Assert.That(_cashTransaction.Status, Is.EqualTo(TransactionStatus.Completed));
        }

        [Test]
        public void ExecuteTransaction_TransactionCompledted_returnsTrue()
        {
            var temp = _uut.ExecuteTransaction(_cashTransaction);

            Assert.That(temp, Is.EqualTo(true));
        }

        [Test]
        public void ExecuteTransaction_TransactionFailed_TransaktionstatusIsFailed()
        {
            _uut.ExecuteTransaction(_netTransaction);

            Assert.That(_netTransaction.Status, Is.EqualTo(TransactionStatus.Failed));
        }

        [Test]
        public void ExecuteTransaction_TransactionFailed_CallPaymentDaoInsert()
        {

            _uut.ExecuteTransaction(_netTransaction);

            _fakePaymentDao.Received(1).Insert(_netTransaction);
        }

        [Test]
        public void ExecuteTransaction_TransactionFailed_returnsFalse()
        {
            var temp = _uut.ExecuteTransaction(_netTransaction);

            Assert.That(temp, Is.EqualTo(false));
        }

        [Test]
        public void PrintTransaction_PrintTransaction_PrintIsCalled()
        {
            _uut.PrintTransaction(_cashTransaction);
            var receip = _fakeReceiptController.CreateReceipt(_cashTransaction);

            _fakeReceiptController.Received(1).Print(receip);
        }

        [Test]
        public void Tally_StrartCange1000_return1000()
        {
            var total = _uut.Tally();

            Assert.That(total, Is.EqualTo(1000));
        }
    }
}
