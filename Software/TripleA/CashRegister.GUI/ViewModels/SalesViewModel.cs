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


        /*
         private ISalesController _salesctrl;

         public ObservableCollection<ViewProduct> ViewProducts { get; } = new ObservableCollection<ViewProduct>();

         public SalesViewModel(ISalesController salesctrl = null)
         {
             _salesctrl = salesctrl;



         }

         public void OnCurrentOrderChanged()
         {
             var currentOrder = _salesctrl.GetCurrentOrder();

             foreach (var lineElement in currentOrder.Lines)
             {
                 var price = (lineElement.UnitPrice*lineElement.Quantity).ToString();

                 ViewProducts.Add(new ViewProduct(lineElement.Product.Name, price, lineElement.Quantity.ToString()));
             }
         }





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





         }
         */
    }
}
