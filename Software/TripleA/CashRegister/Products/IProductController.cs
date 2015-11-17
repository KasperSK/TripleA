using System.Collections.Generic;
using CashRegister.Models;

namespace CashRegister.Products
{
    public interface IProductController
    {
        IReadOnlyCollection<ProductTab> ProductTabs { get; }
    }
}