using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    /// <summary>
    /// A ViewModel for the MainWindow.
    /// </summary>
    public class MainViewModel
    {
        /// <summary>
        /// Contains a ISalesController implementation.
        /// </summary>
        private ISalesController _salesController;

        /// <summary>
        /// Contains the SalesViewModel.
        /// </summary>
        public SalesViewModel SalesViewModel { get; private set; }

        /// <summary>
        /// Contains the NumpadViewModel.
        /// </summary>
        public NumpadViewModel NumpadViewModel { get; private set; }

        /// <summary>
        /// Contains the TabViewModel.
        /// </summary>
        public TabViewModel TabViewModel { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="salesController">An ISalesController implementation.</param>
        public MainViewModel(ISalesController salesController)
        {
            _salesController = salesController;
            NumpadViewModel = new NumpadViewModel();
            TabViewModel = new TabViewModel(salesController, NumpadViewModel);
            SalesViewModel = new SalesViewModel(salesController);
        }
    }
}