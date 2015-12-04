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
        /// <summary>
        /// Constructor
        /// </summary>
        public SalesView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Changes the font size when window is resized.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments sent with the event.</param>
        private void SalesView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = ((ActualHeight+ActualWidth)/40);

            Total.FontSize = (ActualWidth + ActualHeight)/30;
        }
    }
}
