using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using CashRegister.Database;
using CashRegister.Log;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        private string _backGroundColour;

        // RelayCommads
        private RelayCommand<int> _changeTab;
        private ILogger _logger = LogFactory.GetLogger(typeof (TabViewModel));

        //Construct View
        public TabViewModel()
        {
            IDatabaseInitializer<CashRegisterContext> seed;

            // Empty
            seed = null;

            // Kalle Seed
            // seed = new CashProductInitializer();

            // Lærke Seed
            seed = new FullProductInitializer();

            using (var contex = new CashRegisterContext(seed))
            {
                contex.Database.Initialize(true);
            }


            //TabHead.Add(new TabHeader() { Name = "Standart", Com = ChangeTab });
            //TabHead.Add(new TabHeader() { Name = "Luksus", Com = ChangeTab });
            //TabHead.Add(new TabHeader() { Name = "Billig", Com = ChangeTab });

            //TabDictionary.Add("Standart", new List<TabItem>() { new TabItem() { Name = "Tuborg", Colour = "Red", Row = 0, Column = 0 }, new TabItem() { Name = "Carlsberg", Colour = "Red", Row = 0, Column = 1 }, new TabItem() { Name = "Carlsberg", Colour = "Blue", Row = 0, Column = 2 } });
            //TabDictionary.Add("Luksus", new List<TabItem>() { new TabItem() { Name = "Hat", Colour = "Red", Row = 0, Column = 0 }, new TabItem() { Name = "Ceres", Colour = "Purple", Row = 3, Column = 4 } });
            //TabDictionary.Add("Billig", new List<TabItem>() { new TabItem() { Name = "Star", Colour = "Green", Row = 1, Column = 0 }, new TabItem() { Name = "Krudtugle", Colour = "Red", Row = 2, Column = 0 } });
            //TabDictionary["Standart"].ForEach(n => TabItems.Add(n));
            FetchView();
            BackGroundColour = "Green";
        }

        // View Ressourses 
        public Dictionary<int, List<TabItem>> TabDictionary { get; set; } = new Dictionary<int, List<TabItem>>();
        public ObservableCollection<TabItem> TabItems { get; set; } = new ObservableCollection<TabItem>();
        public ObservableCollection<TabHeader> TabHead { get; set; } = new ObservableCollection<TabHeader>();

        public string BackGroundColour
        {
            get { return _backGroundColour; }
            set
            {
                _backGroundColour = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeTab => _changeTab ?? (_changeTab = new RelayCommand<int>(ChangeTabCommand));

        private void ChangeTabCommand(int buttonid)
        {
            TabItems.Clear();
            TabDictionary[buttonid].ForEach(n => TabItems.Add(n));
            BackGroundColour = TabHead.First(th => th.Id == buttonid).Colour;
        }

        //Populate Tabs
        private void FetchView()
        {
            var first = -1;
            var foundFirst = false;

            //var product = new ProductController(new ProductDao(new DalFacade()));
            var salesController = SalesFactory.GuiSalesController;

            foreach (var tab in salesController.ProductTabs)
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
                        foreach (var products in productGroup.Products)
                        {
                            tabItem.Add(new TabItem(products.Name, i, j++, productType.Color));
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
                TabDictionary[first].ForEach(n => TabItems.Add(n));
        }
    }

    public class TabHeader
    {
        public TabHeader(int id, string name = null, ICommand command = null, string colour = "blue")
        {
            Name = name;
            Com = command;
            Colour = colour;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public ICommand Com { get; set; }
    }

    public class TabItem
    {
        public TabItem(string name = null, int row = 0, int column = 0, string colour = "pink")
        {
            Name = name;
            Row = row;
            Column = column;
            Colour = colour;
        }

        public string Name { get; set; }
        public string Colour { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}