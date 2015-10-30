namespace CashRegister.GUI.ViewModels
{
    public class MainViewModel
    {
        public BaseViewModel ViewModel { get; set; }

        public MainViewModel()
        {
            ViewModel = new SettingsViewModel();
        }
    }
}