using System;
using CashRegister.Dal;
using CashRegister.Database;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Dal
{
    public class DalFacadeUnitTest
    {
        [SetUp]
        public void SetUp()
        {
            using (var uut = new CashRegisterContext())
            {
                if (uut.Database.Exists())
                    uut.Database.Delete();
            }
        }

        [Test]
        public void UnitOfWork_UnitOfWorkIsCalled_ReturnsObjectOfTypeUnitOfWork()
        {
            var uut = new DalFacade();

            Assert.That(uut.UnitOfWork, Is.TypeOf<UnitOfWork>());
        }

        [Test]
        public void UnitOfWork_UnitOfWorkIsCalledTwoTimes_InvalidOperationExceptionThrown()
        {
            var uut = new DalFacade();

            var result1 = uut.UnitOfWork;

            Assert.That(() => uut.UnitOfWork, Throws.InvalidOperationException);
        }

        [Test]
        public void Dispose_WhenDisposed_UnitOfWorkIsDisposed()
        {
            var uut = new DalFacade();
            var result = uut.UnitOfWork;
            uut.Dispose();

            Assert.That(() => result.ProductRepository.GetById((long)1), Throws.TypeOf<InvalidOperationException>());
        }
    }
}