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
    public class SalesOrderControllerOrderControllerPaymentController
    {
        private List<OrderLine> _orderLines;

        private IReceiptController _receiptController;
        private IProductController _productController;
        private ICashDrawer _cashDrawer;
        private IPaymentDao _paymentDao;
        private IOrderDao _orderDao;

        private IPaymentController _paymentController;
        private IOrderController _orderController;
        private ISalesController _salesController;

        [SetUp]
        public void SetUp()
        {
            _orderLines = new List<OrderLine>();

            _receiptController = Substitute.For<IReceiptController>();
            _productController = Substitute.For<IProductController>();
            _cashDrawer = Substitute.For<ICashDrawer>();
            _paymentDao = Substitute.For<IPaymentDao>();
            _orderDao = Substitute.For<IOrderDao>();
            _orderDao.When(x => x.AddOrderLine(Arg.Any<OrderLine>())).Do(x => _orderLines.Add(x.Arg<OrderLine>()));

            var paymentProviders = new List<IPaymentProvider>() { new CashPayment() };
            _paymentController = new PaymentController(paymentProviders, _receiptController, _paymentDao, _cashDrawer);
            _orderController = new OrderController(_orderDao);
            _salesController = new SalesController(_orderController, _receiptController, _productController, _paymentController);
        }

        [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_OrderDaoUpdateIsCalledOnce()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(18, "", PaymentType.Cash);

            _orderDao.Received(1).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_OrderDaoUpdateIsCalledTwice()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(10, "", PaymentType.Cash);
            _salesController.StartPayment(8, "", PaymentType.Cash);

            _orderDao.Received(2).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_OrderDaoUpdateIsCalledTwice2()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(10, "", PaymentType.Cash);
            _salesController.StartPayment(8, "", PaymentType.Cash);

           Assert.That(_salesController.MissingPaymentOnOrder(),Is.EqualTo(0)); 
        }

       [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_PaymentDaoInsertIsCalled()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(18, "", PaymentType.Cash);

            _paymentDao.Received(1).Insert(Arg.Any<Transaction>());
        }

        [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_PaymentDaoInsertIsCalledTwice()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(10, "", PaymentType.Cash);
            _salesController.StartPayment(8, "", PaymentType.Cash);

            _paymentDao.Received(2).Insert(Arg.Any<Transaction>());
        }

        [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_PaymentDaoUpdateIsNeverCalled()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(10, "", PaymentType.Cash);
            _salesController.StartPayment(8, "", PaymentType.Cash);

            _paymentDao.DidNotReceive().Update(Arg.Any<Transaction>());
        }

        [Test]
        public void StartPayment_ProductIsAddedToOrderExactAmountIsPayed_CashDrawerOpenIsCalledOnce()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);
            _salesController.StartPayment(18, "", PaymentType.Cash);

            _cashDrawer.Received(1).Open();
        }
    }
}