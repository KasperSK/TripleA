using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CashRegister.GUI.Dialogs;
using CashRegister.GUI.ViewModels;
using CashRegister.Log;
using CashRegister.Receipts;

namespace CashRegister.GUI.Views
{
    /// <summary>
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
        }

        private void SalesView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = ((ActualHeight+ActualWidth)/40);

            Total.FontSize = (ActualWidth + ActualHeight)/30;
        }

        private ILogger _logger = LogFactory.GetLogger(typeof (SalesView));

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            PaymentDialog dlg = new PaymentDialog();
            dlg.Owner = parentWindow;
            
            if (dlg.ShowDialog() == true)
            {
                _logger.Debug("Payment choice " + dlg.PaymentChoice);
                var helper = parentWindow?.DataContext as MainViewModel;
                var relay = helper?.SalesViewModel.PaytypeCommand; 
                relay?.Execute(dlg.PaymentChoice);
            }
        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((this));

            var helper = parentWindow?.DataContext as MainViewModel;

            string balance = helper?.SalesViewModel.GetTally();

            var balanceDlg = new BalanceDialog(balance);

            balanceDlg.Owner = parentWindow;

            if (balanceDlg.ShowDialog() == true)
            {
                //var balanceReceipt = new Receipt();

                //balanceReceipt.Add(balance);


                ////helper?.SalesViewModel.print(balanceReceipt);
            };

            

        }
    }
}
