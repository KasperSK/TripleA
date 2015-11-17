using System.Collections.Generic;
using System.Linq;
using CashRegister.Models;

namespace CashRegister.Products
{
    public class ProductController : IProductController
    {
        private readonly IProductDao _dao;

        private IReadOnlyCollection<ProductTab> _productTabs;

        public IReadOnlyCollection<ProductTab> ProductTabs => _productTabs;

        public ProductController(IProductDao dao)
        {
            _dao = dao;
            RefreshProductTabs();
        }

        private void RefreshProductTabs()
        {
            _productTabs = _dao.GetProductTabs(onlyActive:true);
        }
    }
}