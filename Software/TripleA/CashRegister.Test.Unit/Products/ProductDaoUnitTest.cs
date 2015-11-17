using CashRegister.Dal;
using CashRegister.Products;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Products
{
    [TestFixture]
    public class ProductDaoUnitTest
    {
        [SetUp]
        public void ProductDaoSetup()
        {
            _fakeDalFacade = Substitute.For<IDalFacade>();
            _uut = new ProductDao(_fakeDalFacade);
        }

        private IProductDao _uut;
        private IDalFacade _fakeDalFacade;

        [Test]
        public void ProductDao_GetProductTabs_CallsDal()
        {
            _uut.GetProductTabs(true);
            var temp = _fakeDalFacade.Received(1).UnitOfWork;
        }
    }
}