using System.Collections.Generic;
using System.Linq;
using CashRegister.Models;

namespace CashRegister.Products
{
    public class ProductController : IProductController
    {
		/// <summary>
        /// Our ProductDao
        /// </summary>
        private readonly IProductDao _dao;

		/// <summary>
        /// Collection of the ProductTabs that are active and have Products that are saleable
        /// </summary>
        private IReadOnlyCollection<ProductTab> _productTabs;

		/// <summary>
        /// Collection of the ProductTabs that are active and have Products that are saleable
		/// Hides _productTabs
        /// </summary>
        public IReadOnlyCollection<ProductTab> ProductTabs => _productTabs;

		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dao">Use the ProductController with a specific ProductDao</param>
        /// <returns>ProductController</returns>
        public ProductController(IProductDao dao)
        {
            _dao = dao;
            RefreshProductTabs();
        }

		/// <summary>
        /// Refresh the ProductTabs, so we don't call the database each time.
        /// </summary>
        private void RefreshProductTabs()
        {
            _productTabs = _dao.GetProductTabs(onlyActive:true);
        }
    }
}