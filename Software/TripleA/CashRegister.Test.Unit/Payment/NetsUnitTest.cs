using CashRegister.Models;
using CashRegister.Payment;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Payment
{
    [TestFixture]
    public class NetsUnitTest
    {
        private IPaymentProvider _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new Nets();
        }

        [Test]
        public void Ctor_OnInitialize_TypeIsNets()
        {
            Assert.That(_uut.Type, Is.EqualTo(PaymentType.Nets));
        }

        [Test]
        public void Ctor_OnInitialize_NameIsExpected()
        {
            const string expected = "Nets";
            Assert.That(_uut.Name, Is.EqualTo(expected));
        }

        [Test]
        public void Ctor_OnInitialize_DescriptionIsExpected()
        {
            const string expected = "Nets Dankort/Visa";
            Assert.That(_uut.Description, Is.EqualTo(expected));
        }

        [Test]
        public void TransferAmount_ATransferOf100IsMade_RevenueIsUpdated()
        {
            _uut.TransferAmount(100, "Initial payment");
            Assert.That(_uut.Revenue, Is.EqualTo(100));
        }

        [Test]
        public void TransferAmount_ATransferOfMinus100IsMade_RevenueIsUpdated()
        {
            _uut.TransferAmount(-100, "Initial payment");
            Assert.That(_uut.Revenue, Is.EqualTo(-100));
        }

        [Test]
        public void TransferAmount_TwoTransferOfA100EachIsMade_RevenueIsUpdated()
        {
            _uut.TransferAmount(100, "Initial payment");
            _uut.TransferAmount(100, "Second payment");
            Assert.That(_uut.Revenue, Is.EqualTo(200));
        }
    }
}