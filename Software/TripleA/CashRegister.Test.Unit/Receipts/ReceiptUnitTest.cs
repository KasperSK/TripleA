using System.Collections.Generic;
using CashRegister.Receipts;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Receipts
{
    [TestFixture]
    public class ReceiptUnitTest
    {
        [Test]
        public void Ctor_InitiateRecieptAndTypeOfContentIsListOfString_ContentIsListOfString()
        {
            var uut = new Receipt();
            Assert.That(uut.Content, Is.TypeOf<List<string>>());
        }
    }
}