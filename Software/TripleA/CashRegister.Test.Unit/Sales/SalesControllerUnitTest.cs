using System.Collections.Generic;
using System.Linq;
using CashRegister.Database;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Receipts;
using CashRegister.Sales;
using CashRegister.Printer;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Sales
{
    /*
    [TestFixture]
    public class SalesControllerUnitTest
    {
        private IOrderController orderctrl;
        private IReceiptController receiptctrl;
        private IPrinter printerctrl;
        private ISalesController uut;
        private Product testProduct;
        private Product testProduct2;
        private Product testProduct3;
        private Product testProduct4;


        [SetUp]
        public void Setup()
        {
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
            uut.AddProductToOrder(product);
        }
        
            
        

        [Test]
        public void Ctor_AddProductToOrderList_ProductIsAdded()
        {
            AddingProduct(testProduct);
            Assert.That(uut.GetCurrentOrder().Lines.Any(p => p.Product == testProduct).Equals(true));
        }

        [Test] //Kig igen

        public void SalesController_GetCurrentOrder_CurrentOrderReturned()
        {
            uut.StartNewOrder();
            var OrderTest = uut.GetCurrentOrder();
            //OrderTest.Products.Add(testProduct);

            var COrderTest = uut.GetCurrentOrder();
            Assert.That(COrderTest.Equals(OrderTest));

        }

        [Test]
        public void SalesController_RemoveProductFromOrderList_ProductIsRemoved()
        {
            AddingProduct(testProduct);
            uut.RemoveProductFromOrder(testProduct);
            var current = uut.GetCurrentOrder();
            Assert.That(current.Lines.All(p => p.Product != testProduct));
        }

        [Test]
        public void SalesController_RemoveProductFromOrderList_ProductNotInCollection()
        {
            uut.RemoveProductFromOrder(testProduct);
            var current = uut.GetCurrentOrder();
            Assert.That(current.Lines.All(p => p.Product != testProduct));
        }

        [Test]
        public void SalesController_CreateAndPrintReceipt_PrintIsCalled()
        {
            uut.CreateAndPrintReceipt(uut.GetCurrentOrder());
            var print = receiptctrl.CreateReceipt(uut.GetCurrentOrder());
            receiptctrl.Received(1).Print(print);
        }

        [Test]
        public void SalesController_ClearOrder_OrderIsCleared()
        {
            AddingProduct(testProduct);
            AddingProduct(testProduct2);
            AddingProduct(testProduct3);
            AddingProduct(testProduct4);
            uut.ClearOrder();
            CollectionAssert.IsEmpty(uut.GetCurrentOrder().Lines);
        }


    
    }
    */
}