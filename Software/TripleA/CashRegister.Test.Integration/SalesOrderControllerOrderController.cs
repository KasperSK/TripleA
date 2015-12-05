using System.Collections.Generic;
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
    public class SalesOrderControllerOrderController
    {
        private List<OrderLine> _orderLines;

        private IOrderController _orderController;
        private ISalesController _salesController;

        private IReceiptController _receiptController;
        private IProductController _productController;
        private IPaymentController _paymentController;
        private IOrderDao _orderDao;

        [SetUp]
        public void SetUp()
        {
            _orderLines = new List<OrderLine>();

            _receiptController = Substitute.For<IReceiptController>();
            _productController = Substitute.For<IProductController>();
            _paymentController = Substitute.For<IPaymentController>();
            _orderDao = Substitute.For<IOrderDao>();
            _orderDao.When(x => x.AddOrderLine(Arg.Any<OrderLine>())).Do(x => _orderLines.Add(x.Arg<OrderLine>()));

            _orderController = new OrderController(_orderDao);
            _salesController = new SalesController(_orderController, _receiptController, _productController, _paymentController);
        }

        [Test]
        public void Ctor_OnInitialize_CurrentOrderIsInstanceOfSalesOrder()
        {
            Assert.That(_salesController.CurrentOrder, Is.InstanceOf<SalesOrder>());
        }

        [Test]
        public void Ctor_OnInitialize_CurrentOrderLinesIsEmpty()
        {
           Assert.That(_salesController.CurrentOrderLines, Is.Empty);
        }

        [Test]
        public void Ctor_OnInitialize_CurrentOrderTotalIsEmpty()
        {
            Assert.That(_salesController.CurrentOrderTotal, Is.EqualTo(0));
        }

        [Test]
        public void AddProduct_ProductIsAddedToOrder_OrderLineContainsProduct()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);

            Assert.That(_orderLines[0].Product, Is.EqualTo(product));
        }

        [Test]
        public void AddProduct_ProductIsAddedToOrder_OrderLineContainsQuantity()
        {
            var product = new Product("Beer", 18, true);

            _salesController.AddProductToOrder(product, 1, null);

            Assert.That(_orderLines[0].Quantity, Is.EqualTo(1));
        }

        [Test]
        public void AddProduct_TwoProductsAreAddedToOrder_OrderDaoAddOrderLineIsCalledTwice()
        {
            var productOne = new Product("Beer", 18, true);
            var productTwo = new Product("Vodka", 40, true);

            _salesController.AddProductToOrder(productOne, 1, null);
            _salesController.AddProductToOrder(productTwo, 1, null);

            _orderDao.Received(2).AddOrderLine(Arg.Any<OrderLine>());
        }

        [Test]
        public void CancelOrder_TwoProductsAreAddedToOrderThenCancelOrderIsCalled_OrderDaoClearOrderIsCalledOnce()
        {
            var productOne = new Product("Beer", 18, true);
            var productTwo = new Product("Vodka", 40, true);

            _salesController.AddProductToOrder(productOne, 1, null);
            _salesController.AddProductToOrder(productTwo, 1, null);
            _salesController.CancelOrder();

            _orderDao.Received(1).ClearOrder(Arg.Any<SalesOrder>());
        }
    }
}