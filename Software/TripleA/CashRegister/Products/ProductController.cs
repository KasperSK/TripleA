using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Products
{
    /// <summary>
    /// Implementation of IProductController.
    /// This controller is used to retrive products for the CashRegister.
    /// </summary>
    public class ProductController : IProductController
    {
        /// <summary>
        /// An interface to the a Product Data Access Implementation.
        /// </summary>
        private readonly IProductDao _dao;

        /// <summary>
        /// Collection of the ProductTabs that are active and have Products that are saleable.
        /// </summary>
        public IReadOnlyCollection<ProductTab> ProductTabs { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dao">An IProductDao implementation.</param>
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
            ProductTabs = _dao.GetProductTabs(onlyActive:true);
        }
    }
}