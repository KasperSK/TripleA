using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CashRegister.GUI.Dialogs;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly ISalesController _salesController;

        public SalesViewModel(ISalesController salesController)
        {
            _salesController = salesController;
            _salesController.PropertyChanged += OnCurrentOrderChanged;
        }

        public ObservableCollection<ViewProduct> ViewProducts { get; } = new ObservableCollection<ViewProduct>();
        //Collection the salesView will bind to

        public long Total => _salesController.CurrentOrderTotal;

        public void OnCurrentOrderChanged(object sender, PropertyChangedEventArgs e) //Happening when receiving event from SalesController
        {
            var currentOrderLines = _salesController.CurrentOrderLines; //Retrieving currentorder via SAlesController

            ViewProducts.Clear();

            foreach (var lineElement in currentOrderLines) //Itterating through all orderlines in currentorder
            {
                var price = (lineElement.UnitPrice*lineElement.Quantity).ToString(); //Making Total price for Orderline

                ViewProducts
                    .Add(new ViewProduct(lineElement.Quantity.ToString(), //Adding new Viewproducts to be displayed in SalesView
                        lineElement.Product.Name,
                        price));
                
                
            }
            OnPropertyChanged(nameof(ViewProducts));
            OnPropertyChanged(nameof(Total));

        }

        public class ViewProduct
        {
            public ViewProduct(string count, string name, string price)
            {
                Antal = count;

                Navn = name;

                Pris = price;
            }

            public string Antal { get; set; }

            public string Navn { get; set; }

            public string Pris { get; set; }
        }

        //RelayCommands
        private RelayCommand<PaymentType> _paytypeCommand;
        public ICommand PaytypeCommand => _paytypeCommand ?? (_paytypeCommand = new RelayCommand<PaymentType>(PaytypeCommandExecute));

        private void PaytypeCommandExecute(PaymentType paymentType)
        {
            var amount = _salesController.MissingPaymentOnOrder();
            _salesController.StartPayment((int)amount, "", paymentType);
        }

        private RelayCommand _paymentCommand;
        public ICommand PaymentCommand => _paymentCommand ?? (_paymentCommand = new RelayCommand(PaymentCommandExecute, PaymentCommandCanExecute));

        private void PaymentCommandExecute()
        {
        }

        private bool PaymentCommandCanExecute()
        {
            return ViewProducts.Count > 0;
        }

        private RelayCommand _abortCommand;
        public ICommand AbortCommand => _abortCommand ?? (_abortCommand = new RelayCommand(AbortCommandExecute));

        private void AbortCommandExecute()
        {
            _salesController.CancelOrder();
        }
    }
}