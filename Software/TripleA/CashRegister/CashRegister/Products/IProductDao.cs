using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Products
{
    public interface IProductDao
    {
        List<ProductTab> GetProductTabs(bool onlyActive);
    }
}