using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using CashRegister.CashDrawers;
using CashRegister.Dal;
using CashRegister.Database;
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
    public class UC1CashSale
    {
        private IDalFacade _dalFacade;
        private static IOrderDao _orderDao;
        private IOrderController _orderController;
        private IPrinter _printer;
        private IReceiptController _receiptController;
        private IPaymentController _paymentController;
        private IPaymentDao _paymentDao;
        private int _startAmount;
        private ICashDrawer _cashDrawer;
        private List<IPaymentProvider> _paymentProviders;
        private IProductController _productController;
        private IProductDao _productDao;
        private SalesController _salesController;
        private Product _product;
        private Discount _discount;
        private ProductGroup _productGroup;
        private CashRegisterContext _cashRegisterContext;
        private DbConnection _dbConnection;
        private int _raisedEvent;

        [SetUp]
        public void SetUp()
        {
            _dalFacade = Substitute.For<IDalFacade>();
            _orderDao = new OrderDao(_dalFacade);
            _orderController = new OrderController(_orderDao);
            _printer = new Printer.ReceiptPrinter();
            _receiptController = new ReceiptController(_printer);
            _paymentDao = new PaymentDao(_dalFacade);
            _cashDrawer = new CashDrawer(_startAmount);
            _paymentProviders = new List<IPaymentProvider>();
            _productDao = new ProductDao(_dalFacade);
            _productController = new ProductController(_productDao);
            _discount = new Discount();
            _cashRegisterContext = new CashRegisterContext();
            _productGroup = new ProductGroup();
            _product = new Product("Test", 100, true);
            _startAmount = 1000;
            _paymentProviders.Add(new CashPayment());
            _raisedEvent = 0;
            _paymentController = new PaymentController(_paymentProviders, _receiptController, _paymentDao, _cashDrawer);
            _salesController = new SalesController(_orderController, _receiptController, _productController,
                _paymentController);
        }

        [Test]
        public void AddProductToOrder_SalesControllerCallsDalFacade_ProductAdded()
        {
            _salesController.AddProductToOrder(_product, 1, _discount);
            _dalFacade.UnitOfWork.OrderLineRepository.Received(1).Insert(Arg.Any<OrderLine>());
        }


        private void sales_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _raisedEvent++;
        }

        [Test]
        public void INotifyPropertyChanged_SalesControllerRaisesEvent_PropertyChangedIsCalledOnce()
        {
            _salesController.PropertyChanged += sales_PropertyChanged;
            _salesController.AddProductToOrder(_product, 1, _discount);
            Assert.AreEqual(1,_raisedEvent);
        }

        [Test]
        public void INotifyPropertyChanged_SalesControllerRaisesEvent_PropertyChangedIsCalledTenTimes()
        {
            _salesController.PropertyChanged += sales_PropertyChanged;
            _salesController.AddProductToOrder(_product, 1, _discount); // 1
            _salesController.AddProductToOrder(_product, 1, _discount); // 2
            _salesController.AddProductToOrder(_product, 1, _discount); // 3
            _salesController.AddProductToOrder(_product, 1, _discount); // 4
            _salesController.AddProductToOrder(_product, 1, _discount); // 5
            _salesController.AddProductToOrder(_product, 1, _discount); // 6
            _salesController.AddProductToOrder(_product, 1, _discount); // 7
            _salesController.AddProductToOrder(_product, 1, _discount); // 8
            _salesController.AddProductToOrder(_product, 1, _discount); // 9
            _salesController.AddProductToOrder(_product, 1, _discount); // 10
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
           _salesController.StartPayment(0,"Bla",PaymentType.Cash);
           _dalFacade.UnitOfWork.SalesOrderRepository.Received(2).Update(Arg.Any<SalesOrder>());  
        }

        [Test]
        public void CreateTransaction_SalesControllerCallsDalFacade_TransactionRepositoryInsertIsCalled()
        {
            _salesController.CreateTransaction(0, "Bla", PaymentType.Cash);
            _dalFacade.UnitOfWork.TransactionRepository.Received(1).Insert(Arg.Any<Transaction>());
        }

        [Test]
        public void CreateTransaction_SalesControllerCallsDalFacade_SalesOrderRepositoryUpdateIsCalled()
        {
            _salesController.CreateTransaction(0, "Bla", PaymentType.Cash);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void SaveIncompleteOrder_SalesControllerCallsDalFacade_SalesOrderRepositoryUpdateIsCalled()
        {
            _salesController.SaveIncompleteOrder();
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Any<SalesOrder>());
        }
/*
        [Test]
        public void CreateAndPrintReceipt_SalesControllerCallDalfacade_Something1()
        {
            _salesController.CreateAndPrintReceipt();
            
            _printer.Received(1).Print();   
        }

        [Test]
        public void CreateAndPrintReceipt_SalesControllerCallDalfacade_Something()
        {

        }
        */

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
            _salesController.StartPayment(100,"100 kroner kontant",PaymentType.Cash);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(5).Update(Arg.Any<SalesOrder>());
        }

        [Test]
        public void CancelOrder_CallsCancelOrderWithTransaction_SaveOrderIsCalledSixTimes()
        {
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _salesController.StartPayment(100, "100 kroner kontant", PaymentType.Cash);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(6).Update(Arg.Any<SalesOrder>());
        }
/*
        [Test]
        public void CancelOrder_CallsCancelOrderWithOutTransaction_OrderLineRepositoryDeleteIsCalled()
        {
            _salesController.AddProductToOrder(_product,1,_discount);
            _salesController.CancelOrder();
            _dalFacade.UnitOfWork.OrderLineRepository.Received(1).Delete(Arg.Any<OrderLine>());

        }
*/
        [Test]
        public void CancelOrder_CallsCancelOrderWithOutTransaction_SalesOrderRepositoryUpdateIsCalled()
        {
            _salesController.CancelOrder();
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Any<SalesOrder>());
        }
/*
        [Test]
        public void GetStashedOrder_CallsStashedOrder_StashedOrderSelectedByIdAddedToCurrentOrder()
        {
            
            _salesController.AddProductToOrder(_product, 1, _discount);
            _orderController.SaveOrder();
           _salesController.AddProductToOrder(_product,1,_discount);
            _salesController.AddProductToOrder(_product, 1, _discount);
            _orderController.SaveOrder();
          _salesController.RetrieveIncompleteOrder(1);
            Assert.That(_salesController.CurrentOrder.Lines.Count,Is.EqualTo(2));
        }
*/






    }
}
