using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CashRegister.Database;
using CashRegister.DAL;
using CashRegister.GUI.Annotations;
using CashRegister.Products;

namespace CashRegister.GUI.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        // View Ressourses 
        public Dictionary<string, List<TabItem>> TabDictionary { get; set; } = new Dictionary<string, List<TabItem>>();
        public ObservableCollection<TabItem> TabItems { get; set; } = new ObservableCollection<TabItem>();
        public ObservableCollection<TabHeader> TabHead { get; set; } = new ObservableCollection<TabHeader>();
        private string _backGroundColour;
        public string BackGroundColour {
            get { return _backGroundColour; }
            set { _backGroundColour = value;
                OnPropertyChanged();
            } }

        //Construct View
        public TabViewModel()
        {
            IDatabaseInitializer<CashRegisterContext> seed;

            // Empty
            seed = null;

            // Kalle Seed
            seed = new CashProductInitializer();

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

        // RelayCommads
        private RelayCommand<string> _changeTab;
        public ICommand ChangeTab => _changeTab ?? (_changeTab = new RelayCommand<string>(ChangeTabCommand));

        private void ChangeTabCommand(string button)
        {

            TabItems.Clear();
            TabDictionary[button].ForEach(n => TabItems.Add(n));
            BackGroundColour = TabHead.First(th => th.Name == button).Colour;
        }

        //Populate Tabs
        private void FetchView()
        {
            string first = null;
            bool foundFirst = false;
            var product = new ProductController(new ProductDao(new DalFacade()));
            foreach (var tab in product.ProductTabs)
            {
                TabHead.Add(new TabHeader(tab.Name, ChangeTab));
                if (!foundFirst)
                {
                    first = tab.Name;
                    foundFirst = true;
                }
                foreach (var tabPG in tab.ProductGroups)
                {
                    var tabItem = new List<TabItem>();
                    var i = 0;
                    var j = 0;
                    foreach (var products in tabPG.Products)
                    {
                        tabItem.Add(new TabItem(products.Name, i, j++));
                        if (j >= 4) i++;
                    }
                    TabDictionary.Add(tab.Name, tabItem);      
                }
            }
            if(first != null)
                TabDictionary[first].ForEach(n => TabItems.Add(n));
        }
    }

    public class TabHeader
    {
        public TabHeader(string name = null, ICommand command = null, string colour = "blue")
        {
            Name = name;
            Com = command;
            Colour = colour;
        }
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
