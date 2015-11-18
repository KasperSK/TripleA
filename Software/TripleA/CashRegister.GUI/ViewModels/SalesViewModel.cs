using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly ISalesController _salesController;

        public SalesViewModel(ISalesController salesController)
        {
            total = 0;
            _salesController = salesController;
            _salesController.PropertyChanged += OnCurrentOrderChanged;
        }

        public ObservableCollection<ViewProduct> ViewProducts { get; } = new ObservableCollection<ViewProduct>();
        //Collection the salesView will bind to

        public long total { get; set; }

        public void OnCurrentOrderChanged(object sender, PropertyChangedEventArgs e) //Happening when receiving event from SalesController
        {
            var currentOrder = _salesController.CurrentOrder; //Retrieving currentorder via SAlesController

            ViewProducts.Clear();

            foreach (var lineElement in currentOrder.Lines) //Itterating through all orderlines in currentorder
            {
                var price = (lineElement.UnitPrice*lineElement.Quantity).ToString(); //Making total price for Orderline

                ViewProducts
                    .Add(new ViewProduct(lineElement.Quantity.ToString()
                        , //Adding new Viewproducts to be displayed in SalesView
                        lineElement.Product.Name,
                        price));
                total += lineElement.UnitPrice * lineElement.Quantity;
                OnPropertyChanged();
            }
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
        private RelayCommand _paymentCommand;
        public ICommand PaymentCommand => _paymentCommand ?? (_paymentCommand = new RelayCommand(PaymentCommandExecute, PaymentCommandCanExecute));

        private void PaymentCommandExecute()
        {
            var amount = _salesController.MissingPaymentOnOrder();
            _salesController.StartPayment((int)amount, "Cash", PaymentType.Cash);
        }

        private bool PaymentCommandCanExecute()
        {
            return ViewProducts.Count > 0;
        }

        private RelayCommand _abortCommand;
        public ICommand AbortCommand => _abortCommand ?? (_abortCommand = new RelayCommand(AbortCommandExecute));

        private void AbortCommandExecute()
        {
            _salesController.ClearOrder();
            ViewProducts.Clear();
        }
    }
}