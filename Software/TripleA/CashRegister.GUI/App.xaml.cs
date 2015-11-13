using System.Windows;
using CashRegister.GUI.ViewModels;

namespace CashRegister.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Window win = new MainWindow();
            win.DataContext = new MainViewModel();
            win.Show();
        }
    }
}
