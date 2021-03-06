﻿using System.Linq;
using CashRegister.Dal;
using CashRegister.Database;
using CashRegister.Models;
using NUnit.Framework;

namespace CashRegister.Test.Unit.Dal
{
    [TestFixture]
    public class RepositoryUnitTest
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
        public void Insert_ProductIsInserted_ProductCanBeFoundInDb()
        {
            var product = new Product("Øl", 20, true);

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                uut.Insert(product);
                context.SaveChanges();

                var result = context.Products.FirstOrDefault(p => p.Id == 1);
                Assert.That(result, Is.EqualTo(product));
            }
       }
        
        [Test]
        public void Delete_ProductIsDeleted_NoProductsInDatabase()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);

                uut.Delete(testProduct);
                context.SaveChanges();

                var result = context.Products.AsEnumerable();
                Assert.That(result, Is.Empty);
            }
        }
        
        [Test]
        public void Delete_ProductWithId1IsDeleted_NoProductsInDatabase()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                uut.Delete((long)1);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var result = context.Products.AsEnumerable();
                Assert.That(result, Is.Empty);
            }
        }
        
        [Test]
        public void GetById_ProductWithId1IsRequested_ProductIsReturnedFromDbPriceIsEqual()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.GetById((long)1);
                Assert.That(result.Price, Is.EqualTo(18));
            }
        }

        [Test]
        public void GetById_ProductWithId1IsRequested_ProductIsReturnedFromDbNameIsEqual()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.GetById((long)1);
                Assert.That(result.Name, Is.EqualTo("Kildevand"));
            }
        }

        [Test]
        public void GetById_ProductWithId1IsRequestedWithNoProductsInDb_NoProductIsReturnedFromDb()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.GetById((long)1);
                Assert.That(result, Is.EqualTo(null));
            }
        }

        [Test]
        public void Get_ProductsIsRequestedWhenNoProductsInDb_ProductPriceIsInCollectionFromDb()
        {
            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get();
                Assert.That(result, Is.Empty);
            }
        }

        [Test]
        public void Get_ProductsIsRequested_ProductPriceIsInCollectionFromDb()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get().ElementAt(0);
                Assert.That(result.Price, Is.EqualTo(18));
            }
        }

        [Test]
        public void Get_ProductsIsRequested_ProductNameIsInCollectionFromDb()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get().ElementAt(0);
                Assert.That(result.Name, Is.EqualTo("Kildevand"));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithWhereClause_ProductId2IsReturned()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct1);
                context.Products.Add(testProduct2);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get(x => x.Id == 2).ElementAt(0);
                Assert.That(result.Name, Is.EqualTo("Øl"));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithWhereClause_ProductId1IsReturned()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct1);
                context.Products.Add(testProduct2);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get(x => x.Saleable).ElementAt(0);
                Assert.That(result.Name, Is.EqualTo("Kildevand"));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithOrderByDescendingClause_ProductId1IsReturned()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct1);
                context.Products.Add(testProduct2);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get(null, q => q.OrderByDescending(x => x.Price)).ElementAt(0);
                Assert.That(result.Name, Is.EqualTo("Øl"));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithOrderByClause_ProductId1IsReturned()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct1);
                context.Products.Add(testProduct2);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);
                var result = uut.Get(null, q => q.OrderByDescending(x => x.Price)).ElementAt(0);
                Assert.That(result.Name, Is.EqualTo("Øl"));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithFilteredClauseWithoutLazyLoading_ProductCollectionContains2Products()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            var testProductGroup = new ProductGroup() { Name = "Drikkevarer" };
            testProductGroup.Products.Add(testProduct1);
            testProductGroup.Products.Add(testProduct2);

            using (var context = new CashRegisterContext())
            {
                context.ProductGroups.Add(testProductGroup);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var uut = new Repository<ProductGroup>(context);
                var filter = new[] { "Products" };
                var result = uut.Get(null, null, filter).ElementAt(0);
                Assert.That(result.Products.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithFilteredClauseWithoutLazyLoading_ProductCollectionContainsNoProductList()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            var testProductGroup = new ProductGroup() { Name = "Drikkevarer" };
            testProductGroup.Products.Add(testProduct1);
            testProductGroup.Products.Add(testProduct2);

            using (var context = new CashRegisterContext())
            {
                context.ProductGroups.Add(testProductGroup);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var uut = new Repository<ProductGroup>(context);
                var result = uut.Get().ElementAt(0);
                Assert.That(result.Products.Count, Is.EqualTo(0));
            }
        }

        [Test]
        public void Get_ProductIsRequestedWithEmptyFilteredClause_ArgumentExceptionIsThrown()
        {
            var testProduct1 = new Product("Kildevand", 18, true);
            var testProduct2 = new Product("Øl", 20, false);

            var testProductGroup = new ProductGroup() { Name = "Drikkevarer" };
            testProductGroup.Products.Add(testProduct1);
            testProductGroup.Products.Add(testProduct2);

            using (var context = new CashRegisterContext())
            {
                context.ProductGroups.Add(testProductGroup);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<ProductGroup>(context);
                var filter = new[] { "" };
                Assert.That(() => uut.Get(null, null, filter).ElementAt(0), Throws.ArgumentException);
            }
        }

        [Test]
        public void Update_ProductIsUpdated_UpdatedProductCanBeFoundInDb()
        {
            var testProduct = new Product("Kildevand", 18, true);

            using (var context = new CashRegisterContext())
            {
                context.Products.Add(testProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var uut = new Repository<Product>(context);

                var modifiedProduct = context.Products.FirstOrDefault(p => p.Id == 1);
                modifiedProduct.Price = 20;

                uut.Update(modifiedProduct);
                context.SaveChanges();
            }

            using (var context = new CashRegisterContext())
            {
                var modifiedProduct = context.Products.FirstOrDefault(p => p.Id == 1);

                var result = context.Products.FirstOrDefault(p => p.Id == 1);
                Assert.That(modifiedProduct.Price, Is.EqualTo(20));
            }
        }
    }
}