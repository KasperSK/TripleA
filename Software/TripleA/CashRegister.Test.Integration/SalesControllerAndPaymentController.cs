using System.Collections.Generic;
using CashRegister.CashDrawers;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Products;
using CashRegister.Receipts;
using CashRegister.Sales;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Integration
{
    [TestFixture]
    public class SalesOrderControllerAndPaymentController
    {
        private ISalesController _salesController;
        private IPaymentController _paymentController;

        private IReceiptController _receiptController;
        private IProductController _productController;
        private IOrderController _orderController;

        private IPaymentDao _paymentDao;
        private List<OrderLine> _orderLines;
        private List<Transaction> _transactions;
        private List<IPaymentProvider> _paymentProviders;

        private ICashDrawer _cashDrawer;
        private PaymentProvider _paymentProvider;

    
        /*
    [SetUp]
            public void SetUp()
            {
                _receiptController = Substitute.For<IReceiptController>();
                _productController = Substitute.For<IProductController>();
                _orderController = Substitute.For<IOrderController>();
                _cashDrawer = Substitute.For<ICashDrawer>();
                _transactions = new List<Transaction>();
                _paymentDao = Substitute.For<PaymentDao>();

                _paymentProviders = new List<IPaymentProvider>();

                _paymentProviders.Add(new CashPayment());

                _paymentDao.When(x => x.Insert(Arg.Any<Transaction>())).Do(x => _transactions.Add(x.Arg<Transaction>()));


                _paymentController = new PaymentController(_paymentProviders, _receiptController, _paymentDao, _cashDrawer);
                _salesController = new SalesController(_orderController, _receiptController, _productController, _paymentController);
            }
        */
            [Test]
            public void Ctor_OnInitialize_CurrentOrderTransactionsIsEmpty()
            {
                Assert.AreEqual(0,0);
            }
        }
    }
