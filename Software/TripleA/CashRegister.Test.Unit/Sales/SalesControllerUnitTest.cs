using System;
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

        [SetUp]
        public void Setup()
        {
            _orderctrl = Substitute.For<IOrderController>();
            _paymentController = Substitute.For<IPaymentController>();
            _productController = Substitute.For<IProductController>();
            _receiptctrl = Substitute.For<IReceiptController>();

            _trans = Substitute.For<Transaction>();
            
            _product = new Product("Fedt", 100, true);
            _discount = new Discount
            {
                Description = "Test Discount",
                Id = 0,
                Percent = 10
            };
            _uut = new SalesController(_orderctrl, _receiptctrl, _productController, _paymentController);

        }

        private void AddingProductToOrder(Product product, int quantity, Discount discount)
        {
            _uut.AddProductToOrder(product, quantity, discount);
        }


        [Test]
        public void CTorSalesController_SettingLocalPaymentProviderList_ListIsSet()
        {

        }

        [Test]
        public void SalesController_CallingAddProductOrderController_OrderControllerAddingProductIsCalled()
        {
            //_uut.AddProductToOrder(_product, 1,_discount);
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
        public void SalesController_CancelOrder_StartNewOrderIsCalled()
        {
            //AddingProductToOrder(_product, 1, _discount);
            _uut.CancelOrder();
            _orderctrl.Received(2).CreateNewOrder();

        }
        [Test]
        public void SalesController_CancelOrder_OrderControllerClearOrderIsCalled()
        {
            _uut.CancelOrder();
            _orderctrl.Received(1).ClearOrder();
        }
        [Test]
        public void SalesController_CancelOrder_OrderControllerMissingAmountIsCalled()
        {
            _uut.CancelOrder();
            _orderctrl.Received(1).MissingAmount();

        }
        [Test]
        public void SalesController_CancelOrder_OrderControllerSaveOrderIsCalled()
        {
            _uut.CancelOrder();
            _orderctrl.Received(1).SaveOrder();
        }

        [Test]
        public void SalesController_MissingPayment_OrderControllerMissingAmountIsCalled()
        {
            _uut.MissingPaymentOnOrder();
            _orderctrl.Received(1).MissingAmount();
        }
/*
        [Test]
        public void SalesController_GetIncompleteOrders_OrdercontrollerStashedOrderIsCalled()
        {
            //_orderctrl.StashedOrders.Returns(new List<SalesOrder>());
           _orderctrl.StashedOrders.Returns(x => new List<SalesOrder>());
            Assert.That(_uut.GetIncompleteOrders(), Is.TypeOf<List<SalesOrder>>());
            
        }
*/
        [Test]
        public void SalesController_AddTransaction_OrderControllerAddTransactionIsCalled()
        {
            _uut.AddTransaction(_trans);
            _orderctrl.Received(1).AddTransaction(Arg.Any<Models.Transaction>());
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
            _orderctrl.Received(2).CreateNewOrder();
        }
    }
}
