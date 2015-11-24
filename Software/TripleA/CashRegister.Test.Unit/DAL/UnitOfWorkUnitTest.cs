using System;
using CashRegister.Dal;
using CashRegister.Database;
using CashRegister.Models;
using NSubstitute;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Dal
{
    [TestFixture]
    public class UnitOfWorkUnitTest
    {
        private IDalFacade _dalFacade;

        [SetUp]
        public void SetUp()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            _dalFacade = Substitute.For<IDalFacade>();
        }

        [Test]
        public void DiscountRepository_DiscountRepositoryIsCalled_IsTypeOfRepositoryDiscount()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.DiscountRepository, Is.TypeOf<Repository<Discount>>());
            }
        }

        [Test]
        public void OrderLineRepository_OrderLineRepositoryIsCalled_IsTypeOfOrderLineDiscount()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.OrderLineRepository, Is.TypeOf<Repository<OrderLine>>());
            }
        }

        [Test]
        public void ProductRepository_DiscountRepositoryIsCalled_IsTypeOfRepositoryProduct()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductRepository, Is.TypeOf<Repository<Product>>());
            }
        }

        [Test]
        public void ProductGroupRepository_ProductGroupRepositoryIsCalled_IsTypeOfRepositoryProductGroup()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductGroupRepository, Is.TypeOf<Repository<ProductGroup>>());
            }
        }

        [Test]
        public void ProductTypeRepository_ProductTypeRepositoryIsCalled_IsTypeOfRepositoryProductType()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductTypeRepository, Is.TypeOf<Repository<ProductType>>());
            }
        }

        [Test]
        public void ProductTabRepository_ProductTabRepositoryIsCalled_IsTypeOfRepositoryProductTab()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductTabRepository, Is.TypeOf<Repository<ProductTab>>());
            }
        }

        [Test]
        public void SalesOrderRepository_SalesOrderRepositoryIsCalled_IsTypeOfRepositorySalesOrder()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.SalesOrderRepository, Is.TypeOf<Repository<SalesOrder>>());
            }
        }

        [Test]
        public void TransactionRepository_TransactionRepositoryIsCalled_IsTypeOfRepositoryTransaction()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.TransactionRepository, Is.TypeOf<Repository<Transaction>>());
            }
        }

        [Test]
        public void Dispose_WhenDisposed_DalFacadeReturnUnitOfWorkIsCalled()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new CashRegisterContext(connection, null);
            using (var uut = new UnitOfWork(context, _dalFacade))
            {
            }

            _dalFacade.Received(1).ReturnUnitOfWork();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dispose_WhenDisposed_ContextIsDisposed()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new CashRegisterContext(connection, null);
            using (var uut = new UnitOfWork(context, _dalFacade))
            {
            }

            context.Products.Find((long) 1);
        }

        [Test]
        public void Save_SaveAProductToTheRepository_ProductNameIsReturned()
        {
            var testProduct = new Product("Kildevand", 18, true);
            var connection = Effort.DbConnectionFactory.CreatePersistent("Save");

            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                uut.ProductRepository.Insert(testProduct);
                uut.Save();
            }

            using (var context = new CashRegisterContext(connection, null))
            {
                var uut = new UnitOfWork(context, _dalFacade);
                var result = uut.ProductRepository.GetById((long) 1);
                Assert.That(result.Name, Is.EqualTo("Kildevand"));
            }
        }
    }
}