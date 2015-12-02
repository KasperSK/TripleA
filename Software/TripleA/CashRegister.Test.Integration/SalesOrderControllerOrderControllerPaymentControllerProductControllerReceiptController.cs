using System.Collections.Generic;
using CashRegister.CashDrawers;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Printer;
using CashRegister.Products;
using CashRegister.Receipts;
using CashRegister.Sales;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Integration
{
    [TestFixture]
    public class SalesOrderControllerOrderControllerPaymentControllerProductControllerReceiptController
    {
        private ICashDrawer _cashDrawer;
        private IPrinter _printer;
        private IProductDao _productDao;
        private IPaymentDao _paymentDao;
        private IOrderDao _orderDao;

        private IReceiptController _receiptController;
        private IProductController _productController;
        private IPaymentController _paymentController;
        private IOrderController _orderController;
        private ISalesController _salesController;

        [SetUp]
        public void SetUp()
        {
            _cashDrawer = Substitute.For<ICashDrawer>();
            _printer = Substitute.For<IPrinter>();
            _productDao = Substitute.For<IProductDao>();
            _paymentDao = Substitute.For<IPaymentDao>();
            _orderDao = Substitute.For<IOrderDao>();

            var paymentProviders = new List<IPaymentProvider> { new CashPayment() };

            _receiptController = new ReceiptController(_printer);
            _productController = new ProductController(_productDao);
            _paymentController = new PaymentController(paymentProviders, _receiptController, _paymentDao, _cashDrawer);
            _orderController = new OrderController(_orderDao);
            _salesController = new SalesController(_orderController, _receiptController, _productController,
                _paymentController);
        }

        [Test]
        public void SalesController_PrintingAReceipt_PrinterIsCalled()
        {
            _salesController.CreateAndPrintReceipt();

            _printer.Received(1).Print();
        }
    }
}