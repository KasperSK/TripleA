using System.Collections.Generic;
using System.Linq;
using CashRegister.Models;
using CashRegister.Orders;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Orders
{
    [TestFixture]
    public class OrderControllerUnitTest
    {
        private IOrderDao _dao;
        private IOrderController _uut;

        static readonly int[] _oddNumbers = {1, 3, 5, 7, 9};

        [SetUp]
        public void SetUp()
        {
            _dao = Substitute.For<IOrderDao>();

            _dao.When(p => p.AddOrderLine(Arg.Any<OrderLine>())).Do(p => { p.Arg<OrderLine>().SalesOrder.Lines.Add(p.Arg<OrderLine>()); });
            _dao.When(p => p.ClearOrder(Arg.Any<SalesOrder>())).Do(p => { p.Arg<SalesOrder>().Lines.Clear(); });

            _uut = new OrderController(_dao);
        }

        [Test]
        public void Ctor_InitializesStashedOrdersToAListOfSalesOrder_IsInitialized()
        {
            Assert.That(_uut.StashedOrders, Is.TypeOf<List<SalesOrder>>());
        }

        [Test]
        public void Ctor_CurrentOrderIsInitialized_IsInitialized()
        {
            Assert.That(_uut.CurrentOrder, Is.InstanceOf<SalesOrder>());
        }

        [Test]
        public void CreateOrder_CreatesNewSalesOrder_SalesOrderIsCreated()
        {
            Assert.That(_uut.CurrentOrder, Is.TypeOf<SalesOrder>());
        }

        [Test]
        public void CreateOrder_CreatesNewSalesOrder_OrderLineListIsCreated()
        {
            Assert.That(_uut.CurrentOrder.Lines, Is.TypeOf<List<OrderLine>>());
        }

        [Test]
        public void CreateOrder_CreatesNewSalesOrder_TransactionListIsCreated()
        {
            Assert.That(_uut.CurrentOrder.Transactions, Is.TypeOf<List<Transaction>>());
        }

        [Test]
        public void CreateOrder_CalledTwoTimesOrderIsStashed_SalesOrderIsStashed()
        {
            var uutOrder = _uut.CurrentOrder;
            _uut.StashCurrentOrder();

            Assert.That(_uut.StashedOrders, Contains.Item(uutOrder));
        }

        [Test]
        public void CreateOrder_CalledTwoTimesOrderIsStashed_StashedOrderIsNotEqualsToCurrentOrder()
        {
            var uutOrder = _uut.CurrentOrder;
            _uut.SaveOrder();

            Assert.That(_uut.CurrentOrder, Is.Not.EqualTo(uutOrder));
        }

        [Test]
        public void SaveOrder_WhenNewOrderOrderDaoInsertIsCalled_OrderDaoInsertIsCalled()
        {
            var uutOrder = _uut.CurrentOrder;
            _uut.SaveOrder();

            _dao.Received().Insert(Arg.Is(uutOrder));
        }

        [Test]
        public void SaveOrder_CurrentOrderIsSaved_CurrentOrderIsNew()
        {
            var order = _uut.CurrentOrder;
            _uut.SaveOrder();
            Assert.That(_uut.CurrentOrder, Is.Not.EqualTo(order));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18_TotalIs18()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(18));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18_ProductIsAddedToOrderLines()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer);
            var uutLines = _uut.CurrentOrder.Lines.ToList();

            Assert.That(uutLines[0].Product, Is.EqualTo(beer));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18_QuantityIsOne()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer);
            var uutLines = _uut.CurrentOrder.Lines.ToList();

            Assert.That(uutLines[0].Quantity, Is.EqualTo(1));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18Quantity3ToCurrentOrder_TotalIs3Times18()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(3 * 18));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18QuantityMinus3ToCurrentOrder_TotalIsMinus3Times18()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, -3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(-3 * 18));
        }

        [Test]
        public void AddProduct_AddProduct2TimesWithPrice18Quantity3ToCurrentOrder_TotalIs2Times3Times18()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            _uut.AddProduct(beer, 3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(2 * 3 * 18));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18Quantity3ThenAddProductWithPrice18QuantityMinus3ToCurrentOrder_TotalIs0()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            _uut.AddProduct(beer, -3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(0));
        }

        [Test, TestCaseSource(nameof(_oddNumbers))]
        public void AddProduct_SetProductQuantityN_QuantityIsN(int n)
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, n);
            var uutLines = _uut.CurrentOrder.Lines.ToList();

            Assert.That(uutLines[0].Quantity, Is.EqualTo(n));
        }

        [Test]
        public void AddProduct_AddProductToOrderLine_OrderLineIsProduct()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            var uutLines = _uut.CurrentOrder.Lines.ToList();

            Assert.That(uutLines[0].Product, Is.EqualTo(beer));
        }

        [Test]
        public void ClearOrder_ClearsCurrentOrderLines_CurrentOrderLinesIsCleared()
        {
            var beer = new Product("Øl", 18, true);
            
            _uut.AddProduct(beer, 3);
            _uut.ClearOrder();
            Assert.That(_uut.CurrentOrder.Lines, Is.Empty);
        }

        
        [Test]
        public void MissingAmount_CurrentOrderIsPopulatedWith3Times18ProductAndTransactionFor18IsDone_MissingAmountIs36()
        {
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);

            _uut.CurrentOrder.Transactions.Add(new Transaction() {Price = 18});
            
            Assert.That(_uut.MissingAmount(), Is.EqualTo(36));
        }
        

        [Test]
        public void MissingAmount_CurrentOrderIsEmpty_MissingAmountIs0()
        {
            Assert.That(_uut.MissingAmount(), Is.EqualTo(0));
        }

        [Test, TestCaseSource(nameof(_oddNumbers))]
        public void GetOrderById_CalledWithId_OrderDaoSelectByIdIsCalledWithId(int Id)
        {
            _uut.GetOrderById(Id);
            _dao.Received().SelectById(Id);
        }

        [Test, TestCaseSource(nameof(_oddNumbers))]
        public void GetNLastOrders_CalledWithN_OrderDaoGetNLastOrdersIsCalledWithN(int n)
        {
            _uut.GetLastOrders(n);
            _dao.Received().GetLastOrders(n);
        }

        [Test]
        public void GetStashedOrder_GetAStashedOrderThatDoesNotExist_CurrentOrderIsSame()
        {
            var order = _uut.CurrentOrder;
            _uut.GetStashedOrder(1);
            Assert.That(_uut.CurrentOrder, Is.EqualTo(order));
        }

        [Test]
        public void GetStashedOrder_GetAStashedOrder_CurrentOrderIsStashedOrder()
        {
            var orderCompare = _uut.CurrentOrder;
            _uut.StashCurrentOrder();

            _uut.GetStashedOrder(0);
            
            Assert.That(_uut.CurrentOrder, Is.EqualTo(orderCompare));
        }
    }
}