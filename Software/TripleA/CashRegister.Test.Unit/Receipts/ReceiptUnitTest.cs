using System.Collections.Generic;
using CashRegister.Receipts;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Receipts
{
    [TestFixture]
    public class ReceiptUnitTest
    {
        [Test]
        public void Ctor_InitiateReceipt_ContentIsListOfString()
        {
            var uut = new Receipt();
            Assert.That(uut.Content, Is.TypeOf<List<string>>());
        }

        [Test]
        public void Add_AddsAStringToTheReceipt_ContentContainsString()
        {
            var uut = new Receipt();

            uut.Add("Test");

            Assert.That(uut.Content, Contains.Item("Test"));
        }

        [Test]
        public void AddLine_AddsAStringToTheReceipt_ContentContainsString()
        {
            var uut = new Receipt();

            uut.AddLine("Test");

            Assert.That(uut.Content, Contains.Item("Test"));
        }
    }
}