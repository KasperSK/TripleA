using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CashRegister.Database;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        /*

        private ISalesController _salesctrl;

        public ObservableCollection<TestProduct> TestProducts { get; } = new ObservableCollection<TestProduct>();

        public List<Product> Products = new List<Product>();

        public SalesViewModel(ISalesController salesctrl = null)
        {
            _salesctrl = salesctrl;

            var currentOrder = _salesctrl.GetCurrentOrder();


            foreach (var product in currentOrder.Products)
            {
                TestProducts.Add(new TestProduct(product.ProductName));
            }
            /*

          Products.Add(new Product() {ProductName = "TestNummer1", Prices = new List<Price>()});
            Products[0].Prices.Add(new Price() {Price1 = 50});
          Products.Add(new Product() {ProductName = "TestNummer2", Prices = new List<Price>()});
            Products[1].Prices.Add(new Price() { Price1 = 25 });

            foreach (var product in Products)
            {
                TestProducts.Add(new TestProduct(product.ProductName, "50","1"));
            }
            */

        }





    public class TestProduct
    {

        public string ProdName { get; set; }

        public string Price { get; set; }

        public string Count { get; set; }

        public TestProduct(string name, string price, string count)
        {
            ProdName = name;

            Price = Price;

            Count = count;
        }



        

    }
}
