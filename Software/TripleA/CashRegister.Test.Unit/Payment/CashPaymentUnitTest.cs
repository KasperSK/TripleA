using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Models;
using CashRegister.Payment;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Payment
{
    [TestFixture]
    class CashPaymentUnitTest
    {
        private CashPayment _uut;
        private int _startChange;

        [SetUp]
        public void SetUp()
        {
            _startChange = 100;
            _uut = new CashPayment(_startChange);
        }

        [Test]
        public void Ctor_StartChangeIsSetTo1000_StartChangeIs1000()
        {
            var uut = new CashPayment(1000);

            Assert.That(uut.StartChange, Is.EqualTo(1000));
        }

        [Test]
        public void TransferAmount_TestReturnValue_ReturnTrue()
        {
            var transferBool = _uut.TransferAmount(100, "Desciption");

            Assert.That(transferBool, Is.EqualTo(true));
        }

        [Test]
        public void Tally_Tranfer100_TallyIs100()
        {
            _uut.TransferAmount(100, "Desciption");
            var amount = _uut.Tally();

            Assert.That(amount, Is.EqualTo(100));
        }

        [Test]
        public void Tally_Transfer100and10_TallyIs150()
        {
            _uut.TransferAmount(100, "Desciption");
            _uut.TransferAmount(50, "Desciption");

            var amount = _uut.Tally();

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

