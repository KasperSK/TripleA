using System.Collections.ObjectModel;
using System.Linq;
using CashRegister.Dal;
using CashRegister.Models;

namespace CashRegister.Products
{
    /// <summary>
    ///     Implementation of IProductDao
    /// </summary>
    public class ProductDao : IProductDao
    {
		/// <summary>
		///     Our DalFacade, how we connect and query the database.
		/// </summary>
        private readonly IDalFacade _dalFacade;

		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseLayerFacade">Use the ProductDao with a specific DalFacade</param>
        /// <returns>ProductDao</returns>
        public ProductDao(IDalFacade databaseLayerFacade)
        {
            _dalFacade = databaseLayerFacade;
        }

		/// <summary>
        /// Collection of the ProductTabs that are active and have Products that are saleable
		/// Still have error when product is not saleable
		/// <param name="onlyActive">Only return Active Tabs and Saleable Products</param>
		/// <returns>Collection of the ProductTabs</returns>
        /// </summary>
        public ReadOnlyCollection<ProductTab> GetProductTabs(bool onlyActive)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                return
                    new ReadOnlyCollection<ProductTab>(
                        uow.ProductTabRepository.Get(
                            p =>
                                p.Active &&
                                p.ProductTypes.Any(s => s.ProductGroups.Any(q => q.Products.Any(r => r.Saleable))),
                            includeProperties:
                                new[]
                                {"ProductTypes", "ProductTypes.ProductGroups", "ProductTypes.ProductGroups.Products"})
                            .ToList());
            }
        }

        /*
        /// <param name="product">Product to be inserted into the database</param>
        public virtual long Insert(Product product)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.ProductRepository.Insert(product);
            }
            return product.Id;
        }

        public virtual Product SelectById(int id)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                return uow.ProductRepository.GetById(id);
            }
        }

        public virtual void Delete(Product product)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.ProductRepository.Delete(product);
            }
        }

        public virtual void Update(Product product)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                uow.ProductRepository.Update(product);
            }
        }

        public virtual ProductGroup SelectByGroupId(int id)
        {
            using (var uow = _dalFacade.UnitOfWork)
            {
                return uow.ProductGroupRepository.GetById(id);
            }
        }
        */
    }
}