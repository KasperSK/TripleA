using System;
using System.Collections.Generic;
using CashRegister.Models;
using CashRegister.Printer;
using CashRegister.Receipts;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Receipts
{
    [TestFixture]
    public class ReceiptControllerUnitTest
    {
        private IPrinter _printer;
        private IReceiptController _uut;

        [SetUp]
        public void SetUp()
        {
            _printer = Substitute.For<IPrinter>();
            _uut = new ReceiptController(_printer);
        }

        [Test]
        public void CreateReceipt_GenerateReceiptFromAnEmptyTransaction_ReceiptContainsOneString()
        {
            var transaction = new Transaction();
            _uut.CreateReceipt(transaction);

            _printer.Received(1).Print();
        }

        [Test]
        public void CreateReceipt_GenerateReceiptFromASalesOrderWithNoOrderlines_ReceiptContainsSevenStrings()
        {
            var salesorder = new SalesOrder()
            {
                Id = 1,
                Date = new DateTime(2010, 10, 3, 12, 0, 0),
                Status = OrderStatus.Completed
            };

            _uut.CreateReceipt(salesorder);

            _printer.Received(1).Print();
        }

        [Test]
        public void CreateReceipt_GenerateReceiptFromASalesOrderWithOneOrderlines_ReceiptContainsEightStrings()
        {
            var orderline = new OrderLine()
            {
                Id = 1,
                Product = new Product("Øl", 18, true),
                Quantity = 2
            };

            var salesorder = new SalesOrder()
            {
                Id = 1,
                Date = new DateTime(2010, 10, 3, 12, 0, 0),
                Status = OrderStatus.Completed
            };

            salesorder.Lines.Add(orderline);

            _uut.CreateReceipt(salesorder);

            _printer.Received(1).Print();
        }
    }
}