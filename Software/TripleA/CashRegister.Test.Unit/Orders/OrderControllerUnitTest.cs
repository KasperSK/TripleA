using CashRegister.Database;
using CashRegister.Orders;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Orders
{
    [TestFixture]
    public class OrderControllerUnitTest
    {
        private readonly long[] _testIds = new long[] {1, 3, 5, 8, 55, 98, 10010};
        private readonly int[] _testNs = new int[] {1, 3, 5, 7, 9};

        private IOrderDao _dao;
        private OrderController _uut;

        [SetUp]
        public void SetUp()
        {
            _dao = Substitute.For<IOrderDao>();
            _uut = new OrderController(_dao);
        }

        [Test]
        public void CreateOrder_OrderDaoInsertIsCalledWithAnOrderList_CallWasMade()
        {
            _uut.CreateOrder();
            _dao.Received().Insert(Arg.Any<OrderList>());
        }

        [Test]
        public void SaveOrder_OrderDaoInsertIsCalledWithTheOrderList_CallWasMade()
        {
            var order = new OrderList();
            _uut.SaveOrder(order);
            _dao.Received().Insert(Arg.Is<OrderList>(order));
        }

        [Test, TestCaseSource("_testIds")]
        public void GetOrderById_OrderDaoSelectByIdIsCalledWithSpecficId_CallWasMade(long id)
        {
            _uut.GetOrderById(id);
            _dao.Received().SelectById(Arg.Is<long>(id));
        }

        [Test, TestCaseSource("_testNs")]
        public void GetNLastOrders_OrderDaoGetNLatestOrdersIsCalledWithNumberOfListsToReturned_CallWasMade(int n)
        {
            _uut.GetNLastOrders(n);
            _dao.Received().GetNLastOrders(Arg.Is<int>(n));
        }

        [Test]
        public void ClearOrder_OrderDaoUpdateIsCalledWithAnOrderList_CallWasMade()
        {
            var order = new OrderList();
            _uut.ClearOrder(ref order);
            _dao.Received().Update(Arg.Is<OrderList>(order));
        }

        [Test]
        public void MissingAmount_CalledWithEmptyTransactionList_Returns0()
        {
            var order = new OrderList();
            Assert.That(_uut.MissingAmount(order), Is.EqualTo(0));
        }
    }
}