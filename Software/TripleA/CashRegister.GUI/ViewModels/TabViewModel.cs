using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CashRegister.Database;
using CashRegister.Log;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        private string _backGroundColour;

        private readonly ISalesController _salesController;
        private readonly INumpad _numpad;

        //Construct View
        public TabViewModel(ISalesController salesController, INumpad numpad)
        {
            _salesController = salesController;
            _numpad = numpad;
            FetchView();
        }

        // View Ressourses 
        public Dictionary<int, List<TabItem>> TabDictionary { get; } = new Dictionary<int, List<TabItem>>();
        public ObservableCollection<TabItem> TabItems { get; } = new ObservableCollection<TabItem>();
        public ObservableCollection<TabHeader> TabHead { get; } = new ObservableCollection<TabHeader>();

        //Fokused tab colour
        public string BackGroundColour
        {
            get { return _backGroundColour; }
            set
            {
                _backGroundColour = value;
                OnPropertyChanged();
            }
        }

        // RelayCommads
        private RelayCommand<int> _changeTab;
        public ICommand ChangeTab => _changeTab ?? (_changeTab = new RelayCommand<int>(ChangeTabCommand));
        private void ChangeTabCommand(int buttonid)
        {
            TabItems.Clear();
            TabDictionary[buttonid].ForEach(n => TabItems.Add(n));
            BackGroundColour = TabHead.First(th => th.Id == buttonid).Colour;
        }

        private RelayCommand<Product> _addProductCommand;
        public ICommand AddProduct => _addProductCommand ?? (_addProductCommand = new RelayCommand<Product>(AddProductCommand));
        private void AddProductCommand(Product product)
        {
                _salesController.AddProductToOrder(product, _numpad.Amount,
                    new Discount {Description = "No DISCOUNT FOR YOU", Percent = 0});
                _numpad.ClearNumpad();
        }

        //Populate Tabs
        public void FetchView()
        {
            var first = -1;
            var foundFirst = false;

            //var product = new ProductController(new ProductDao(new DalFacade()));

            foreach (var tab in _salesController.ProductTabs.OrderBy(p => p.Priority))
            {
                TabHead.Add(new TabHeader(tab.Id, tab.Name, ChangeTab, tab.Color));
                if (!foundFirst)
                {
                    first = tab.Id;
                    foundFirst = true;
                }
                var tabItem = new List<TabItem>();
                var i = 0;
                var j = 0;
                foreach (var productType in tab.ProductTypes)
                {
                    foreach (var productGroup in productType.ProductGroups)
                    {
                        foreach (var product in productGroup.Products)
                        {
                            tabItem.Add(new TabItem(i, j++, productType, AddProduct, product));
                            if (j >= 5)
                            {
                                i++;
                                j = 0;
                            }
                        }
                    }
                }
                TabDictionary.Add(tab.Id, tabItem);
            }
            if (first != -1)
            {
                ChangeTabCommand(first);
            }
        }
    }

    public class TabHeader
    {
        public TabHeader(int id, string name, ICommand command, string colour)
        {
            Name = name;
            Command = command;
            Colour = colour;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public ICommand Command { get; set; }
    }

    public class TabItem
    {
        public TabItem(int row, int column, ProductType productType, ICommand command, Product product)
        {

            Row = row;
            Column = column;
            ProductType = productType;
            Command = command;
            Product = product;
        }

        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
        public string Name => Product.Name;
        public string Colour => ProductType.Color;
        public int Row { get; set; }
        public int Column { get; set; }
        public ICommand Command { get; set; }
    }
}