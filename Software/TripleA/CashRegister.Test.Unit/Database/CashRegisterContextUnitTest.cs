using System.Data.Entity;
using CashRegister.Database;
using CashRegister.Models;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Database
{ 
    [TestFixture]
    public class CashRegisterContextUnitTest
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
        public void Discounts_DiscountsIsCalled_IsTypeOfDbSetDiscount()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.Discounts, Is.TypeOf<DbSet<Discount>>());
            }
        }

        [Test]
        public void Discounts_DiscountsIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.Discounts, Is.Empty);
            }
        }

        [Test]
        public void SalesOrders_SalesOrdersIsCalled_IsTypeOfDbSetSalesOrder()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.SalesOrders, Is.TypeOf<DbSet<SalesOrder>>());
            }
        }

        [Test]
        public void SalesOrders_SalesOrdersIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.SalesOrders, Is.Empty);
            }
        }

        [Test]
        public void Orderlines_OrderlinesIsCalled_IsTypeOfDbSetOrderlines()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.OrderLines, Is.TypeOf<DbSet<OrderLine>>());
            }
        }

        [Test]
        public void Orderlines_OrderlinesIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.OrderLines, Is.Empty);
            }
        }

        [Test]
        public void Products_ProductsIsCalled_IsTypeOfDbSetProducts()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.Products, Is.TypeOf<DbSet<Product>>());
            }
        }

        [Test]
        public void Products_ProductsIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.Products, Is.Empty);
            }
        }

        [Test]
        public void ProductGroups_ProductGroupsIsCalled_IsTypeOfDbSetProductGroup()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.ProductGroups, Is.TypeOf<DbSet<ProductGroup>>());
            }
        }

        [Test]
        public void ProductGroups_ProductGroupsIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.ProductGroups, Is.Empty);
            }
        }

        [Test]
        public void ProductTypes_ProductTypesIsCalled_IsTypeOfDbSetProductType()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.ProductTypes, Is.TypeOf<DbSet<ProductType>>());
            }
        }

        [Test]
        public void ProductTypes_ProductTypesIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.ProductTypes, Is.Empty);
            }
        }

        [Test]
        public void Transactions_TransactionsIsCalled_IsTypeOfDbSetTransaction()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.Transactions, Is.TypeOf<DbSet<Transaction>>());
            }
        }

        [Test]
        public void Transactions_TransactionsIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.Transactions, Is.Empty);
            }
        }

        [Test]
        public void ProductTabs_ProductTabsIsCalled_IsTypeOfDbSetProductTabs()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.ProductTabs, Is.TypeOf<DbSet<ProductTab>>());
            }
        }

        [Test]
        public void ProductTabs_ProductTabsIsCalled_RepositoryIsEmpty()
        {
            using (var uut = new CashRegisterContext())
            {
                Assert.That(uut.ProductTabs, Is.Empty);
            }
        }
    }
}