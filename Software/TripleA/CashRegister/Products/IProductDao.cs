using System.Collections.Generic;
using System.Collections.ObjectModel;
using CashRegister.Models;

namespace CashRegister.Products
{
    public interface IProductDao
    {
        ReadOnlyCollection<ProductTab> GetProductTabs(bool onlyActive);
    }
}