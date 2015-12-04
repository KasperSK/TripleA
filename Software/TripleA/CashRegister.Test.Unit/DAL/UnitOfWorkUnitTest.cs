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
            _dalFacade = Substitute.For<IDalFacade>();

            using (var uut = new CashRegisterContext())
            {
                if (uut.Database.Exists())
                    uut.Database.Delete();
            }
        }

        [Test]
        public void DiscountRepository_DiscountRepositoryIsCalled_IsTypeOfRepositoryDiscount()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.DiscountRepository, Is.TypeOf<Repository<Discount>>());
            }
        }

        [Test]
        public void OrderLineRepository_OrderLineRepositoryIsCalled_IsTypeOfOrderLineDiscount()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.OrderLineRepository, Is.TypeOf<Repository<OrderLine>>());
            }
        }

        [Test]
        public void ProductRepository_DiscountRepositoryIsCalled_IsTypeOfRepositoryProduct()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductRepository, Is.TypeOf<Repository<Product>>());
            }
        }

        [Test]
        public void ProductGroupRepository_ProductGroupRepositoryIsCalled_IsTypeOfRepositoryProductGroup()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductGroupRepository, Is.TypeOf<Repository<ProductGroup>>());
            }
        }

        [Test]
        public void ProductTypeRepository_ProductTypeRepositoryIsCalled_IsTypeOfRepositoryProductType()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductTypeRepository, Is.TypeOf<Repository<ProductType>>());
            }
        }

        [Test]
        public void ProductTabRepository_ProductTabRepositoryIsCalled_IsTypeOfRepositoryProductTab()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.ProductTabRepository, Is.TypeOf<Repository<ProductTab>>());
            }
        }

        [Test]
        public void SalesOrderRepository_SalesOrderRepositoryIsCalled_IsTypeOfRepositorySalesOrder()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.SalesOrderRepository, Is.TypeOf<Repository<SalesOrder>>());
            }
        }

        [Test]
        public void TransactionRepository_TransactionRepositoryIsCalled_IsTypeOfRepositoryTransaction()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                Assert.That(uut.TransactionRepository, Is.TypeOf<Repository<Transaction>>());
            }
        }

        [Test]
        public void Dispose_WhenDisposed_DalFacadeReturnUnitOfWorkIsCalled()
        {
            var context = new CashRegisterContext();
            using (var uut = new UnitOfWork(context, _dalFacade))
            {
            }

            _dalFacade.Received(1).ReturnUnitOfWork();
        }

        [Test]
        public void Dispose_WhenDisposed_InvalidOperationExceptionIsThrown()
        {
            var context = new CashRegisterContext();
            using (var uut = new UnitOfWork(context, _dalFacade))
            {
            }

            Assert.That(() => context.Products.Find((long)1), Throws.InvalidOperationException);
        }

        [Test]
        public void Save_SaveAProductToTheRepository_ProductNameIsReturned()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                uut.ProductRepository.Insert(testProduct);
                uut.Save();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new UnitOfWork(context, _dalFacade);
                var result = uut.ProductRepository.GetById((long) 1);
                Assert.That(result.Name, Is.EqualTo("Kildevand"));
            }
        }
    }
}