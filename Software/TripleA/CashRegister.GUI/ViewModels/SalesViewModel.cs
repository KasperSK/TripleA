using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Ink;
using System.Windows.Input;
using CashRegister.Database;
using CashRegister.DAL;
using CashRegister.Models;
using CashRegister.Orders;
using CashRegister.Printer;
using CashRegister.Receipts;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {


        
         private ISalesController _salesctrl;

         public ObservableCollection<ViewProduct> ViewProducts { get; } = new ObservableCollection<ViewProduct>();  //Collection the salesView will bind to

        public SalesViewModel()
        {
            _salesctrl = null;
        }

         public SalesViewModel(ISalesController salesCtrl)
         {
             _salesctrl = salesCtrl;
         }

        public void SetSalesController(ISalesController salesController)
        {
            _salesctrl = salesController;
        }

         public void OnCurrentOrderChanged()                                        //Happening when receiving event from SalesController
         {
             var currentOrder = _salesctrl.GetCurrentOrder();                       //Retrieving currentorder via SAlesController

             foreach (var lineElement in currentOrder.Lines)                        //Itterating through all orderlines in currentorder
             {
                 var price = (lineElement.UnitPrice*lineElement.Quantity).ToString(); //Making total price for Orderline

                 ViewProducts
                    .Add(new ViewProduct(lineElement.Quantity.ToString()
                    ,                  //Adding new Viewproducts to be displayed in SalesView
                   lineElement.Product.Name,
                     price));
             }
         }





         public class ViewProduct
         {

             public string Antal { get; set; }

             public string Navn { get; set; }

             public string Pris { get; set; }

             public ViewProduct(string count, string name, string price)
             {

                Antal = count;

                Navn = name;

                Pris = price;

            }





         }
         
    }
}
