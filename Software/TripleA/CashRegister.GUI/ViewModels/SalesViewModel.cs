using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CashRegister.Database;
using MvvmFoundation.Wpf;

namespace CashRegister.GUI.ViewModels
{
    class SalesViewModel
    {

        public SalesViewModel()
        {
            _products = new ObservableCollection<Product>();

            _products.Add(new Product() {ProductName = "Digs"});
        }
        private ObservableCollection<Product> _products;

        public IEnumerable<Product> Products => _products;

        private ICommand _addProduct;

        public ICommand AddProduct
        {
            get { return _addProduct ?? (_addProduct = new RelayCommand(AddProduct_Command, AddCanExecute)); }
        }



        public void AddProduct_Command()
        {
            Product _Product = new Product() {ProductName = "TestProduct"};

            _products.Add(_Product);
        }

        public bool AddCanExecute()
        {
            return true;
        }

    }
}
