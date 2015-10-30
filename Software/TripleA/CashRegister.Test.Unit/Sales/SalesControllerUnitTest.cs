/*using System.Collections.Generic;
using System.Linq;
using CashRegister.Database;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Payment;
using CashRegister.Receipts;
using CashRegister.Sales;
using CashRegister.Printer;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Sales
{
    [TestFixture]
    public class SalesControllerUnitTest
    {
        private IOrderController _orderctrl;
        private IReceiptController _receiptctrl;
        private IPrinter _printerctrl;
        private ISalesController _uut;
        private ITransaction _transaction;
        private Product _testProduct;
        private Product _testProduct2;
        private Product _testProduct3;
        private Product _testProduct4;


        [SetUp]
        public void Setup()
        {
            _orderctrl = Substitute.For<OrderController>(Substitute.For<OrderDao>());
            _orderctrl.CreateOrder().Returns(new OrderList());

            _printerctrl = Substitute.For<ReceiptPrinter>();
            _transaction = Substitute.For<ITransaction>();
            _receiptctrl = Substitute.For<ReceiptController>(_printerctrl);
            _testProduct = new Product { ProductName = "test" };
            _testProduct2 = new Product { ProductName = "test" };
            _testProduct3 = new Product { ProductName = "test" };
            _testProduct4 = new Product { ProductName = "test" };
            _uut = new SalesController(_orderctrl, _receiptctrl);
            orderctrl = Substitute.For<OrderController>(Substitute.For<OrderDao>());
            orderctrl.CreateOrder().Returns(new OrderList());

            printerctrl = Substitute.For<ReceiptPrinter>();
            receiptctrl = Substitute.For<ReceiptController>(printerctrl);
            testProduct = new Product("test", 10, true);
            testProduct2 = new Product("test1", 11, true);
            testProduct3 = new Product("test2", 12, true);
            testProduct4 = new Product("test3", 13, false);
            uut = new SalesController(orderctrl, receiptctrl);
        }

        public void AddingProduct(Product product)
        {
            _uut.AddProductToOrder(product);
        }




        [Test]
        public void Ctor_AddProductToOrderList_ProductIsAdded()
        {
            AddingProduct(_testProduct);
            CollectionAssert.Contains(_uut.GetCurrentOrder().Products, _testProduct);
            AddingProduct(testProduct);
            Assert.That(uut.GetCurrentOrder().Lines.Any(p => p.Product == testProduct).Equals(true));
        }

        [Test] //Kig igen
        public void SalesController_GetCurrentOrder_CurrentOrderReturned()
        {
            _uut.StartNewOrder();
            var OrderTest = _uut.GetCurrentOrder();
            //OrderTest.Products.Add(testProduct);

            var COrderTest = _uut.GetCurrentOrder();
            Assert.That(COrderTest.Equals(OrderTest));
        }

        [Test]
        public void SalesController_RemoveProductFromOrderList_ProductIsRemoved()
        {
            AddingProduct(_testProduct);
            _uut.RemoveProductFromOrder(_testProduct);
            var Current = _uut.GetCurrentOrder();
            CollectionAssert.DoesNotContain(Current.Products, _testProduct);
            AddingProduct(testProduct);
            uut.RemoveProductFromOrder(testProduct);
            var current = uut.GetCurrentOrder();
            Assert.That(current.Lines.All(p => p.Product != testProduct));
        }

        [Test]
        public void SalesController_RemoveProductFromOrderList_ProductNotInCollection()
        {
            _uut.RemoveProductFromOrder(_testProduct);
            var Current = _uut.GetCurrentOrder();
            CollectionAssert.DoesNotContain(Current.Products, _testProduct);
            uut.RemoveProductFromOrder(testProduct);
            var current = uut.GetCurrentOrder();
            Assert.That(current.Lines.All(p => p.Product != testProduct));
        }

        [Test]
        public void SalesController_CreateAndPrintReceipt_PrintIsCalled()
        {
            _uut.CreateAndPrintReceipt(_uut.GetCurrentOrder());
            var print = _receiptctrl.CreateReceipt(_uut.GetCurrentOrder());
            _receiptctrl.Received(1).Print(print);
        }

        [Test]
        public void SalesController_ClearOrder_OrderIsCleared()
        {
            AddingProduct(_testProduct);
            AddingProduct(_testProduct2);
            AddingProduct(_testProduct3);
            AddingProduct(_testProduct4);
            _uut.ClearOrder();
            CollectionAssert.IsEmpty(_uut.GetCurrentOrder().Products);
            AddingProduct(testProduct);
            AddingProduct(testProduct2);
            AddingProduct(testProduct3);
            AddingProduct(testProduct4);
            uut.ClearOrder();
            CollectionAssert.IsEmpty(uut.GetCurrentOrder().Lines);
        }

        [Test]
        public void SalesController_CancelCurrentOrder_OrderCancelledNewOrderCreated()
        {
            var trans = new Transaction();
            _uut.ClearOrder();
            CollectionAssert.IsEmpty(_uut.GetCurrentOrder().Products);


            var returnTrans = new Transaktion();

            ClearOrder();
            foreach (var Transaktion in CurrentOrder.Transaktions)
            {
                returnTrans.TransaktionPrice -= Transaktion.TransaktionPrice;
            }
            CurrentOrder.Transaktions.Add(returnTrans);

            StartNewOrder();
        }

        [Test]
        public void SalesController_SaveIncompleteOrder_IncompleteOrderSaveNewOrderCreated()
        {
            var Spa = _uut.GetCurrentOrder();
           _uut.SaveIncompleteOrder();
            Assert.That(_uut
        }

        [Test]
        public void SalesController_AddTransaction_TransactionAdded()
        {
            var trans = new Transaktion();
            trans.TransaktionPrice = 100;
            _uut.AddTransaction(trans);
            CollectionAssert.Contains(_uut.GetCurrentOrder().Transaktions, trans);
        }
    }
}
*/