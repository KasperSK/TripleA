using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.Dal;
using CashRegister.Models;
using CashRegister.Orders;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Orders
{
    [TestFixture]
    public class OrderDaoUnitTest
    {
        private IDalFacade _dalFacade;
        private IOrderDao _uut;

        private SalesOrder _salesOrderOne;
        private SalesOrder _salesOrderThree;
        List<SalesOrder> _orderedSalesOrders;

        [SetUp]
        public void SetUp()
        {
            _salesOrderOne = new SalesOrder { Id = 1 };
            var salesOrderTwo = new SalesOrder { Id = 2 };
            _salesOrderThree = new SalesOrder { Id = 3 };
            var salesOrders = new List<SalesOrder> { salesOrderTwo, _salesOrderOne, _salesOrderThree };

            _dalFacade = Substitute.For<IDalFacade>();
            _dalFacade.UnitOfWork.SalesOrderRepository
                .When(x => x.Get(null, Arg.Any<Func<IQueryable<SalesOrder>, IOrderedQueryable<SalesOrder>>>()))
                .Do(x => _orderedSalesOrders = x.Arg<Func<IQueryable<SalesOrder>, IOrderedQueryable<SalesOrder>>>().Invoke(salesOrders.AsQueryable()).ToList());
            _dalFacade.UnitOfWork.SalesOrderRepository
                .Get(null, Arg.Any<Func<IQueryable<SalesOrder>, IOrderedQueryable<SalesOrder>>>())
                .Returns(salesOrders);

            _uut = new OrderDao(_dalFacade);
        }

        [Test]
        public void Delete_SalesOrderIsDeleted_SalesOrderRepositoryDeleteIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            _uut.Delete(salesOrder);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Delete(Arg.Is(salesOrder));
        }

        [Test]
        public void Delete_SalesOrderIsDeleted_SaveIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            _uut.Delete(salesOrder);
            _dalFacade.UnitOfWork.Received(1).Save();
        }

        [Test]
        public void Update_SalesOrderIsUpdated_SalesOrderRepositorUpdateIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            _uut.Update(salesOrder);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Is(salesOrder));
        }

        [Test]
        public void Update_SalesOrderIsUpdated_SaveIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            _uut.Update(salesOrder);
            _dalFacade.UnitOfWork.Received(1).Save();
        }

        [Test]
        public void Insert_SalesOrderIsInserted_SalesOrderRepositoryInsertIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            _uut.Insert(salesOrder);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Insert(Arg.Is(salesOrder));
        }

        [Test]
        public void Insert_SalesOrderIsInserted_SaveIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            _uut.Insert(salesOrder);
            _dalFacade.UnitOfWork.Received(1).Save();
        }

        [Test]
        public void SelectById_SalesOrderWithId1IsRequested_SalesOrderRepositorySelectByIdIsCalledOnce()
        {
            _uut.SelectById(1);
            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).GetById(Arg.Is((long)1));
        }

        [Test]
        public void AddOrderLine_OrderLineIsAddedToSalesOrder_SalesOrderRepositoryUpdateIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            var orderLine = new OrderLine {SalesOrder = salesOrder};

            _uut.AddOrderLine(orderLine);

            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Is(salesOrder));
        }

        [Test]
        public void AddOrderLine_OrderLineIsAddedToSalesOrder_SalesOrderRepositoryInsertIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            var orderLine = new OrderLine { SalesOrder = salesOrder };

            _uut.AddOrderLine(orderLine);

            _dalFacade.UnitOfWork.OrderLineRepository.Received(1).Insert(Arg.Is(orderLine));
        }

        [Test]
        public void AddOrderLine_OrderLineIsAddedToSalesOrder_SaveIsCalled()
        {
            var salesOrder = new SalesOrder();
            var orderLine = new OrderLine { SalesOrder = salesOrder };

            _uut.AddOrderLine(orderLine);

            _dalFacade.UnitOfWork.Received(1).Save();
        }
        
        [Test]
        public void ClearOrder_OrderlinesAreClearedFromSalesOrder_SalesOrderRepositoryUpdateIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            var orderLine = new OrderLine();
            salesOrder.Lines.Add(orderLine);

            _uut.ClearOrder(salesOrder);

            _dalFacade.UnitOfWork.SalesOrderRepository.Received(1).Update(Arg.Is(salesOrder));
        }

        [Test]
        public void ClearOrder_OrderlinesAreClearedFromSalesOrder_SalesOrderRepositoryDeleteIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            var orderLine = new OrderLine();
            salesOrder.Lines.Add(orderLine);

            _uut.ClearOrder(salesOrder);

            _dalFacade.UnitOfWork.OrderLineRepository.Received(1).Delete(Arg.Is(orderLine));
        }

        [Test]
        public void ClearOrder_OrderlinesAreClearedFromSalesOrder_SaveIsCalledOnce()
        {
            var salesOrder = new SalesOrder();
            var orderLine = new OrderLine();
            salesOrder.Lines.Add(orderLine);

            _uut.ClearOrder(salesOrder);

            _dalFacade.UnitOfWork.Received(1).Save();
        }

        [Test]
        public void GetLastOrders_NLastOrdersAreRequested_ResultCountIsEqualTo3()
        {
            var result = _uut.GetLastOrders(3);

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetLastOrders_NLastOrdersAreRequested_CollectionIsSorted()
        {
            _uut.GetLastOrders(3);

            Assert.That(_orderedSalesOrders.First(), Is.EqualTo(_salesOrderOne));
        }

        [Test]
        public void LastOrder_LastOrderIsRequested_LastOrderIsReturned()
        { 
            var result = _uut.LastOrder;

            Assert.That(result, Is.EqualTo(_salesOrderThree));
        }

        [Test]
        public void LastOrder_LastOrderIsRequested_CollectionIsSorted()
        {
            var result = _uut.LastOrder;

            Assert.That(_orderedSalesOrders.First(), Is.EqualTo(_salesOrderOne));
        }

        [Test]
        public void LastId_LastOrderIdIsRequested_LastOrderIsReturned()
        {
            var result = _uut.LastId;

            Assert.That(result, Is.EqualTo(_salesOrderThree.Id));
        }

        [Test]
        public void LastId_LastOrderIdIsRequested_CollectionIsSorted()
        {
            var result = _uut.LastId;

            Assert.That(_orderedSalesOrders.First(), Is.EqualTo(_salesOrderOne));
        }
    }
}