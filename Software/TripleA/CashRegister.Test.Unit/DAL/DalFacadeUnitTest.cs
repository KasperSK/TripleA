using System;
using CashRegister.Dal;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Dal
{
    public class DalFacadeUnitTest
    {
        [SetUp]
        public void SetUp()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();           
        }

        [Test]
        public void UnitOfWork_UnitOfWorkIsCalled_ReturnsObjectOfTypeUnitOfWork()
        {
            var uut = new DalFacade();

            Assert.That(uut.UnitOfWork, Is.TypeOf<UnitOfWork>());
        }

        [Test]
        public void UnitOfWork_UnitOfWorkIsCalledWithDbConnection_ReturnsObjectOfTypeUnitOfWork()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var uut = new DalFacade { DbConnection = connection };

            Assert.That(uut.UnitOfWork, Is.TypeOf<UnitOfWork>());
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnitOfWork_UnitOfWorkIsCalledTwoTimes_InvalidOperationExceptionThrown()
        {
            var uut = new DalFacade();

            var result1 = uut.UnitOfWork;
            var result2 = uut.UnitOfWork;
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dispose_WhenDisposed_UnitOfWorkIsDisposed()
        {
            var uut = new DalFacade();
            var result = uut.UnitOfWork;
            uut.Dispose();

            result.ProductRepository.GetById((long) 1);
        }
    }
}