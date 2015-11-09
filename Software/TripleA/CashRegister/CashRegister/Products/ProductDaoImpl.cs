using System.Linq;

namespace CashRegister.Products
{
    using DAL;
    using Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implementation og IProductDao
    /// </summary>
    public class ProductDaoImpl : IProductDao
    {
        private readonly IDalFacade _dalFacade;

        public ProductDaoImpl(IDalFacade dalfacede)
        {
            _dalFacade = dalfacede;
        }

        /// <param name="product">Product to be inserted into the database</param>
        public virtual long Insert(Product product)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.ProductRepository.Insert(product);
            }
            return product.Id;
        }

        public virtual Product SelectById(int id)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.ProductRepository.GetById(id);
            }
        }

        public virtual void Delete(Product product)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.ProductRepository.Delete(product);
            }
        }

        public virtual void Update(Product product)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                uow.ProductRepository.Update(product);
            }
        }

        public virtual ProductGroup SelectByGroupId(int id)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.ProductGroupRepository.GetById(id);
            }
        }

        public List<ProductTab> GetProductTabs(bool onlyActive)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return uow.ProductTabRepository.Get(t => t.Active == onlyActive).OrderBy(t => t.Priority).ToList();
            }
        }
    }
}

