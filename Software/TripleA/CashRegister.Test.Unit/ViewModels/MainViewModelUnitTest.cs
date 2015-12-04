using CashRegister.GUI.ViewModels;
using CashRegister.Sales;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.ViewModels
{
    [TestFixture]
    public class MainViewModelUnitTest
    {
        private ISalesController _salesController;

        [SetUp]
        public void SetUp()
        {
            _salesController = Substitute.For<ISalesController>();
        }

        [Test]
        public void SalesViewModel_()
        {
            var uut = new MainViewModel(_salesController);

            Assert.That(uut.SalesViewModel, Is.TypeOf<SalesViewModel>());
        }

        [Test]
        public void NumpadViewModel_()
        {
            var uut = new MainViewModel(_salesController);

            Assert.That(uut.NumpadViewModel, Is.TypeOf<NumpadViewModel>());
        }

        [Test]
        public void TabViewModel_()
        {
            var uut = new MainViewModel(_salesController);

            Assert.That(uut.TabViewModel, Is.TypeOf<TabViewModel>());

        }
    }
}