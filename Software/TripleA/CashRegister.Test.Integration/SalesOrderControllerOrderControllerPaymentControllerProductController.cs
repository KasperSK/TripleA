using System.Collections.Generic;
using CashRegister.CashDrawers;
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
    public class SalesOrderControllerOrderControllerPaymentControllerProductController
    {      
        private IReceiptController _receiptController;
        private ICashDrawer _cashDrawer;
        private IProductDao _productDao;
        private IPaymentDao _paymentDao;
        private IOrderDao _orderDao;

        private IProductController _productController;
        private IPaymentController _paymentController;
        private IOrderController _orderController;
        private ISalesController _salesController;

        [SetUp]
        public void SetUp()
        {
            _receiptController = Substitute.For<IReceiptController>();
            _cashDrawer = Substitute.For<ICashDrawer>();
            _productDao = Substitute.For<IProductDao>();
            _paymentDao = Substitute.For<IPaymentDao>();
            _orderDao = Substitute.For<IOrderDao>();

            var paymentProviders = new List<IPaymentProvider> {new CashPayment()};
            _productController = new ProductController(_productDao);
            _paymentController = new PaymentController(paymentProviders, _receiptController, _paymentDao, _cashDrawer);
            _orderController = new OrderController(_orderDao);
            _salesController = new SalesController(_orderController, _receiptController, _productController,
                _paymentController);
        }

        [Test]
        public void ProductTabs_GetCurrentProductTabs_ProductDaoGetProductTabsIsCalledOnce()
        {
            var productTabs = _salesController.ProductTabs;

            _productDao.Received(1).GetProductTabs(Arg.Any<bool>());
        }
    }
}