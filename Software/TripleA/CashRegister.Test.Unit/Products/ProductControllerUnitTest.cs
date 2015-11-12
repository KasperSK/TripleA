using System.Collections.Generic;
using System.Collections.ObjectModel;
using CashRegister.Models;
using CashRegister.Products;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Products
{
    [TestFixture]
    public class ProductControllerUnitTest
    {
        private IProductController _uut;
        private IProductDao _fakeProductDao;
        private ReadOnlyCollection<ProductTab> _tabs;

        [SetUp]
        public void ProductControllerSetup()
        {
            _tabs = new ReadOnlyCollection<ProductTab>(new List<ProductTab> {new ProductTab {Name = "Flaf"} });
            _fakeProductDao = Substitute.For<IProductDao>();
            _fakeProductDao.GetProductTabs(true).Returns(_tabs);
            _uut = new ProductController(_fakeProductDao);
        }

        [Test]
        public void ProductController_Constructor_CallDao()
        {
            _fakeProductDao.Received(1).GetProductTabs(true);
        }

        [Test]
        public void ProductController_ProductTabs_EqualsInput()
        {
            Assert.That(_uut.ProductTabs, Is.EqualTo(_tabs));
        }
    }
}