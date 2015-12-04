using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CashRegister.GUI.Dialogs;
using CashRegister.Models;
using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    /// <summary>
    /// The ViewModel for the SalesView.
    /// </summary>
    public class SalesViewModel : BaseViewModel
    {
        /// <summary>
        /// An implementation of ISalesController.
        /// </summary>
        private readonly ISalesController _salesController;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="salesController">An implementation of ISalesController.</param>
        public SalesViewModel(ISalesController salesController)
        {
            _salesController = salesController;
            _salesController.PropertyChanged += OnCurrentOrderChanged;
        }

        /// <summary>
        /// Contains a collection of ViewProducts.
        /// </summary>
        public ObservableCollection<ViewProduct> ViewProducts { get; } = new ObservableCollection<ViewProduct>();
        
        /// <summary>
        /// Contains the total price of the current order.
        /// </summary>
        public string Total => _salesController.CurrentOrder.Total + " kr.";

        /// <summary>
        /// Handles events from the SalesController.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments sent with the event.</param>
        public void OnCurrentOrderChanged(object sender, PropertyChangedEventArgs e)
        {
            var currentOrderLines = _salesController.CurrentOrderLines;

            ViewProducts.Clear();           

            foreach (var lineElement in currentOrderLines)
            {
                var price = (lineElement.UnitPrice*lineElement.Quantity).ToString();

                ViewProducts.Add(new ViewProduct(lineElement.Quantity.ToString(), lineElement.Product.Name, price));
                
                OnPropertyChanged();
            }

            OnPropertyChanged(nameof(ViewProducts));
            OnPropertyChanged(nameof(Total));
        }

        /// <summary>
        /// Returns the Tally.
        /// </summary>
        /// <returns>The tally</returns>
        public string GetTally()
        {
            return _salesController.Tally();
        }

        /// <summary>
        /// Helper class for showing products on the display.
        /// </summary>
        public class ViewProduct
        { 
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="count">The quantity of the Product</param>
            /// <param name="name">The name of the Product</param>
            /// <param name="price">The price of the Product</param>
            public ViewProduct(string count, string name, string price)
            {
                Antal = count;
                Navn = name;
                Pris = price +" kr.";
            }

            /// <summary>
            /// Contains the quantity of the Product.
            /// </summary>
            public string Antal { get; set; }

            /// <summary>
            /// Contains the name of the Product.
            /// </summary>
            public string Navn { get; set; }

            /// <summary>
            /// Contains the price of the Product.
            /// </summary>
            public string Pris { get; set; }
        }
        
        /// <summary>
        /// Contains an ICommand implementation for PaytypeCommand.
        /// </summary>
        private ICommand _paytypeCommand;

        /// <summary>
        /// Contains an ICommand property that returns an implementation to Invoke a PaytypeCommand.
        /// </summary>
        public ICommand PaytypeCommand => _paytypeCommand ?? (_paytypeCommand = new RelayCommand<PaymentType>(PaytypeCommandExecute));

        /// <summary>
        /// The logic for PaytypeCommand.
        /// </summary>
        /// <param name="paymentType"></param>
        private void PaytypeCommandExecute(PaymentType paymentType)
        {
            var amount = _salesController.MissingPaymentOnOrder();
            _salesController.StartPayment((int)amount, "", paymentType);
        }

        /// <summary>
        /// Contains an ICommand implementation for PaymentCommand.
        /// </summary>
        private ICommand _payCommand;

        /// <summary>
        /// Contains an ICommand property that returns an implementation to Invoke a PaymentCommand.
        /// </summary>
        public ICommand PayCommand
        {
            get { return _payCommand ?? (_payCommand = new RelayCommand<Window>(PayCommand_Execute)); }
        }

        /// <summary>
        /// The logic for an PaymentCommand_Exectute.
        /// </summary>
        private void PayCommand_Execute(Window parrentWindow)
        {
            var dlg = new PaymentDialog();
            dlg.Owner = parrentWindow;

            if (dlg.ShowDialog() == true)
            {
                PaytypeCommandExecute(dlg.PaymentChoice);
            }
        }

        /// <summary>
        /// A predicate for an execution of PaymentCommandExecute.
        /// </summary>
        /// <returns>False if ViewProducts.Count is less than 0.</returns>
        private bool PaymentCommandCanExecute()
        {
            return ViewProducts.Count > 0;
        }

        /// <summary>
        /// Contains an ICommand implementation for AbortCommand.
        /// </summary>
        private ICommand _abortCommand;

        /// <summary>
        /// Contains an ICommand property that returns an implemtation to Invoke a AbortCommand.
        /// </summary>
        public ICommand AbortCommand => _abortCommand ?? (_abortCommand = new RelayCommand(AbortCommandExecute));

        /// <summary>
        /// The logic for an AbortCommand.
        /// </summary>
        private void AbortCommandExecute()
        {
            ViewProducts.Clear();
            _salesController.CancelOrder();
            OnPropertyChanged(nameof(Total));
        }

        /// <summary>
        /// Contains an ICommand implementation for BalanceCommand.
        /// </summary>
        private ICommand _balanceCommand;

        /// <summary>
        /// Contains an ICommand property that returns an implemantion to Invoke a BalanceCommand.
        /// </summary>
        public ICommand BalanceCommand
        {
            get { return _balanceCommand ?? (_balanceCommand = new RelayCommand<Window>(BalanceCommand_Execute)); }
        }

        /// <summary>
        /// The logic for an BalanceCommand.
        /// </summary>
        /// <param name="parrentWindow">The parentWindow.</param>
        private void BalanceCommand_Execute(Window parrentWindow)
        {
            var balance = _salesController.Tally();

            var balanceDlg = new BalanceDialog(balance);

            balanceDlg.Owner = parrentWindow;

            if (balanceDlg.ShowDialog() == true)
            {
                //var balanceReceipt = new Receipt();

                //balanceReceipt.Add(balance);

                //_salesController.Print(balanceReceipt);
            };
        }
    }
}