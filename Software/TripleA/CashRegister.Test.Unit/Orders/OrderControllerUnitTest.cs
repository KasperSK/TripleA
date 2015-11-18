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

        private readonly int[] _oddNumbers = {1, 3, 5, 7, 9};

        [SetUp]
        public void SetUp()
        {
            _dao = Substitute.For<IOrderDao>();
            _uut = new OrderController(_dao);
        }

        [Test]
        public void Ctor_InitializesStashedOrdersToAListOfSalesOrder_IsInitialized()
        {
            Assert.That(_uut.StashedOrders, Is.TypeOf<List<SalesOrder>>());
        }

        [Test]
        public void Ctor_CurrentOrderIsNotInitialized_IsNotInitialized()
        {
            Assert.That(_uut.CurrentOrder, Is.EqualTo(null));
        }

        [Test]
        public void CreateOrder_CreatesNewSalesOrder_SalesOrderIsCreated()
        {
            _uut.CreateNewOrder();
            Assert.That(_uut.CurrentOrder, Is.TypeOf<SalesOrder>());
        }

        [Test]
        public void CreateOrder_CreatesNewSalesOrder_OrderLineListIsCreated()
        {
            _uut.CreateNewOrder();
            Assert.That(_uut.CurrentOrder.Lines, Is.TypeOf<List<OrderLine>>());
        }

        [Test]
        public void CreateOrder_CreatesNewSalesOrder_TransactionListIsCreated()
        {
            _uut.CreateNewOrder();
            Assert.That(_uut.CurrentOrder.Transactions, Is.TypeOf<List<Transaction>>());
        }

        [Test]
        public void CreateOrder_CalledTwoTimesOrderIsStashed_SalesOrderIsStashed()
        {
            _uut.CreateNewOrder();
            var uutOrder = _uut.CurrentOrder;
            _uut.CreateNewOrder();

            Assert.That(_uut.StashedOrders, Contains.Item(uutOrder));
        }

        [Test]
        public void CreateOrder_CalledTwoTimesOrderIsStashed_StashedOrderIsNotEqualsToCurrentOrder()
        {
            _uut.CreateNewOrder();
            var uutOrder = _uut.CurrentOrder;
            _uut.CreateNewOrder();

            Assert.That(_uut.CurrentOrder, Is.Not.EqualTo(uutOrder));
        }

        [Test]
        public void SaveOrder_WhenNewOrderOrderDaoInsertIsCalled_OrderDaoInsertIsCalled()
        {
            _uut.CreateNewOrder();
            var uutOrder = _uut.CurrentOrder;
            _uut.SaveOrder();

            _dao.Received().Insert(Arg.Is(uutOrder));
        }

        [Test]
        public void UpdateOrder_CurrentOrderIsUpdated_OrderDaoUpdateIsCalledIsWithCurrentOrder()
        {
            _uut.CreateNewOrder();
            var uutOrder = _uut.CurrentOrder;
            _uut.UpdateOrder();
            _dao.Received().Update(Arg.Is(uutOrder));
        }

        [Test]
        public void SaveOrder_CurrentOrderIsSaved_CurrentOrderIsNull()
        {
            _uut.CreateNewOrder();
            _uut.SaveOrder();
            Assert.That(_uut.CurrentOrder, Is.EqualTo(null));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18Quantity3ToCurrentOrder_TotalIs3Times18()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(3 * 18));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18QuantityMinus3ToCurrentOrder_TotalIsMinus3Times18()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, -3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(-3 * 18));
        }

        [Test]
        public void AddProduct_AddProduct2TimesWithPrice18Quantity3ToCurrentOrder_TotalIs2Times3Times18()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            _uut.AddProduct(beer, 3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(2 * 3 * 18));
        }

        [Test]
        public void AddProduct_AddProductWithPrice18Quantity3ThenAddProductWithPrice18QuantityMinus3ToCurrentOrder_TotalIs0()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            _uut.AddProduct(beer, -3);
            Assert.That(_uut.CurrentOrder.Total, Is.EqualTo(0));
        }

        [Test, TestCaseSource(nameof(_oddNumbers))]
        public void AddProduct_SetProductQuantityN_QuantityIsN(int n)
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, n);
            var uutLines = _uut.CurrentOrder.Lines.ToList();

            Assert.That(uutLines[0].Quantity, Is.EqualTo(n));
        }

        [Test]
        public void AddProduct_AddProductToOrderLine_OrderLineIsProduct()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            var uutLines = _uut.CurrentOrder.Lines.ToList();

            Assert.That(uutLines[0].Product, Is.EqualTo(beer));
        }

        [Test]
        public void AddTransaction_AddsATransactionToTheSalesOrder_TransactionIsAddedToTheSalesOrder()
        {
            _uut.CreateNewOrder();
            var transactionOne = new Transaction() {Price = 120};
            _uut.AddTransaction(transactionOne);

            var uutTransactions = _uut.CurrentOrder.Transactions.ToList();

            Assert.That(uutTransactions[0], Is.EqualTo(transactionOne));
        }

        [Test]
        public void ClearOrder_ClearsCurrentOrderLines_CurrentOrderLinesIsCleared()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);
            
            _uut.AddProduct(beer, 3);
            _uut.ClearOrder();
            Assert.That(_uut.CurrentOrder.Lines, Is.Empty);
        }

        [Test]
        public void MissingAmount_CurrentOrderIsPopulatedWith3Times18ProductAndTransactionFor18IsDone_MissingAmountIs36()
        {
            _uut.CreateNewOrder();
            var beer = new Product("Øl", 18, true);

            _uut.AddProduct(beer, 3);
            _uut.AddTransaction(new Transaction() {Price = 18});
            
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
        public void GetStashedOrder_GetAStashedOrderThatDoesNotExist_CurrentOrderIsNull()
        {
            _uut.GetStashedOrder(1);
            Assert.That(_uut.CurrentOrder, Is.EqualTo(null));
        }

        [Test]
        public void GetStashedOrder_GetAStashedOrder_CurrentOrderIsStashedOrder()
        {
            _uut.CreateNewOrder();
            var orderCompare = _uut.CurrentOrder;
            _uut.CreateNewOrder();
            _uut.GetStashedOrder(0);
            
            Assert.That(_uut.CurrentOrder, Is.EqualTo(orderCompare));
        }
    }
}