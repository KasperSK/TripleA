using System;
using System.Linq;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Receipts;
using CashRegister.Sales;
using CashRegister.Printer;
using CashRegister.Products;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Sales
{
    [TestFixture]
    public class SalesControllerUnitTest
    {
        private IOrderController _orderctrl;
        private IReceiptController _receiptctrl;
        private IPaymentController _paymentController;
        private IProductController _productController;
        private ISalesController _uut;
        private Product _product;
        private Discount _discount;
        private Transaction _trans;
        private PaymentType _paymentType;
        private int _amountToPay;
        private string _description;
        private IPaymentController _paymentControllerFail;
        private IOrderController _orderControllerMissingNone;


        [SetUp]
        public void Setup()
        {   _amountToPay = 100;
            _orderctrl = Substitute.For<IOrderController>();
            _paymentController = Substitute.For<IPaymentController>();
            _paymentController.ExecuteTransaction(Arg.Any<Transaction>()).Returns(true);
            _paymentControllerFail = Substitute.For<IPaymentController>();
            _paymentControllerFail.ExecuteTransaction(Arg.Any<Transaction>()).Returns(false);
            _productController = Substitute.For<IProductController>();
            _orderctrl.MissingAmount().Returns(_amountToPay);
            _orderControllerMissingNone = Substitute.For<IOrderController>();
            _orderControllerMissingNone.MissingAmount().Returns(0);
            _receiptctrl = Substitute.For<IReceiptController>();
            _trans = Substitute.For<Transaction>();
            _orderctrl.CurrentOrder.Returns(new SalesOrder());
            _orderControllerMissingNone.CurrentOrder.Returns(new SalesOrder());
            _product = new Product("Fedt", 100, true);
            _paymentType = PaymentType.Cash;
           
            _description = "Description";
            _discount = new Discount
            {
                Description = "Test Discount",
                Id = 0,
                Percent = 10
            };
            _uut = new SalesController(_orderctrl, _receiptctrl, _productController, _paymentController);
        }

        private void GuiSalesController_Get_ReturnASalesController()
        {
            
        }

        private void AddingProductToOrder(Product product, int quantity, Discount discount)
        {
            _uut.AddProductToOrder(product, quantity, discount);
        }

        [Test]
        public void SalesController_CreateTransaction_transactionCompletedAndReturned()
        {
            var transaction = _uut.CreateTransaction(_amountToPay, _description, _paymentType);
            Assert.That(transaction,Is.TypeOf<Transaction>());
        }

        [Test]
        public void SalesController_CurrentOrder_CurrentOrderIsOrderControlCurrent()
        {
            Assert.That(_uut.CurrentOrder == _orderctrl.CurrentOrder );
        }

        [Test]
        public void SalesController_ProductTabs_ProductTabsIsproductControllerProductTabs()
        {
            Assert.That(_uut.ProductTabs==_productController.ProductTabs);
        }

        [Test]
        public void SalesController_IncompleteOrders_IncompleteOrdersIsOrderControllerStashedOrders()
        {
            Assert.That(_uut.IncompleteOrders == _orderctrl.StashedOrders);
        }

        [Test]
        public void SalesController_CreateAndPrintReceipt_ReceiptControllerCreateReceiptIsCalled()
        {
           _uut.CreateAndPrintReceipt();
            _receiptctrl.Received(1).CreateReceipt(_orderctrl.CurrentOrder);
        }

        [Test]
        public void SalesController_CreateAndPrintReceipt_OrderControllerSaveOrderIsCalled()
        {
            _uut.CreateAndPrintReceipt();
            _orderctrl.SaveOrder();
        }

        /*
        [Test]
        public void SalesController_StartPayment_Order()
        {      

           // AddingProductToOrder(_product, _amountToPay, _discount);      
            _uut.StartPayment(_amountToPay, _description, _paymentType);
            _orderctrl.Received(1).AddTransaction(Arg.Any<Transaction>());
        }
        */

        /*
        [Test]
        public void SalesController_StartPayment_OrderControllerAddTransactionIsCalled()
        {
            _uut.StartPayment(_amountToPay, _description, _paymentType);
            _orderctrl.Received(1).AddTransaction(Arg.Any<Transaction>());
        }
        */

        [Test]
        public void SalesController_StartPayment_OrderControllerSaveOrderIsCalled()
        {
            var uut = new SalesController(_orderControllerMissingNone, _receiptctrl, _productController,
                _paymentController);
            uut.StartPayment(_amountToPay, _description, _paymentType);
            _orderControllerMissingNone.Received(1).SaveOrder();
        }

        [Test]
        public void SalesController_PaymentProviderDescriptor_PaymentProviderDescriptorIsOrderControllerPaymentProviderDescriptor()
        {
            Assert.That(Equals(_uut.PaymentProviderDescriptor, _uut.PaymentProviderDescriptor));
        }

        [Test]
        public void SalesController_CreateTransaction_transactionFailedAndReturned()
        {
           
            var sales = new SalesController(_orderctrl, _receiptctrl, _productController, _paymentControllerFail);
            var transaction = sales.CreateTransaction(_amountToPay, _description, _paymentType);
            Assert.That(transaction.Description, Is.EqualTo("Transaction failed"));
        }

        [Test]
        public void SalesController_CallingAddProductOrderController_OrderControllerAddingProductIsCalled()
        {
            AddingProductToOrder(_product, 1, _discount);
            _orderctrl.Received(1).AddProduct(_product, 1, _discount);
        }

        [Test]
        public void SalesController_RemoveProductFromOrderList_OrderControllerAddingProductIsCalled()
        {
            AddingProductToOrder(_product, 1, _discount);
            _uut.RemoveProductFromOrder(_product, 1, _discount);
            _orderctrl.Received(1).AddProduct(_product, -1, _discount);
        }

        [Test]
        public void SalesController_ClearOrder_OrderControllerClearOrderIsCalled()
        {
            _uut.ClearOrder();
            _orderctrl.Received(1).ClearOrder();
        }

        [Test]
        public void SalesController_CancelOrder_StartNewOrderIsNotCalled()
        {
            _uut.CancelOrder();
            _orderctrl.Received(1).ClearOrder();
        }

        [Test]
        public void SalesController_CancelOrder_ClearOrderIsCalled()
        {
            var uut = new SalesController(_orderControllerMissingNone, _receiptctrl, _productController, _paymentController);
            uut.CancelOrder();
            _orderControllerMissingNone.Received(1).ClearOrder();
        }

        [Test]
        public void SalesController_CancelOrder_OrderControllerClearOrderIsCalled()
        {
            _uut.CancelOrder();
            _orderctrl.Received(1).ClearOrder();
        }

        [Test]
        public void SalesController_CancelOrder_OrderControllerSaveOrderIsCalled()
        {
            _uut.CancelOrder();
            _orderctrl.Received(0).SaveOrder();
        }

        [Test]
        public void SalesController_MissingPayment_OrderControllerMissingAmountIsCalled()
        {
            _uut.MissingPaymentOnOrder();
            _orderctrl.Received(1).MissingAmount();
        }

        [Test]
        public void SalesController_RetrieveIncompleteOrder_OrdercontrollerGetStashedOrderIsCalled()
        {
            _uut.RetrieveIncompleteOrder(1);
            _orderctrl.Received(1).GetStashedOrder(1);
        }

        [Test]
        public void SalesController_SaveIncompleteOrder_OrderControllerCreateNewOrderIsCalled()
        {
            _uut.SaveIncompleteOrder();
            _orderctrl.Received(1).SaveOrder();
        }
    }
}
