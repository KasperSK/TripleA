using CashRegister.Models;
using CashRegister.Payment;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Payment
{
    [TestFixture]
     public class CashPaymentUnitTest
    {
        private CashPayment _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new CashPayment();
        }

        [Test]
        public void Ctor_Revenue_Is0()
        {
            var uut = new CashPayment();

            Assert.That(uut.Revenue, Is.EqualTo(0));
        }

        [Test]
        public void TransferAmount_TestReturnValue_ReturnTrue()
        {
            var transferBool = _uut.TransferAmount(100, "Desciption");

            Assert.That(transferBool, Is.EqualTo(true));
        }

        [Test]
        public void Tally_Tranfer100_RevenueIs100()
        {
            _uut.TransferAmount(100, "Desciption");
            var amount = _uut.Revenue;

            Assert.That(amount, Is.EqualTo(100));
        }

        [Test]
        public void Tally_Transfer100and10_RevenueIs150()
        {
            _uut.TransferAmount(100, "Desciption");
            _uut.TransferAmount(50, "Desciption");

            var amount = _uut.Revenue;

            Assert.That(amount, Is.EqualTo(150));
        }

        [Test]
        public void PaymentType_TestType_TypeIsCash()
        {
            Assert.That(_uut.Type, Is.EqualTo(PaymentType.Cash));
        }

        [Test]
        public void TransactionStatus_TestReturn_ReturnTrue()
        {
            var statusBool = _uut.TransactionStatus();
            Assert.That(statusBool, Is.EqualTo(true));    
        }

        [Test]
        public void name_TestName_NameIsCashPayment()
        {
            Assert.That(_uut.Name, Is.EqualTo("CashPayment"));
        }

        [Test]
        public void Description_TestDescription_DiscriptionIsCashPayment()
        {
            Assert.That(_uut.Description, Is.EqualTo("CashPayment, nothing fancy here"));
        }
    }
}