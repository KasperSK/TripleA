namespace CashRegister.GUI.ViewModels
{
    public class MainViewModel
    {
        public BaseViewModel SalesViewModel { get; set; }
        public BaseViewModel NumpadViewModel { get; set; }

        public MainViewModel()
        {
            SalesViewModel = new SalesViewModel();
            NumpadViewModel = new NumpadViewModel();
        }
    }
}