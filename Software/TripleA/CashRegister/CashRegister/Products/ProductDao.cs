using System.Collections.ObjectModel;
using System.Linq;
using CashRegister.DAL;
using CashRegister.Models;

namespace CashRegister.Products
{
    /// <summary>
    ///     Implementation og IProductDao
    /// </summary>
    public class ProductDao : IProductDao
    {
        private readonly IDalFacade _dalFacade;

        public ProductDao(IDalFacade databaseLayerFacade)
        {
            _dalFacade = databaseLayerFacade;
        }

        // Still have error when product is not saleable
        public ReadOnlyCollection<ProductTab> GetProductTabs(bool onlyActive)
        {
            using (var uow = _dalFacade.GetUnitOfWork())
            {
                return
                    new ReadOnlyCollection<ProductTab>(
                        uow.ProductTabRepository.Get(
                            p => p.Active && p.ProductTypes.Any(s => s.ProductGroups.Any(q => q.Products.Any(r => r.Saleable))),
                            includeProperties: new[] {"ProductTypes", "ProductTypes.ProductGroups", "ProductTypes.ProductGroups.Products" }).ToList());
            }
        }

        /*
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
        */
    }
}