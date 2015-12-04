using CashRegister.CashDrawers;
using NUnit.Framework;

namespace CashRegister.Test.Unit.CashDrawer
{
    [TestFixture]
    public class CashDrawerUnitTest
    {
        private ICashDrawer _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new CashDrawers.CashDrawer(1000);    
        }

        [Test]
        public void CashChange_CashDrawerIsInitializedWith1000_CashChangeReturns1000()
        {
            Assert.That(_uut.CashChange, Is.EqualTo(1000));
        }
    }
}