using CashRegister.Sales;

namespace CashRegister.GUI.ViewModels
{
    public class MainViewModel
    {
        private ISalesController _salesController ;
        public SalesViewModel SalesViewModel { get; private set; }
        public NumpadViewModel NumpadViewModel { get; private set; }
        public TabViewModel TabViewModel { get; private set; }

        public MainViewModel(ISalesController salesController)
        {
            _salesController = salesController;
            NumpadViewModel = new NumpadViewModel();
            TabViewModel = new TabViewModel(salesController, NumpadViewModel);
            SalesViewModel = new SalesViewModel(salesController);
        }
    }
}