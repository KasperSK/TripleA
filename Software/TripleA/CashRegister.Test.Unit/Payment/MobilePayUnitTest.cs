using CashRegister.Models;
using CashRegister.Payment;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Payment
{
    public class MobilePayUnitTest
    {
        private IPaymentProvider _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new MobilePay();
        }

        [Test]
        public void Ctor_OnInitialize_TypeIsNets()
        {
            Assert.That(_uut.Type, Is.EqualTo(PaymentType.MobilePay));
        }

        [Test]
        public void Ctor_OnInitialize_NameIsExpected()
        {
            const string expected = "MobilePay";
            Assert.That(_uut.Name, Is.EqualTo(expected));
        }

        [Test]
        public void Ctor_OnInitialize_DescriptionIsExpected()
        {
            const string expected = "MobilePay from Danske Bank";
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