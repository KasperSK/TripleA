using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    /// <summary>
    /// The ViewModel for the TabView
    /// </summary>
    public class TabViewModel : BaseViewModel
    {
        /// <summary>
        /// An implementation of ISalesController.
        /// </summary>
        private readonly ISalesController _salesController;

        /// <summary>
        /// An implementation of INumpad.
        /// </summary>
        private readonly INumpad _numpad;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="salesController">An implementation of ISalesController.</param>
        /// <param name="numpad">An implementation of INumpad.</param>
        public TabViewModel(ISalesController salesController, INumpad numpad)
        {
            _salesController = salesController;
            _numpad = numpad;
            FetchView();
        }

        /// <summary>
        /// Contains a Dictionary for the TabItems.
        /// </summary>
        public Dictionary<int, List<TabItem>> TabDictionary { get; } = new Dictionary<int, List<TabItem>>();

        /// <summary>
        /// Contains the ObservableCollection of the TabItem's inside the TabHead.
        /// </summary>
        public ObservableCollection<TabItem> TabItems { get; } = new ObservableCollection<TabItem>();

        /// <summary>
        /// Contains the ObservableCollection of TabHeader's.
        /// </summary>
        public ObservableCollection<TabHeader> TabHead { get; } = new ObservableCollection<TabHeader>();

        /// <summary>
        /// A string to hold the background colour of the buttons.
        /// </summary>
        private string _backGroundColour;

        /// <summary>
        /// A property the notifies if a background colour has changed.
        /// </summary>
        public string BackGroundColour
        {
            get { return _backGroundColour; }
            set
            {
                _backGroundColour = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// An ICommand implementation of ChangeTab.
        /// </summary>
        private ICommand _changeTab;

        /// <summary>
        /// Contains an ICommand to Invoke a ChangeTab.
        /// </summary>
        public ICommand ChangeTab => _changeTab ?? (_changeTab = new RelayCommand<int>(ChangeTabCommand));

        /// <summary>
        /// The logic for ChangeTab.
        /// </summary>
        /// <param name="buttonid">The id for the dictionary.</param>
        private void ChangeTabCommand(int buttonid)
        {
            TabItems.Clear();
            TabDictionary[buttonid].ForEach(n => TabItems.Add(n));
            BackGroundColour = TabHead.First(th => th.Id == buttonid).Colour;
        }

        /// <summary>
        /// An ICommand implementation of AddProduct
        /// </summary>
        private ICommand _addProduct;

        /// <summary>
        /// Contains an ICommand to Invoke a AddProduct.
        /// </summary>
        public ICommand AddProduct => _addProduct ?? (_addProduct = new RelayCommand<Product>(AddProductCommand));

        /// <summary>
        /// The logic for AddProduct.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        private void AddProductCommand(Product product)
        {
            _salesController.AddProductToOrder(product, _numpad.Amount, new Discount {Description = "No DISCOUNT FOR YOU", Percent = 0});
            _numpad.ClearNumpad();
        }

        /// <summary>
        /// Fetches ProductTabs.
        /// </summary>
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

    /// <summary>
    /// A helper class for TabHeader's
    /// </summary>
    public class TabHeader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id of the TabHeader.</param>
        /// <param name="name">The name of the TabHeader.</param>
        /// <param name="command">An ICommand implementation.</param>
        /// <param name="colour">The colour of the TabHeader.</param>
        public TabHeader(int id, string name, ICommand command, string colour)
        {
            Name = name;
            Command = command;
            Colour = colour;
            Id = id;
        }

        /// <summary>
        /// Contains the id of the TabHeader.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Contains the name of the TabHeader.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contains the colour of the TabHeader.
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        /// Contains an implementation of ICommand.
        /// </summary>
        public ICommand Command { get; set; }
    }

    /// <summary>
    /// A helper class for TabItem's
    /// </summary>
    public class TabItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="row">The row location of the TabItem.</param>
        /// <param name="column">The column location of the TabItem.</param>
        /// <param name="productType">The ProductType of the TabItem.</param>
        /// <param name="command">An ICommand implementation.</param>
        /// <param name="product">The Product that the TabItem references.</param>
        public TabItem(int row, int column, ProductType productType, ICommand command, Product product)
        {
            Row = row;
            Column = column;
            ProductType = productType;
            Command = command;
            Product = product;
        }

        /// <summary>
        /// Contains the Product of the TabItem.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Contains the ProductType of the TabItem.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Contains the name of the Product.
        /// </summary>
        public string Name => Product.Name;

        /// <summary>
        /// Contains the colour of the ProductType.
        /// </summary>
        public string Colour => ProductType.Color;

        /// <summary>
        /// Contains the row location of the TabItem.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Contains the column location of the TabItem.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Contains a ICommand implementation
        /// </summary>
        public ICommand Command { get; set; }
    }
}