using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CashRegister.CashDrawers;
using CashRegister.Dal;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Printer;
using CashRegister.Products;
using CashRegister.Receipts;
using CashRegister.Sales;
using NUnit.Framework;
using NSubstitute;

namespace CashRegister.Test.Integration
{
    [TestFixture]
    public class SalesOrderControllerOrderControllerPaymentControllerProductControllerReceiptControllerDao
    {
        private int _raisedEvent;
        private Product _product;
        private Discount _discount;

        private IPrinter _printer;
        private ICashDrawer _cashDrawer;
        private IDalFacade _dalFacade;

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
            _raisedEvent = 0;
            _discount = new Discount();
            _product = new Product("Test", 100, true);

            _printer = Substitute.For<IPrinter>();
            _cashDrawer = Substitute.For<ICashDrawer>();
            _dalFacade = Substitute.For<IDalFacade>();

            _productDao = new ProductDao(_dalFacade);
            _paymentDao = new PaymentDao(_dalFacade);
            _orderDao = new OrderDao(_dalFacade);
            _receiptController = new ReceiptController(_printer);
            _productController = new ProductController(_productDao);
            var paymentProviders = new List<IPaymentProvider> { new CashPayment() };
            _paymentController = new PaymentController(paymentProviders, _receiptController, _paymentDao, _cashDrawer);
            _orderController = new OrderController(_orderDao);
            _salesController = new SalesController(_orderController, _receiptController, _productController,
                _paymentController);
        }

        [Test]
        public void AddProductToOrder_SalesControllerCallsDalFacade_ProductAdded()
        {
            _salesController.AddProductToOrder(_product, 1, _discount);
            _dalFacade.UnitOfWork.OrderLineRepository.Received(1).Insert(Arg.Any<OrderLine>());
        }


        private void SalesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _raisedEvent++;
        }

        [Test]
        public void INotifyPropertyChanged_SalesControllerRaisesEvent_PropertyChangedIsCalledOnce()
        {
            _salesController.PropertyChanged += SalesPropertyChanged;
            _salesController.AddProductToOrder(_product, 1, _discount);
            Assert.AreEqual(1,_raisedEvent);
        }

        [Test]
        public void INotifyPropertyChanged_SalesControllerRaisesEvent_PropertyChangedIsCalledTenTimes()
        {
            _salesController.PropertyChanged += SalesPropertyChanged;
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            Assert.AreEqual(10, _raisedEvent);
        }

        [Test]
        public void RemoveProductFromOrder_SalesControllerCallsDalFacade_OrderLineRepositoryInserIsCalled()
        {
            _salesController.RemoveProductFromOrder(_product, 1, _discount);
            _dalFacade.UnitOfWork.OrderLineRepository.Received(1).Insert(Arg.Any<OrderLine>());
        }

        [Test]
        public void StartPayment_SalesControllerCallsDalFacade_SalesOrderRepositoryUpdateIsCalled()
        {
           _salesController.StartPayment(0,"",PaymentType.Cash);
           _dalFacade.UnitOfWork.SalesOrderRepository.Received(2).Update(Arg.Any<SalesOrder>());  
        }

        [Test]
        public void CreateTransaction_SalesControllerCallsDalFacade_TransactionRepositoryInsertIsCalled()
        {
            _salesController.CreateTransaction(0, "", PaymentType.Cash);
            _dalFacade.UnitOfWork.TransactionRepository.Received(1).Insert(Arg.Any<Transaction>());
        }

        [Test]
        public void CreateTransaction_SalesControllerCallsDalFacade_SalesOrderRepositoryUpdateIsCalled()
        {
            _salesController.CreateTransaction(0, "", PaymentType.Cash);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void SaveIncompleteOrder_SalesControllerCallsDalFacade_SalesOrderRepositoryUpdateIsCalled()
        {
            _salesController.SaveIncompleteOrder();
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void MissingPaymentOnOrder_CallsMissingAmountOnCurrentOrder_MissingAmountIsReturned()
        {
           var answer = _salesController.MissingPaymentOnOrder();
            Assert.AreEqual(_orderController.CurrentOrder.Total - _orderController.CurrentOrder.Transactions.Sum(t => t.Price),answer);
        }

        [Test]
        public void CancelOrder_CallsCancelOrderWithTransaction_SaveOrderIsCalledFiveTimes()
        {
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.StartPayment(100,"",PaymentType.Cash);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(5).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void CancelOrder_CallsCancelOrderWithTransaction_SaveOrderIsCalledSixTimes()
        {
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.StartPayment(100, "", PaymentType.Cash);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(6).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void CancelOrder_CallsCancelOrderWithOutTransaction_SalesOrderRepositoryUpdateIsCalled()
        {
            _salesController.CancelOrder();
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Any<SalesOrder>());
        }
    }
}
