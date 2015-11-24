using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using CashRegister.Models;
using CashRegister.Payment;

namespace CashRegister.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for PaymentDialog.xaml
    /// </summary>
    public partial class PaymentDialog : Window
    {
        public PaymentDialog()
        {
            InitializeComponent();
            DataContext = data;

            Cash.Click += cashButton_Click;
            Dankort.Click += dankortButton_Click;
            MobilePay.Click += mobilePayButton_Click;
        }

        void cashButton_Click(object sender, RoutedEventArgs e)
        {
            data.CurrentPaymentType = PaymentType.Cash;
            DialogResult = true;
        }

        void dankortButton_Click(object sender, RoutedEventArgs e)
        {
            data.CurrentPaymentType = PaymentType.Nets;
            DialogResult = true;
        }

        void mobilePayButton_Click(object sender, RoutedEventArgs e)
        {
            data.CurrentPaymentType = PaymentType.MobilePay;
            DialogResult = true;
        }

        class DialogData : INotifyPropertyChanged
        {
            PaymentType currentPaymentType;
            public PaymentType CurrentPaymentType
            {
                get { return currentPaymentType; }
                set { currentPaymentType = value; Notify("currentPaymentType"); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            void Notify(string prop) { if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); } }
        }

        DialogData data = new DialogData();

        public PaymentType PaymentChoice
        {
            get { return data.CurrentPaymentType; }
            set { data.CurrentPaymentType = value; }
        }
    }
}
