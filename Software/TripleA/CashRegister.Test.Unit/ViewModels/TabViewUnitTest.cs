using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CashRegister.GUI.ViewModels;
using CashRegister.Models;
using CashRegister.Sales;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.ViewModels
{
    public class MakeProducts
    {
        private readonly Collection<ProductTab> _testCollection = new Collection<ProductTab>();
        public IReadOnlyCollection<ProductTab> TestData => _testCollection;

        public void AddProductTab(bool active, string color, int id, string name, int priority)
        {
            _testCollection.Add(new ProductTab
            {
                Active = active,
                Name = name,
                Id = id,
                Color = color,
                Priority = priority
            });
        }

        public void AddProductType(string color, int id, int price, string name, int tabId)
        {
            _testCollection.First(p => p.Id == tabId)
                .ProductTypes.Add(new ProductType {Color = color, Id = id, Name = name, Price = price});
        }

        public void AddProductGroup(string name, int id, int tabId, int typeId)
        {
            _testCollection.First(p => p.Id == tabId)
                .ProductTypes.First(p => p.Id == typeId)
                .ProductGroups.Add(new ProductGroup {Id = id, Name = name});
        }

        public void AddProduct(string name, int price, bool saleable, int tabId, int typeId, int groupId)
        {
            _testCollection.First(p => p.Id == tabId)
                .ProductTypes.First(p => p.Id == typeId)
                .ProductGroups.First(p => p.Id == groupId)
                .Products.Add(new Product(name, price, saleable));
        }

        public Product ReturnProduct(int tabId, int typeId, int groupId, string name)
        {
            return
                _testCollection.First(p => p.Id == tabId)
                    .ProductTypes.First(p => p.Id == typeId)
                    .ProductGroups.First(p => p.Id == groupId)
                    .Products.First(p => p.Name == name);
        }

        public ProductType RetrunProductType(int tabId, int typeId)
        {
            return _testCollection.First(p => p.Id == tabId).ProductTypes.First(p => p.Id == typeId);
        }
    }

    [TestFixture]
    public class TabViewUnitTest
    {
        [SetUp]
        public void SetUp()
        {
            _testProducts = new MakeProducts();
            _fakeSalesController = Substitute.For<ISalesController>();
            _fakeNumpad = Substitute.For<INumpad>();
            _uut = new TabViewModel(_fakeSalesController, _fakeNumpad);
            _fakeNotifyTest = Substitute.For<INotifyTest>();


            _testProducts.AddProductTab(true, "Red", 1, "RedStuff", 1);
            _testProducts.AddProductType("Red", 1, 20, "RedSnask", 1);
            _testProducts.AddProductGroup("RedHat", 2, 1, 1);
            _testProducts.AddProduct("GreenAle", 20, true, 1, 1, 2);
        }


        private MakeProducts _testProducts;

        private TabViewModel _uut;
        private ISalesController _fakeSalesController;
        private INumpad _fakeNumpad;
        private INotifyTest _fakeNotifyTest;

        [Test]
        public void AddProduct_ICommand_CallsSalesCtrl()
        {
            var command = _uut.AddProduct;
            command.Execute(new Product("YellowAle", 20, true));
            _fakeSalesController.Received(1).AddProductToOrder(Arg.Any<Product>(), Arg.Any<int>(), Arg.Any<Discount>());
        }

        [Test]
        public void BackgroundColor_SetColor_NotifyCalled()
        {
            _uut = new TabViewModel(_fakeSalesController, _fakeNumpad);
            _uut.PropertyChanged += _fakeNotifyTest.TestINotify;

            _uut.BackGroundColour = "Green";

            _fakeNotifyTest.Received(1).TestINotify(Arg.Any<object>(), Arg.Any<PropertyChangedEventArgs>());
        }

        [Test]
        public void ChangeTab_ICommand_SetsNewBgColor()
        {
            _testProducts.AddProductTab(true, "Yellow", 2, "YellowStuff", 1);
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            var command = _uut.ChangeTab;
            command.Execute(2);

            Assert.That(_uut.BackGroundColour, Is.EqualTo("Yellow"));
        }

        [Test]
        public void FetchView_TabItem_HasRightColor()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First().Colour, Is.EqualTo("Red"));
        }

        [Test]
        public void FetchView_TabItem_HasRightColumn()
        {
            _testProducts.AddProduct("BlueAle", 20, true, 1, 1, 2);
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First(p => p.Name == "BlueAle").Column, Is.EqualTo(1));
        }

        [Test]
        public void FetchView_TabItem_HasRightCommand()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First().Command, Is.EqualTo(_uut.AddProduct));
        }

        [Test]
        public void FetchView_TabItem_HasRightName()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First().Name, Is.EqualTo("GreenAle"));
        }

        [Test]
        public void FetchView_TabItem_HasRightProduct()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First().Product, Is.EqualTo(_testProducts.ReturnProduct(1, 1, 2, "GreenAle")));
        }

        [Test]
        public void FetchView_TabItem_HasRightProductType()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First().ProductType, Is.EqualTo(_testProducts.RetrunProductType(1, 1)));
        }

        [Test]
        public void FetchView_TabItem_HasRightRow()
        {
            _testProducts.AddProduct("BlueAle", 20, true, 1, 1, 2);
            _testProducts.AddProduct("PinkAle", 20, true, 1, 1, 2);
            _testProducts.AddProduct("BrownAle", 20, true, 1, 1, 2);
            _testProducts.AddProduct("GrayAle", 20, true, 1, 1, 2);
            _testProducts.AddProduct("YellowAle", 20, true, 1, 1, 2);
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabItems.First(p => p.Name == "YellowAle").Row, Is.EqualTo(1));
        }

        [Test]
        public void FetchView_TeabHead_HasRightColor()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabHead.First().Colour, Is.EqualTo("Red"));
        }

        [Test]
        public void FetchView_TeabHead_HasRightICommand()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabHead.First().Command, Is.EqualTo(_uut.ChangeTab));
        }

        [Test]
        public void FetchView_TeabHead_HasRightId()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabHead.First().Id, Is.EqualTo(1));
        }

        [Test]
        public void FetchView_TeabHead_HasRightName()
        {
            _fakeSalesController.ProductTabs.Returns(_testProducts.TestData);

            _uut.FetchView();

            Assert.That(_uut.TabHead.First().Name, Is.EqualTo("RedStuff"));
        }
    }
}