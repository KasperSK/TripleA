using CashRegister.Payment;
using System.Collections.Generic;
using System.Linq;
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
        private IPaymentController _uut;

        private IPaymentProvider _cashPayment;
        private IPaymentProvider _nets;
        private Transaction _cashTransaction;
        private Transaction _netTransaction;

        private List<IPaymentProvider> _paymentProviders;

        private IPaymentDao _fakePaymentDao;
        private IReceiptController _fakeReceiptController;
        private ICashDrawer _fakeCashDrawer;
        
        [SetUp]
        public void SetUp()
        {
            _cashPayment = new CashPayment();
            _nets = Substitute.ForPartsOf<Nets>();
            _nets.When(x => x.TransferAmount(Arg.Any<int>(), Arg.Any<string>())).DoNotCallBase();
            _nets.TransferAmount(Arg.Any<int>(), Arg.Any<string>()).Returns(false);
            
            _paymentProviders = new List<IPaymentProvider> { _cashPayment, _nets };

            _fakePaymentDao = Substitute.For<IPaymentDao>();
            _fakeReceiptController = Substitute.For<IReceiptController>();
            _fakeCashDrawer = Substitute.For<ICashDrawer>();
            _fakeCashDrawer.CashChange.Returns(1000);

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
        public void Ctor_PaymentProviderListIsEmpty_PaymentDescriptorIsEmpty()
        {

            var uut = new PaymentController(new List<IPaymentProvider>(), _fakeReceiptController, _fakePaymentDao, _fakeCashDrawer);
            var paymentDescriptor = uut.PaymentProviderDescriptors;

            Assert.That(paymentDescriptor.Count(), Is.EqualTo(0));
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
        public void Tally_StartChange1000_return1000()
        {
            var total = _uut.Tally();

            Assert.That(total, Is.EqualTo("Cash in drawer: 1000\nTotal: 0\n\nCashPayment: 0\nNets: 0"));  // Er sat til at returnere en streng som bruges i Afstemningsvinduet
                                                                                                          // Hvorfor ser resultatet sådan ud Kalle?
        }
    }
}
