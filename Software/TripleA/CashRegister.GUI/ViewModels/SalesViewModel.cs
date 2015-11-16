using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Ink;
using System.Windows.Input;
using CashRegister.Database;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {


        private ISalesController _salesctrl;

        public ObservableCollection<ViewProduct> TestProducts { get; } = new ObservableCollection<ViewProduct>();

        public List<Product> Products = new List<Product>();

        public SalesViewModel(ISalesController salesctrl = null)
        {

        }

        /*

            ICommand _refreshCurrentOrder;


            public Icommand RefreshCurrentOrder
    */







        public class ViewProduct
        {

            public string Navn { get; set; }

            public string Pris { get; set; }

            public string Antal { get; set; }

            public ViewProduct(string name, string price, string count)
            {
                Navn = name;

                Pris = price;

                Antal = count;
            }

            public void IncrementCount()
            {
                var count = Int32.Parse(Antal);

                count++;

                Antal = count.ToString();
            }





        }
    }
}
